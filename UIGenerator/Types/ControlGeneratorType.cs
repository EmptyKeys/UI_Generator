using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EmptyKeys.UserInterface.Generator.Types
{
    /// <summary>
    /// Implements Control generator
    /// </summary>
    public class ControlGeneratorType : ElementGeneratorType
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
                return typeof(Control);
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

            Control control = source as Control;            
            CodeComHelper.GenerateBrushField(method, fieldReference, source, Control.BackgroundProperty);
            CodeComHelper.GenerateBrushField(method, fieldReference, source, Control.BorderBrushProperty);
            CodeComHelper.GenerateThicknessField(method, fieldReference, source, Control.BorderThicknessProperty);
            CodeComHelper.GenerateThicknessField(method, fieldReference, source, Control.PaddingProperty);
            CodeComHelper.GenerateBrushField(method, fieldReference, source, Control.ForegroundProperty);
            CodeComHelper.GenerateEnumField<HorizontalAlignment>(method, fieldReference, source, Control.HorizontalContentAlignmentProperty);
            CodeComHelper.GenerateEnumField<VerticalAlignment>(method, fieldReference, source, Control.VerticalContentAlignmentProperty);

            CodeComHelper.GenerateTemplateStyleField(classType, method, fieldReference, source, Control.TemplateProperty);

            FontGenerator.Instance.AddFont(control.FontFamily, control.FontSize, control.FontStyle, control.FontWeight, method);
            CodeComHelper.GenerateFontFamilyField(method, fieldReference, source, Control.FontFamilyProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(method, fieldReference, source, Control.FontSizeProperty);
            CodeComHelper.GenerateFontStyleField(method, fieldReference, source, Control.FontStyleProperty, Control.FontWeightProperty);

            return fieldReference;
        }
    }
}
