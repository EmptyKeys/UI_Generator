using System;
using System.CodeDom;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace EmptyKeys.UserInterface.Generator.Types
{
    /// <summary>
    /// Implements Button control generator
    /// </summary>
    public class ButtonGeneratorType : ContentControlGeneratorType
    {
        /// <summary>
        /// Gets the type of the xaml.
        /// </summary>
        /// <value>
        /// The type of the xaml.
        /// </value>
        public override Type XamlType
        {
            get
            {
                return typeof(Button);
            }
        }

        /// <summary>
        /// Generates control fields
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="initMethod">The initialize method.</param>
        /// <param name="generateField">if set to <c>true</c> [generate field].</param>
        /// <returns></returns>
        public override CodeExpression Generate(DependencyObject source, CodeTypeDeclaration classType, CodeMemberMethod initMethod, bool generateField)
        {
            CodeExpression fieldReference = base.Generate(source, classType, initMethod, generateField);
            ButtonBase buttonBase = source as ButtonBase;

            RoutedCommand command = buttonBase.Command as RoutedCommand;
            if (command != null && !string.IsNullOrEmpty(command.Name) && command.OwnerType != null)
            {                
                initMethod.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(fieldReference, "Command"), 
                    new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(command.OwnerType.Name), command.Name + "Command")));
            }

            CodeComHelper.GenerateField<object>(initMethod, fieldReference, source, ButtonBase.CommandParameterProperty);
            CodeComHelper.GenerateEnumField<ClickMode>(initMethod, fieldReference, source, ButtonBase.ClickModeProperty);
            return fieldReference;
        }
    }
}
