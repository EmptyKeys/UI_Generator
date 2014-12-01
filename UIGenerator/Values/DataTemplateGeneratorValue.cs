using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmptyKeys.UserInterface.Generator.Values
{
    /// <summary>
    /// Implements generator for Data Template
    /// </summary>
    public class DataTemplateGeneratorValue : IGeneratorValue
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
                return typeof(DataTemplate);
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
            return GetDataTemplateValueExpression(parentClass, method, value, baseName);
        }

        private static CodeExpression GetDataTemplateValueExpression(CodeTypeDeclaration parentClass, CodeMemberMethod method, object value, string baseName)
        {
            DataTemplate dataTemplate = value as DataTemplate;
            Type dataType = dataTemplate.DataType as Type;
            DependencyObject content = dataTemplate.LoadContent();
            string variableName = baseName + "_dt";
            string creator = CodeComHelper.GenerateTemplate(parentClass, method, content, variableName);

            if (dataType != null)
            {
                return new CodeObjectCreateExpression("DataTemplate", new CodeTypeOfExpression(dataType.FullName), new CodeSnippetExpression(creator));
            }

            return new CodeObjectCreateExpression("DataTemplate", new CodeSnippetExpression(creator));
        }
    }
}
