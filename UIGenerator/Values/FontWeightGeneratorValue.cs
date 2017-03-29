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
    /// Implements generator for Font Weight value
    /// </summary>    
    public class FontWeightGeneratorValue : IGeneratorValue
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
                return typeof(FontWeight);
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
            CodeExpression valueExpression = null;
            if (value != null)
            {
                FontWeight fontWeight = (FontWeight)value;
                CodeTypeReferenceExpression typeReference = new CodeTypeReferenceExpression("FontStyle");
                if (fontWeight == FontWeights.Normal)
                {
                    valueExpression = new CodeFieldReferenceExpression(typeReference, "Regular");
                }
                else
                {
                    valueExpression = new CodeFieldReferenceExpression(typeReference, fontWeight.ToString());
                }
            }

            return valueExpression;
        }
    }
}
