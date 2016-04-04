using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using EmptyKeys.UserInterface.Designer;

namespace EmptyKeys.UserInterface.Generator.Values
{
    /// <summary>
    /// Implements generator for SolidColorBrushAnimation
    /// </summary>
    public class SolidColorBrushAnimGeneratorValue : TimelineGeneratorValue
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
                return typeof(SolidColorBrushAnimation);
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
            SolidColorBrushAnimation animation = value as SolidColorBrushAnimation;
            CodeComHelper.GenerateColorField(method, baseValue, animation, SolidColorBrushAnimation.FromProperty);
            CodeComHelper.GenerateColorField(method, baseValue, animation, SolidColorBrushAnimation.ToProperty);
            CodeComHelper.GenerateColorField(method, baseValue, animation, SolidColorBrushAnimation.ByProperty);
            CodeComHelper.GenerateField<bool>(method, baseValue, animation, SolidColorBrushAnimation.IsAdditiveProperty);

            if (animation.EasingFunction != null)
            {
                EasingFunctionBase easingFunc = animation.EasingFunction as EasingFunctionBase;
                CodeComHelper.GenerateEasingFunction(method, baseValue, baseName, easingFunc);
            }

            return baseValue;
        }  
    }
}
