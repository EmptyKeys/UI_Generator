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
    /// Implements generator for Thickness value
    /// </summary>    
    public class ThicknessGeneratorValue : IGeneratorValue
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
                return typeof(Thickness);
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
                Thickness thickness = (Thickness)value;

                if (thickness.Left == thickness.Top && thickness.Top == thickness.Right && thickness.Right == thickness.Bottom)
                {
                    valueExpression = new CodeObjectCreateExpression("Thickness", new CodePrimitiveExpression((float)thickness.Left));
                }
                else
                {
                    valueExpression = new CodeObjectCreateExpression(
                                "Thickness",
                                new CodePrimitiveExpression((float)thickness.Left),
                                new CodePrimitiveExpression((float)thickness.Top),
                                new CodePrimitiveExpression((float)thickness.Right),
                                new CodePrimitiveExpression((float)thickness.Bottom));
                }
            }

            return valueExpression;
        }
    }
}
