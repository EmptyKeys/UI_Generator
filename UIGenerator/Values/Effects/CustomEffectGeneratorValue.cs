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
    /// Implements value generator for Custom Effect
    /// </summary>
    /// <seealso cref="EmptyKeys.UserInterface.Generator.IGeneratorValue" />
    public class CustomEffectGeneratorValue : IGeneratorValue
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
                return typeof(CustomEffect);
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
                string variableName = baseName + "_cef";
                var variable = new CodeVariableDeclarationStatement("CustomEffect", variableName, new CodeObjectCreateExpression("CustomEffect"));
                method.Statements.Add(variable);
                valueExpression = new CodeVariableReferenceExpression(variableName);

                CustomEffect effect = value as CustomEffect;
                CodeComHelper.GenerateField<string>(method, valueExpression, effect, CustomEffect.EffectAssetProperty);

                EffectAssets.Instance.AddEffect(effect.EffectAsset);
            }

            return valueExpression;                      
        }
    }
}
