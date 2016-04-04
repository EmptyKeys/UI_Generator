using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace EmptyKeys.UserInterface.Generator.Types
{
    /// <summary>
    /// Implements Range Base generator
    /// </summary>
    public class RangeBaseGeneratorType : ControlGeneratorType
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
                return typeof(RangeBase);
            }
        }

        /// <summary>
        /// Generates control fields
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="method">The initialize method.</param>
        /// <param name="generateField">if set to <c>true</c> [generate field].</param>
        /// <returns></returns>
        public override CodeExpression Generate(DependencyObject source, CodeTypeDeclaration classType, CodeMemberMethod method, bool generateField)
        {
            CodeExpression fieldReference = base.Generate(source, classType, method, generateField);
            RangeBase rangeBase = source as RangeBase;

            CodeComHelper.GenerateFieldDoubleToFloat(method, fieldReference, source, RangeBase.MinimumProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(method, fieldReference, source, RangeBase.MaximumProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(method, fieldReference, source, RangeBase.ValueProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(method, fieldReference, source, RangeBase.SmallChangeProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(method, fieldReference, source, RangeBase.LargeChangeProperty);
            return fieldReference;
        }
    }
}
