using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace EmptyKeys.UserInterface.Generator.Values
{
    /// <summary>
    /// Implements value generator for DoubleAnimation type
    /// </summary>
    public class DoubleAnimationGeneratorValue : TimelineGeneratorValue
    {
        /// <summary>
        /// Gets the type of the value.
        /// </summary>
        /// <value>
        /// The type of the value.
        /// </value>
        public override Type ValueType
        {
            get
            {
                return typeof(DoubleAnimation);
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
        public override CodeExpression Generate(CodeTypeDeclaration parentClass, CodeMemberMethod method, object value, string baseName, ResourceDictionary dictionary = null)
        {
            CodeExpression baseValue = base.Generate(parentClass, method, value, baseName, dictionary);
            DoubleAnimation animation = value as DoubleAnimation;
            CodeComHelper.GenerateFieldDoubleToFloat(method, baseValue, animation, DoubleAnimation.FromProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(method, baseValue, animation, DoubleAnimation.ToProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(method, baseValue, animation, DoubleAnimation.ByProperty);
            CodeComHelper.GenerateField<bool>(method, baseValue, animation, DoubleAnimation.IsAdditiveProperty);

            if (animation.EasingFunction != null)
            {
                EasingFunctionBase easingFunc = animation.EasingFunction as EasingFunctionBase;
                CodeComHelper.GenerateEasingFunction(method, baseValue, baseName, easingFunc);                
            }

            return baseValue;
        }        
    }
}
