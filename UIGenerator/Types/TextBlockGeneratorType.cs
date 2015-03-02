using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace EmptyKeys.UserInterface.Generator.Types
{
    /// <summary>
    /// Implements Text Block control generator
    /// </summary>
    public class TextBlockGeneratorType : ElementGeneratorType
    {
        /// <summary>
        /// Gets the type of the xaml.
        /// </summary>
        /// <value>
        /// The type of the xaml.
        /// </value>
        public override Type XamlType
        {
            get { return typeof(TextBlock); }
        }


        /// <summary>
        /// Generates code
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="method">The initialize method.</param>
        /// <param name="generateField">if set to <c>true</c> [generate field].</param>
        /// <returns></returns>
        public override CodeExpression Generate(DependencyObject source, CodeTypeDeclaration classType, CodeMemberMethod method, bool generateField)
        {
            CodeExpression fieldReference = base.Generate(source, classType, method, generateField);

            TextBlock textBlock = source as TextBlock;
            CodeComHelper.GenerateBrushField(method, fieldReference, source, TextBlock.BackgroundProperty);
            CodeComHelper.GenerateBrushField(method, fieldReference, source, TextBlock.ForegroundProperty);

            if (textBlock.Inlines.Count > 1)
            {
                StringBuilder text = new StringBuilder();
                foreach (var line in textBlock.Inlines)
                {
                    Run runLine = line as Run;
                    if (runLine != null)
                    {
                        text.AppendLine(runLine.Text);
                    }
                }

                CodeComHelper.GenerateField(method, fieldReference, "Text", text.ToString());
            }
            else
            {
                CodeComHelper.GenerateField<string>(method, fieldReference, source, TextBlock.TextProperty);
            }


            CodeComHelper.GenerateEnumField<TextAlignment>(method, fieldReference, source, TextBlock.TextAlignmentProperty);
            CodeComHelper.GenerateEnumField<TextWrapping>(method, fieldReference, source, TextBlock.TextWrappingProperty);

            FontGenerator.Instance.AddFont(textBlock.FontFamily, textBlock.FontSize, textBlock.FontStyle, textBlock.FontWeight, method);
            CodeComHelper.GenerateFontFamilyField(method, fieldReference, source, TextBlock.FontFamilyProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(method, fieldReference, source, TextBlock.FontSizeProperty);
            CodeComHelper.GenerateFontStyleField(method, fieldReference, source, TextBlock.FontStyleProperty, TextBlock.FontWeightProperty);

            return fieldReference;
        }        
    }
}
