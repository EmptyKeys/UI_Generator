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
    /// Implements generator for Items Panel Template
    /// </summary>
    public class ItemsPanelTemplateGeneratorValue : IGeneratorValue
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
                return typeof(ItemsPanelTemplate);
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
            return GetItemsPanelTemplateValueExpression(parentClass, method, value, baseName);
        }

        private static CodeExpression GetItemsPanelTemplateValueExpression(CodeTypeDeclaration parentClass, CodeMemberMethod method, object value, string baseName)
        {
            ItemsPanelTemplate template = value as ItemsPanelTemplate;
            DependencyObject content = template.LoadContent();
            string variableName = baseName + "_ipt";
            string creator = CodeComHelper.GenerateTemplate(parentClass, method, content, variableName);
            CodeVariableDeclarationStatement templateVar = new CodeVariableDeclarationStatement(
                    "ControlTemplate", variableName,
                    new CodeObjectCreateExpression("ControlTemplate", new CodeSnippetExpression(creator)));
            method.Statements.Add(templateVar);
            return new CodeVariableReferenceExpression(variableName);
        }
    }
}