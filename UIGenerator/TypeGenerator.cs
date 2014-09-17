using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EmptyKeys.UserInterface.Generator.Types;

namespace EmptyKeys.UserInterface.Generator
{
    /// <summary>
    /// Implements generator for XAML types
    /// </summary>
    public class TypeGenerator
    {
        /// <summary>
        /// Gets the generators.
        /// </summary>
        /// <value>
        /// The generators.
        /// </value>
        public Dictionary<Type, IGeneratorType> Generators
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeGenerator"/> class.
        /// </summary>
        public TypeGenerator()
        {
            Generators = new Dictionary<Type, IGeneratorType>();

            IGeneratorType textBlock = new TextBlockGeneratorType();
            Generators.Add(textBlock.XamlType, textBlock);

            IGeneratorType stackPanel = new StackPanelGeneratorType();
            Generators.Add(stackPanel.XamlType, stackPanel);

            IGeneratorType uiRoot = new UIRootGeneratorType();
            Generators.Add(uiRoot.XamlType, uiRoot);

            IGeneratorType border = new BorderGeneratorType();
            Generators.Add(border.XamlType, border);

            IGeneratorType button = new ButtonGeneratorType();
            Generators.Add(button.XamlType, button);

            IGeneratorType control = new ControlGeneratorType();
            Generators.Add(control.XamlType, control);

            IGeneratorType contentControl = new ContentControlGeneratorType();
            Generators.Add(contentControl.XamlType, contentControl);

            IGeneratorType presenter = new ContentPresenterGeneratorType();
            Generators.Add(presenter.XamlType, presenter);

            IGeneratorType toggle = new ToggleButtonGeneratorType();
            Generators.Add(toggle.XamlType, toggle);

            IGeneratorType checkBox = new CheckBoxGeneratorType();
            Generators.Add(checkBox.XamlType, checkBox);

            IGeneratorType itemsControl = new ItemsControlGeneratorType();
            Generators.Add(itemsControl.XamlType, itemsControl);

            IGeneratorType combo = new ComboBoxGeneratorType();
            Generators.Add(combo.XamlType, combo);

            IGeneratorType dataGrid = new DataGridGeneratorType();
            Generators.Add(dataGrid.XamlType, dataGrid);

            IGeneratorType expander = new ExpanderGeneratorType();
            Generators.Add(expander.XamlType, expander);

            IGeneratorType grid = new GridGeneratorType();
            Generators.Add(grid.XamlType, grid);

            IGeneratorType header = new HeaderedContentControlGeneratorType();
            Generators.Add(header.XamlType, header);

            IGeneratorType image = new ImageGeneratorType();
            Generators.Add(image.XamlType, image);

            IGeneratorType listBox = new ListBoxGeneratorType();
            Generators.Add(listBox.XamlType, listBox);

            IGeneratorType scrollViewer = new ScrollViewerGeneratorType();
            Generators.Add(scrollViewer.XamlType, scrollViewer);

            IGeneratorType tabControl = new TabControlGeneratorType();
            Generators.Add(tabControl.XamlType, tabControl);

            IGeneratorType textBox = new TextBoxGeneratorType();
            Generators.Add(textBox.XamlType, textBox);

            IGeneratorType progressBar = new ProgressBarGeneratorType();
            Generators.Add(progressBar.XamlType, progressBar);

            IGeneratorType scrollContentPresenter = new ScrollContentPresenterGeneratorType();
            Generators.Add(scrollContentPresenter.XamlType, scrollContentPresenter);

            IGeneratorType repeatButton = new RepeatButtonGeneratorType();
            Generators.Add(repeatButton.XamlType, repeatButton);

            IGeneratorType scrollBar = new ScrollBarGeneratorType();
            Generators.Add(scrollBar.XamlType, scrollBar);

            IGeneratorType listBoxItem = new ListBoxItemGeneratorType();
            Generators.Add(listBoxItem.XamlType, listBoxItem);

            IGeneratorType tabItem = new TabItemGeneratorType();
            Generators.Add(tabItem.XamlType, tabItem);

            IGeneratorType comboBoxItem = new ComboBoxItemGeneratorType();
            Generators.Add(comboBoxItem.XamlType, comboBoxItem);

            IGeneratorType userControl = new UserControlGeneratorType();
            Generators.Add(userControl.XamlType, userControl);

            IGeneratorType customUserControl = new CustomUserControlGeneratorType();
            Generators.Add(customUserControl.XamlType, customUserControl);

            IGeneratorType track = new TrackGeneratorType();
            Generators.Add(track.XamlType, track);

            IGeneratorType thumb = new ThumbGeneratorType();
            Generators.Add(thumb.XamlType, thumb);

            IGeneratorType slider = new SliderGeneratorType();
            Generators.Add(slider.XamlType, slider);

            IGeneratorType numeric = new NumericTextBoxGeneratorType();
            Generators.Add(numeric.XamlType, numeric);
        }

        /// <summary>
        /// Processes the generators.
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="method">The initialize method.</param>
        /// <param name="generateFields">if set to <c>true</c> [generate fields].</param>
        /// <returns></returns>
        public CodeExpression ProcessGenerators(object source, CodeTypeDeclaration classType, CodeMemberMethod method, bool generateFields)
        {
            if (source == null)
            {
                return null;
            }

            IGeneratorType generator;
            Type elementType = source.GetType();

            if (elementType.BaseType == typeof(UserControl))
            {
                // we have to retype any custom user control to fake generator so we can generate code for it
                elementType = typeof(CustomUserControlGeneratorType);
            }

            if (Generators.TryGetValue(elementType, out generator))
            {
                DependencyObject xamlSource = source as DependencyObject;

                if (xamlSource != null)
                {
                    CodeExpression parent = generator.Generate(xamlSource, classType, method, generateFields);
                    CodeComHelper.GenerateAttachedProperties(method, parent, xamlSource);
                    FrameworkElement elem = source as FrameworkElement;
                    if (elem != null)
                    {
                        CodeComHelper.GenerateBindings(method, parent, elem);
                        CodeComHelper.GenerateResourceReferences(method, parent, elem);

                        if (elem.Resources.Count != 0 || elem.Resources.MergedDictionaries.Count != 0)
                        {
                            ResourceDictionaryGenerator resourcesGenerator = new ResourceDictionaryGenerator();

                            CodeMemberMethod resourcesMethod = new CodeMemberMethod();
                            resourcesMethod.Name = "InitializeElement" + elem.Name + "Resources";
                            resourcesMethod.Parameters.Add(new CodeParameterDeclarationExpression("UIElement", "elem"));
                            classType.Members.Add(resourcesMethod);
                            resourcesGenerator.Generate(elem.Resources, classType, resourcesMethod, 
                                new CodeFieldReferenceExpression(new CodeVariableReferenceExpression("elem"), "Resources"));

                            method.Statements.Add(new CodeMethodInvokeExpression(
                                new CodeThisReferenceExpression(), resourcesMethod.Name, parent));
                        }
                    }

                    IEnumerable children = generator.GetChildren(xamlSource);
                    if (children != null)
                    {
                        foreach (DependencyObject child in children)
                        {
                            if (child == null)
                            {
                                continue;
                            }

                            int index = method.Statements.Count;
                            CodeExpression childRef = ProcessGenerators(child, classType, method, generateFields);
                            if (childRef != null)
                            {
                                generator.AddChild(parent, childRef, method, index + 2); // +1 for creating instance +1 for comment
                            }
                        }
                    }

                    return parent;
                }
            }

            string errorText = "Unknown type : " + source.GetType(); 
            Console.WriteLine(errorText);
            CodeSnippetStatement error = new CodeSnippetStatement("#error " + errorText);
            method.Statements.Add(error);

            return null;
        }
    }
}
