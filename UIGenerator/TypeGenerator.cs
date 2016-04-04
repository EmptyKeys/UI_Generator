using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EmptyKeys.UserInterface.Designer;
using EmptyKeys.UserInterface.Generator.Types;
using EmptyKeys.UserInterface.Generator.Types.Charts;
using EmptyKeys.UserInterface.Generator.Types.Controls;
using EmptyKeys.UserInterface.Generator.Types.Controls.Primitives;
using EmptyKeys.UserInterface.Generator.Types.Shapes;

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

            IGeneratorType dataGridColumn = new DataGridColumnGeneratorType();
            Generators.Add(dataGridColumn.XamlType, dataGridColumn);

            IGeneratorType dataGridBoundColumn = new DataGridBoundColumnGeneratorType();
            Generators.Add(dataGridBoundColumn.XamlType, dataGridBoundColumn);

            IGeneratorType dataGridTextColumn = new DataGridTextColumnGeneratorType();
            Generators.Add(dataGridTextColumn.XamlType, dataGridTextColumn);

            IGeneratorType dataGridCheckBoxColumn = new DataGridCheckBoxColumnGeneratorType();
            Generators.Add(dataGridCheckBoxColumn.XamlType, dataGridCheckBoxColumn);

            IGeneratorType dataGridTemplateColumn = new DataGridTemplateColumnGeneratorType();
            Generators.Add(dataGridTemplateColumn.XamlType, dataGridTemplateColumn);

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

            IGeneratorType pass = new PasswordBoxGeneratorType();
            Generators.Add(pass.XamlType, pass);

            IGeneratorType customPassword = new CustomPasswordBoxGeneratorType();
            Generators.Add(customPassword.XamlType, customPassword);

            IGeneratorType animImage = new AnimatedImageGeneratorType();
            Generators.Add(animImage.XamlType, animImage);

            IGeneratorType shape = new ShapeGeneratorType();
            Generators.Add(shape.XamlType, shape);

            IGeneratorType rectangle = new RectangleGeneratorType();
            Generators.Add(rectangle.XamlType, rectangle);

            IGeneratorType ellipse = new EllipseGeneratorType();
            Generators.Add(ellipse.XamlType, ellipse);

            IGeneratorType canvas = new CanvasGeneratorType();
            Generators.Add(canvas.XamlType, canvas);

            IGeneratorType path = new PathGeneratorType();
            Generators.Add(path.XamlType, path);

            IGeneratorType dockPanel = new DockPanelGeneratorType();
            Generators.Add(dockPanel.XamlType, dockPanel);

            IGeneratorType line = new LineGeneratorType();
            Generators.Add(line.XamlType, line);

            IGeneratorType treeView = new TreeViewGeneratorType();
            Generators.Add(treeView.XamlType, treeView);

            IGeneratorType headerItems = new HeaderedItemsControlGeneratorType();
            Generators.Add(headerItems.XamlType, headerItems);

            IGeneratorType treeViewItem = new TreeViewItemGeneratorType();
            Generators.Add(treeViewItem.XamlType, treeViewItem);

            IGeneratorType wrapPanel = new WrapPanelGeneratorType();
            Generators.Add(wrapPanel.XamlType, wrapPanel);

            IGeneratorType popup = new PopupGeneratorType();
            Generators.Add(popup.XamlType, popup);

            IGeneratorType tabPanel = new TabPanelGeneratorType();
            Generators.Add(tabPanel.XamlType, tabPanel);

            IGeneratorType radio = new RadioButtonGeneratorType();
            Generators.Add(radio.XamlType, radio);

            IGeneratorType groupBox = new GroupBoxGeneratorType();
            Generators.Add(groupBox.XamlType, groupBox);

            IGeneratorType chart = new ChartGeneratorType();
            Generators.Add(chart.XamlType, chart);

            IGeneratorType series = new LineSeries2DGeneratorType();
            Generators.Add(series.XamlType, series);

            IGeneratorType seriesPoint = new SeriesPointGeneratorType();
            Generators.Add(seriesPoint.XamlType, seriesPoint);
        }

        /// <summary>
        /// Determines whether the specified type has generator.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public bool HasGenerator(Type type)
        {
            return Generators.ContainsKey(type);
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

            bool customUserControl = false;
            if (elementType.BaseType == typeof(UserControl))
            {
                // we have to retype any custom user control to fake generator so we can generate code for it
                customUserControl = true;
                elementType = typeof(CustomUserControlGeneratorType);
            }

            if (Generators.TryGetValue(elementType, out generator))
            {
                DependencyObject xamlSource = source as DependencyObject;

                if (xamlSource != null)
                {
                    CodeExpression parent = generator.Generate(xamlSource, classType, method, generateFields);
                    CodeComHelper.GenerateAttachedProperties(method, parent, xamlSource);

                    Type oldDataType = BindingGenerator.Instance.ActiveDataType;
                    Type newType = xamlSource.GetValue(GeneratedBindings.DataTypeProperty) as Type;
                    if (newType != null)
                    {
                        BindingGenerator.Instance.ActiveDataType = newType;
                    }

                    FrameworkElement elem = source as FrameworkElement;
                    if (elem != null)
                    {
                        CodeComHelper.GenerateBindings(method, parent, elem, elem.Name);
                        CodeComHelper.GenerateResourceReferences(method, parent, elem);

                        if (!customUserControl && (elem.Resources.Count != 0 || elem.Resources.MergedDictionaries.Count != 0))
                        {
                            ResourceDictionaryGenerator resourcesGenerator = new ResourceDictionaryGenerator();

                            CodeMemberMethod resourcesMethod = new CodeMemberMethod();
                            resourcesMethod.Attributes = MemberAttributes.Static | MemberAttributes.Private;
                            resourcesMethod.Name = "InitializeElement" + elem.Name + "Resources";
                            resourcesMethod.Parameters.Add(new CodeParameterDeclarationExpression("UIElement", "elem"));
                            classType.Members.Add(resourcesMethod);
                            resourcesGenerator.Generate(elem.Resources, classType, resourcesMethod, 
                                new CodeFieldReferenceExpression(new CodeVariableReferenceExpression("elem"), "Resources"));

                            method.Statements.Add(new CodeMethodInvokeExpression(null,resourcesMethod.Name, parent));
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

                    BindingGenerator.Instance.ActiveDataType = oldDataType;

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
