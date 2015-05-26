using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace EmptyKeys.UserInterface.Generator.Types
{
    /// <summary>
    /// Implements the Label control generator
    /// </summary>
    public class LabelGeneratorType : ElementGeneratorType
    {
        /// <summary>
        /// The XamlType for labels
        /// </summary>
        public override Type XamlType
        {
            get
            {
                return typeof(Label);
            }
        }

        /// <summary>
        /// Generates the code for the label
        /// </summary>
        /// <param name="source">The source object</param>
        /// <param name="classType">The class</param>
        /// <param name="method">The member method</param>
        /// <param name="generateField">Whether to generate a field</param>
        /// <returns></returns>
        public override System.CodeDom.CodeExpression Generate(System.Windows.DependencyObject source, System.CodeDom.CodeTypeDeclaration classType, System.CodeDom.CodeMemberMethod method, bool generateField)
        {
            var textBlock = CreateTextBlockFromLabel(source as Label);
            CodeExpression fieldReference = base.Generate(textBlock, classType, method, generateField);

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

            CodeComHelper.GenerateThicknessField(method, fieldReference, source, TextBlock.PaddingProperty);
            CodeComHelper.GenerateEnumField<TextAlignment>(method, fieldReference, source, TextBlock.TextAlignmentProperty);
            CodeComHelper.GenerateEnumField<TextWrapping>(method, fieldReference, source, TextBlock.TextWrappingProperty);

            FontGenerator.Instance.AddFont(textBlock.FontFamily, textBlock.FontSize, textBlock.FontStyle, textBlock.FontWeight, method);
            CodeComHelper.GenerateFontFamilyField(method, fieldReference, source, TextBlock.FontFamilyProperty);
            CodeComHelper.GenerateFieldDoubleToFloat(method, fieldReference, source, TextBlock.FontSizeProperty);
            CodeComHelper.GenerateFontStyleField(method, fieldReference, source, TextBlock.FontStyleProperty, TextBlock.FontWeightProperty);

            return fieldReference;
        }

        private TextBlock CreateTextBlockFromLabel(Label label)
        {
            var block = new TextBlock();

            block.Name = label.Name;
            block.Height = label.Height;
            block.MaxHeight = label.MaxHeight;
            block.MinHeight = label.MinHeight;
            block.Width = label.Width;
            block.MaxWidth = label.MaxWidth;
            block.MinWidth = label.MinWidth;
            block.Tag = label.Tag;
            block.IsEnabled = label.IsEnabled;
            block.IsHitTestVisible = label.IsHitTestVisible;
            block.SnapsToDevicePixels = label.SnapsToDevicePixels;
            block.Focusable = label.Focusable;
            block.Visibility = label.Visibility;
            block.Margin = label.Margin;
            block.HorizontalAlignment = label.HorizontalAlignment;
            block.VerticalAlignment = label.VerticalAlignment;
            block.ToolTip = label.ToolTip;
            block.Opacity = label.Opacity;

            block.Foreground = label.Foreground;
            block.Background = label.Background;

            block.Text = label.Content.ToString();

            block.Padding = label.Padding;
            block.TextAlignment = GetAlignment(label.HorizontalContentAlignment);

            block.FontFamily = label.FontFamily;
            block.FontSize = label.FontSize;
            block.FontStyle = label.FontStyle;
            block.FontWeight = label.FontWeight;

            return block;
        }

        private TextAlignment GetAlignment(HorizontalAlignment alignment)
        {
            switch (alignment)
            {
                case HorizontalAlignment.Left:
                    return TextAlignment.Left;
                case HorizontalAlignment.Center:
                    return TextAlignment.Center;
                case HorizontalAlignment.Right:
                    return TextAlignment.Right;
                case HorizontalAlignment.Stretch:
                    return TextAlignment.Justify;
                default:
                    return TextAlignment.Left;
            }
        }
    }
}
