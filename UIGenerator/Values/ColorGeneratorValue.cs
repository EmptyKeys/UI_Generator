using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace EmptyKeys.UserInterface.Generator.Values
{
    /// <summary>
    /// Implements value generator for Color type
    /// </summary>
    public class ColorGeneratorValue : IGeneratorValue
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
                return typeof(Color);
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
            Color color = (Color)value;
            CodeObjectCreateExpression colorExpr = new CodeObjectCreateExpression(
                    "ColorW",
                    new CodePrimitiveExpression(color.R),
                    new CodePrimitiveExpression(color.G),
                    new CodePrimitiveExpression(color.B),
                    new CodePrimitiveExpression(color.A));

            return colorExpr;
        }
    }
}
