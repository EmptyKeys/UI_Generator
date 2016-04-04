using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EmptyKeys.UserInterface.Generator.Types
{
    /// <summary>
    /// Implements Slider UI Generator
    /// </summary>
    public class SliderGeneratorType : RangeBaseGeneratorType
    {
        /// <summary>
        /// Gets the type of the xaml.
        /// </summary>
        /// <value>
        /// The type of the xaml.
        /// </value>
        public override Type XamlType
        {
            get
            {
                return typeof(Slider);
            }
        }

        /// <summary>
        /// Generates the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="method">The method.</param>
        /// <param name="generateField">if set to <c>true</c> [generate field].</param>
        /// <returns></returns>
        public override CodeExpression Generate(DependencyObject source, CodeTypeDeclaration classType, CodeMemberMethod method, bool generateField)
        {
            CodeExpression fieldReference = base.Generate(source, classType, method, generateField);

            CodeComHelper.GenerateEnumField<Orientation>(method, fieldReference, source, Slider.OrientationProperty);
            CodeComHelper.GenerateField<bool>(method, fieldReference, source, Slider.IsSnapToTickEnabledProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(method, fieldReference, source, Slider.TickFrequencyProperty);

            return fieldReference;
        }
    }
}
