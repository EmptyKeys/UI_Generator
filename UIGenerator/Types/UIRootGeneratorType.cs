using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EmptyKeys.UserInterface.Designer;

namespace EmptyKeys.UserInterface.Generator.Types
{
    /// <summary>
    /// Implements UI Root control generator
    /// </summary>
    public class UIRootGeneratorType : ContentControlGeneratorType
    {
        /// <summary>
        /// Gets the type of the xaml.
        /// </summary>
        /// <value>
        /// The type of the xaml.
        /// </value>
        public override Type XamlType
        {
            get { return typeof(UIRoot); }
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
            CodeExpression fieldReference = new CodeThisReferenceExpression();

            CodeComHelper.GenerateBrushField(method, fieldReference, source, Control.BackgroundProperty);
            CodeComHelper.GenerateBrushField(method, fieldReference, source, Control.BorderBrushProperty);
            CodeComHelper.GenerateThicknessField(method, fieldReference, source, Control.BorderThicknessProperty);
            CodeComHelper.GenerateThicknessField(method, fieldReference, source, Control.PaddingProperty);
            CodeComHelper.GenerateBrushField(method, fieldReference, source, Control.ForegroundProperty);

            CodeComHelper.GenerateTemplateStyleField(classType, method, fieldReference, source, FrameworkElement.StyleProperty);

            Control control = source as Control;
            FontGenerator.Instance.AddFont(control.FontFamily, control.FontSize, control.FontStyle, control.FontWeight, method);
            CodeComHelper.GenerateFontFamilyField(method, fieldReference, source, Control.FontFamilyProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(method, fieldReference, source, Control.FontSizeProperty);
            CodeComHelper.GenerateFontStyleField(method, fieldReference, source, Control.FontStyleProperty, Control.FontWeightProperty);

            return fieldReference;
        }        
    }
}
