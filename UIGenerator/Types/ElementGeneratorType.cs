using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using EmptyKeys.UserInterface.Designer.Input;

namespace EmptyKeys.UserInterface.Generator.Types
{
    /// <summary>
    /// Implements UI Element generator
    /// </summary>
    public class ElementGeneratorType : IGeneratorType
    {
        private static int nameUniqueId;

        /// <summary>
        /// Gets the type of the xaml.
        /// </summary>
        /// <value>
        /// The type of the xaml.
        /// </value>
        public virtual Type XamlType
        {
            get { return typeof(UIElement); }
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <param name="source">The dependency object</param>
        /// <returns></returns>
        public virtual IEnumerable GetChildren(DependencyObject source)
        {
            return null;
        }

        /// <summary>
        /// Generates control fields
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="method">The initialize method.</param>
        /// <param name="generateField">if set to <c>true</c> [generate field].</param>
        /// <returns></returns>
        public virtual CodeExpression Generate(DependencyObject source, CodeTypeDeclaration classType, CodeMemberMethod method, bool generateField)
        {
            FrameworkElement element = source as FrameworkElement;
            string typeName = element.GetType().Name;

            if (string.IsNullOrEmpty(element.Name))
            {
                element.Name = "e_" + nameUniqueId;
                nameUniqueId++;
            }

            if (generateField)
            {
                CodeMemberField field = new CodeMemberField(typeName, element.Name);
                classType.Members.Add(field);
            }

            CodeComment comment = new CodeComment(element.Name + " element");
            method.Statements.Add(new CodeCommentStatement(comment));

            CodeExpression fieldReference = null;
            if (!generateField)
            {
                fieldReference = new CodeVariableReferenceExpression(element.Name);
                CodeTypeReference variableType = new CodeTypeReference(typeName);
                CodeVariableDeclarationStatement declaration = new CodeVariableDeclarationStatement(variableType, element.Name);
                declaration.InitExpression = new CodeObjectCreateExpression(typeName);
                method.Statements.Add(declaration);
            }
            else
            {
                fieldReference = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), element.Name);
                method.Statements.Add(new CodeAssignStatement(fieldReference, new CodeObjectCreateExpression(typeName)));
            }

            CodeComHelper.GenerateField<string>(method, fieldReference, source, FrameworkElement.NameProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(method, fieldReference, source, FrameworkElement.HeightProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(method, fieldReference, source, FrameworkElement.MaxHeightProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(method, fieldReference, source, FrameworkElement.MinHeightProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(method, fieldReference, source, FrameworkElement.WidthProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(method, fieldReference, source, FrameworkElement.MaxWidthProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(method, fieldReference, source, FrameworkElement.MinWidthProperty);
            CodeComHelper.GenerateField<object>(method, fieldReference, source, FrameworkElement.TagProperty);
            CodeComHelper.GenerateField<bool>(method, fieldReference, source, FrameworkElement.IsEnabledProperty);
            CodeComHelper.GenerateField<bool>(method, fieldReference, source, FrameworkElement.IsHitTestVisibleProperty);
            CodeComHelper.GenerateField<bool>(method, fieldReference, source, FrameworkElement.SnapsToDevicePixelsProperty);
            CodeComHelper.GenerateField<bool>(method, fieldReference, source, UIElement.FocusableProperty);
            CodeComHelper.GenerateEnumField<Visibility>(method, fieldReference, source, FrameworkElement.VisibilityProperty);
            CodeComHelper.GenerateThicknessField(method, fieldReference, source, FrameworkElement.MarginProperty);
            CodeComHelper.GenerateEnumField<HorizontalAlignment>(method, fieldReference, source, FrameworkElement.HorizontalAlignmentProperty);
            CodeComHelper.GenerateEnumField<VerticalAlignment>(method, fieldReference, source, FrameworkElement.VerticalAlignmentProperty);
            CodeComHelper.GenerateTemplateStyleField(classType, method, fieldReference, source, FrameworkElement.StyleProperty);
            CodeComHelper.GenerateToolTipField(classType, method, fieldReference, source, FrameworkElement.ToolTipProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(method, fieldReference, source, FrameworkElement.OpacityProperty);

            if (element.Triggers.Count > 0)
            {
                string parentName = element.Name;
                GenerateTriggers(classType, method, element, typeName, fieldReference, parentName);
            }

            if (element.InputBindings.Count > 0)
            {
                GenerateInputBindings(method, element, fieldReference);
            }

            return fieldReference;
        }

        private static void GenerateInputBindings(CodeMemberMethod method, FrameworkElement element, CodeExpression fieldReference)
        {
            for (int i = 0; i < element.InputBindings.Count; i++)
            {
                CodeVariableDeclarationStatement bindingVar = null;
                string bindingVarName = element.Name + "_IB_" + i;
                var bindingVarRef = new CodeVariableReferenceExpression(bindingVarName);
                MouseBinding mouseBinding = element.InputBindings[i] as MouseBinding;
                if (mouseBinding != null)
                {
                    bindingVar = new CodeVariableDeclarationStatement("MouseBinding", bindingVarName, new CodeObjectCreateExpression("MouseBinding"));
                    method.Statements.Add(bindingVar);

                    MouseGesture mouseGesture = mouseBinding.Gesture as MouseGesture;
                    if (mouseGesture != null)
                    {
                        method.Statements.Add(new CodeAssignStatement(
                            new CodeFieldReferenceExpression(bindingVarRef, "Gesture"),
                            new CodeObjectCreateExpression("MouseGesture",
                                new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(MouseAction).Name), mouseGesture.MouseAction.ToString()),
                                new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(ModifierKeys).Name), mouseGesture.Modifiers.ToString())
                                )));
                    }
                }

                KeyBinding keyBinding = element.InputBindings[i] as KeyBinding;
                if (keyBinding != null)
                {
                    bindingVar = new CodeVariableDeclarationStatement("KeyBinding", bindingVarName, new CodeObjectCreateExpression("KeyBinding"));
                    method.Statements.Add(bindingVar);

                    KeyGesture keyGesture = keyBinding.Gesture as KeyGesture;
                    if (keyGesture != null)
                    {
                        method.Statements.Add(new CodeAssignStatement(
                            new CodeFieldReferenceExpression(bindingVarRef, "Gesture"),
                            new CodeObjectCreateExpression("KeyGesture",
                                new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("KeyCode"), keyGesture.Key.ToString()),
                                new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(ModifierKeys).Name), keyGesture.Modifiers.ToString()),
                                new CodePrimitiveExpression(keyGesture.DisplayString)
                                )));
                    }
                }

                GamepadBinding gamepadBinding = element.InputBindings[i] as GamepadBinding;
                if (gamepadBinding != null)
                {
                    bindingVar = new CodeVariableDeclarationStatement("GamepadBinding", bindingVarName, new CodeObjectCreateExpression("GamepadBinding"));
                    method.Statements.Add(bindingVar);

                    GamepadGesture gesture = gamepadBinding.Gesture as GamepadGesture;
                    if (gesture != null)
                    {
                        method.Statements.Add(new CodeAssignStatement(
                            new CodeFieldReferenceExpression(bindingVarRef, "Gesture"),
                            new CodeObjectCreateExpression("GamepadGesture",
                                new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeof(GamepadInput).Name), gesture.GamepadInput.ToString()))
                                ));
                    }
                }

                if (bindingVar != null)
                {
                    DependencyObject depObject = element.InputBindings[i] as DependencyObject;                    
                    CodeComHelper.GenerateField<object>(method, bindingVarRef, depObject, InputBinding.CommandParameterProperty);
                    CodeComHelper.GenerateBindings(method, bindingVarRef, depObject, bindingVarName, fieldReference, true);

                    method.Statements.Add(new CodeMethodInvokeExpression(
                        new CodeVariableReferenceExpression(element.Name), "InputBindings.Add", new CodeVariableReferenceExpression(bindingVarName)));
                }
            }
        }

        private static void GenerateTriggers(CodeTypeDeclaration parentClass, CodeMemberMethod method, FrameworkElement element, string typeName, CodeExpression fieldReference, string parentName)
        {
            for (int i = 0; i < element.Triggers.Count; i++)
            {
                EventTrigger trigger = element.Triggers[i] as EventTrigger;
                if (trigger == null)
                {
                    continue;
                }

                CodeComHelper.GenerateEventTrigger(parentClass, method, typeName, fieldReference, parentName, i, trigger);
            }
        }

        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="child">The child.</param>
        /// <param name="initMethod">The initialize method.</param>
        /// <param name="index">The index.</param>
        public virtual void AddChild(CodeExpression parent, CodeExpression child, CodeMemberMethod initMethod, int index)
        {
        }
    }
}
