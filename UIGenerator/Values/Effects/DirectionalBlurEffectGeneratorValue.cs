using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EmptyKeys.UserInterface.Designer.Effects;

namespace EmptyKeys.UserInterface.Generator.Values.Effects
{
    /// <summary>
    /// Implements value generator for Directional Blur Effect
    /// </summary>
    /// <seealso cref="EmptyKeys.UserInterface.Generator.IGeneratorValue" />
    public class DirectionalBlurEffectGeneratorValue : IGeneratorValue
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
                return typeof(DirectionalBlurEffect);
            }
        }

        /// <summary>
        /// Generates the specified parent class.
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
                string variableName = baseName + "_dbef";
                var variable = new CodeVariableDeclarationStatement("DirectionalBlurEffect", variableName, new CodeObjectCreateExpression("DirectionalBlurEffect"));
                method.Statements.Add(variable);
                valueExpression = new CodeVariableReferenceExpression(variableName);

                DirectionalBlurEffect effect = value as DirectionalBlurEffect;
                CodeComHelper.GenerateField<float>(method, valueExpression, effect, DirectionalBlurEffect.AngleProperty);
                CodeComHelper.GenerateField<float>(method, valueExpression, effect, DirectionalBlurEffect.BlurAmountProperty);
            }

            return valueExpression;                      
        }
    }
}
