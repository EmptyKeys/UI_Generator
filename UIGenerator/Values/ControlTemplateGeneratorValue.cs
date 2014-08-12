using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EmptyKeys.UserInterface.Generator.Values
{
    /// <summary>
    /// Implements generator for Control Template
    /// </summary>
    public class ControlTemplateGeneratorValue : IGeneratorValue
    {
        /// <summary>
        /// Gets the type of the value.
        /// </summary>
        /// <value>
        /// The type of the value.
        /// </value>
        public Type ValueType
        {
            get
            {
                return typeof(ControlTemplate);
            }
        }

        /// <summary>
        /// Generates code for value
        /// </summary>
        /// <param name="parentClass">The parent class.</param>
        /// <param name="method">The method.</param>
        /// <param name="value">The value.</param>
        /// <param name="baseName">Name of the base.</param>
        /// <param name="dictionary">The dictionary.</param>
        /// <returns></returns>
        public CodeExpression Generate(CodeTypeDeclaration parentClass, CodeMemberMethod method, object value, string baseName, ResourceDictionary dictionary = null)
        {
            return GetControlTemplateValueExpression(parentClass, method, value, baseName);
        }

        private static CodeExpression GetControlTemplateValueExpression(CodeTypeDeclaration parentClass, CodeMemberMethod method, object value, string baseName)
        {
            ControlTemplate controlTemplate = value as ControlTemplate;
            DependencyObject content = controlTemplate.LoadContent();
            string variableName = baseName + "_ct";
            string creator = CodeComHelper.GenerateTemplate(parentClass, method, content, variableName);
            Type targetType = controlTemplate.TargetType;
            CodeVariableDeclarationStatement controlTemplateVar;
            if (targetType != null)
            {
                controlTemplateVar = new CodeVariableDeclarationStatement(
                    "ControlTemplate", variableName,
                    new CodeObjectCreateExpression("ControlTemplate", new CodeTypeOfExpression(targetType.Name), new CodeSnippetExpression(creator)));
            }
            else
            {
                controlTemplateVar = new CodeVariableDeclarationStatement(
                    "ControlTemplate", variableName,
                    new CodeObjectCreateExpression("ControlTemplate", new CodeSnippetExpression(creator)));
            }

            method.Statements.Add(controlTemplateVar);

            TriggerCollection triggers = controlTemplate.Triggers;
            CodeComHelper.GenerateTriggers(parentClass, method, variableName, targetType, triggers);

            return new CodeVariableReferenceExpression(variableName);
        }
    }
}
