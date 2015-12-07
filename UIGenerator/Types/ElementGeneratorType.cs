using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;
using EmptyKeys.UserInterface.Designer.Input;
using EmptyKeys.UserInterface.Designer.Interactions;

namespace EmptyKeys.UserInterface.Generator.Types
{
    /// <summary>
    /// Implements UI Element generator
    /// </summary>
    public class ElementGeneratorType : IGeneratorType
    {
        /// <summary>
        /// The name unique identifier
        /// </summary>
        public static int NameUniqueId;

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
                element.Name = "e_" + NameUniqueId;
                NameUniqueId++;
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

            if (element.Cursor != null)
            {
                CursorType cursorType = (CursorType)Enum.Parse(typeof(CursorType), element.Cursor.ToString());
                CodeComHelper.GenerateEnumField(method, fieldReference, "CursorType", typeof(CursorType).Name, cursorType.ToString());
            }

            if (element.Triggers.Count > 0)
            {
                string parentName = element.Name;
                GenerateTriggers(classType, method, element, typeName, fieldReference, parentName);
            }

            if (element.InputBindings.Count > 0)
            {
                GenerateInputBindings(method, element, fieldReference);
            }

            var behaviors = Interaction.GetBehaviors(element);
            if (behaviors.Count > 0)
            {
                GenerateBehaviors(behaviors, classType, method, element, fieldReference);
            }

            return fieldReference;
        }

        private void GenerateBehaviors(BehaviorCollection behaviors, CodeTypeDeclaration classType, CodeMemberMethod method, FrameworkElement element, CodeExpression fieldReference)
        {

            for (int i = 0; i < behaviors.Count; i++)
            {
                var behavior = behaviors[i];
                string behaviorName = element.Name + "_BEH_" + i;
                Type type = behavior.GetType();
                CodeVariableDeclarationStatement variable = new CodeVariableDeclarationStatement(type.Name, behaviorName, new CodeObjectCreateExpression(type.Name));
                method.Statements.Add(variable);
                var behaviorVarRef = new CodeVariableReferenceExpression(behaviorName);
                
                method.Statements.Add(new CodeMethodInvokeExpression(
                        new CodeVariableReferenceExpression("Interaction"), "GetBehaviors(" + element.Name + ").Add", behaviorVarRef));

                ValueGenerator valueGenerator = new ValueGenerator();
                MethodInfo generateFieldMethod = typeof(CodeComHelper).GetMethod("GenerateField");

                LocalValueEnumerator enumerator = behavior.GetLocalValueEnumerator();
                while (enumerator.MoveNext())
                {
                    LocalValueEntry entry = enumerator.Current;
                    DependencyProperty property = entry.Property;
                    Type valueType = entry.Value.GetType();
                    if (CodeComHelper.IsValidForFieldGenerator(entry.Value))
                    {
                        if (valueGenerator.Generators.ContainsKey(property.PropertyType) || valueGenerator.Generators.ContainsKey(valueType))
                        {
                            CodeExpression propValue = valueGenerator.ProcessGenerators(classType, method, entry.Value, behaviorName);
                            if (propValue != null)
                            {
                                method.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(behaviorVarRef, property.Name), propValue));
                            }
                        }
                        else if (entry.Value is ActionCollection)
                        {
                            GenerateBehaviorActions(entry.Value as ActionCollection, classType, method, behaviorVarRef, behaviorName);
                        }
                        else
                        {
                            MethodInfo generic = generateFieldMethod.MakeGenericMethod(property.PropertyType);
                            if (generic == null)
                            {
                                throw new NullReferenceException("Generic method not created for type - " + property.PropertyType);
                            }

                            generic.Invoke(null, new object[] { method, behaviorVarRef, behavior, property });
                        }
                    }
                }

                CodeComHelper.GenerateBindings(method, behaviorVarRef, behavior, behaviorName);
                CodeComHelper.GenerateResourceReferences(method, behaviorVarRef, behavior);
            }
        }

        private void GenerateBehaviorActions(ActionCollection actionCollection, CodeTypeDeclaration classType, CodeMemberMethod method, CodeVariableReferenceExpression behaviorVarRef, string behaviorName)
        {
            for (int i = 0; i < actionCollection.Count; i++)
            {
                var action = actionCollection[i];
                string actionName = behaviorName + "_ACT_" + i;
                Type type = action.GetType();

                CodeVariableDeclarationStatement variable = new CodeVariableDeclarationStatement(type.Name, actionName, new CodeObjectCreateExpression(type.Name));
                method.Statements.Add(variable);
                var actionVarRef = new CodeVariableReferenceExpression(actionName);

                method.Statements.Add(new CodeMethodInvokeExpression(
                        behaviorVarRef, "Actions.Add", actionVarRef));

                ValueGenerator valueGenerator = new ValueGenerator();
                MethodInfo generateFieldMethod = typeof(CodeComHelper).GetMethod("GenerateField");

                LocalValueEnumerator enumerator = action.GetLocalValueEnumerator();
                while (enumerator.MoveNext())
                {
                    LocalValueEntry entry = enumerator.Current;
                    DependencyProperty property = entry.Property;
                    Type valueType = entry.Value.GetType();
                    if (CodeComHelper.IsValidForFieldGenerator(entry.Value))
                    {
                        if (valueGenerator.Generators.ContainsKey(property.PropertyType) || valueGenerator.Generators.ContainsKey(valueType))
                        {
                            CodeExpression propValue = valueGenerator.ProcessGenerators(classType, method, entry.Value, actionName);
                            if (propValue != null)
                            {
                                method.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(actionVarRef, property.Name), propValue));
                            }
                        }
                        else if (entry.Value is PropertyPath)
                        {
                            PropertyPath path = entry.Value as PropertyPath;
                            method.Statements.Add(new CodeAssignStatement(
                                new CodeFieldReferenceExpression(actionVarRef, property.Name), 
                                new CodeObjectCreateExpression("PropertyPath", new CodePrimitiveExpression(path.Path))));
                        }
                        else
                        {
                            MethodInfo generic = generateFieldMethod.MakeGenericMethod(property.PropertyType);
                            if (generic == null)
                            {
                                throw new NullReferenceException("Generic method not created for type - " + property.PropertyType);
                            }

                            generic.Invoke(null, new object[] { method, actionVarRef, action, property });
                        }
                    }
                }

                CodeComHelper.GenerateBindings(method, actionVarRef, action, actionName, behaviorVarRef);
                //CodeComHelper.GenerateResourceReferences(method, actionVarRef, action);
            }
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
                    CodeComHelper.GenerateBindings(method, bindingVarRef, depObject, bindingVarName, fieldReference, false);

                    method.Statements.Add(new CodeMethodInvokeExpression(
                        new CodeVariableReferenceExpression(element.Name), "InputBindings.Add", new CodeVariableReferenceExpression(bindingVarName)));

                    method.Statements.Add(new CodeAssignStatement(
                        new CodeFieldReferenceExpression(new CodeVariableReferenceExpression(bindingVarName), "Parent"),
                        new CodeVariableReferenceExpression(element.Name)));
                }
            }
        }

        private static void GenerateTriggers(CodeTypeDeclaration parentClass, CodeMemberMethod method, FrameworkElement element, string typeName, CodeExpression fieldReference, string parentName)
        {
            for (int i = 0; i < element.Triggers.Count; i++)
            {
                System.Windows.EventTrigger trigger = element.Triggers[i] as System.Windows.EventTrigger;
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
