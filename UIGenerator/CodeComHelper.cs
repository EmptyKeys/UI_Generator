using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using EmptyKeys.UserInterface.Designer;
using EmptyKeys.UserInterface.Designer.Interactions;

namespace EmptyKeys.UserInterface.Generator
{
    /// <summary>
    /// Code Com helper methods
    /// </summary>
    public static class CodeComHelper
    {
        private static readonly string ResourceReferenceExpressionTypeName = "ResourceReferenceExpression";
        private static readonly string ResourceKeyPropertyName = "ResourceKey";
        private static List<string> supportedAttachedProperties = new List<string>();
        private static List<string> ignoredProperties = new List<string>();

        /// <summary>
        /// Initializes the <see cref="CodeComHelper"/> class.
        /// </summary>
        static CodeComHelper()
        {
            supportedAttachedProperties.Add("IsSelected");
            supportedAttachedProperties.Add("ItemForItemContainer");
            supportedAttachedProperties.Add("Row");
            supportedAttachedProperties.Add("Column");
            supportedAttachedProperties.Add("RowSpan");
            supportedAttachedProperties.Add("ColumnSpan");
            supportedAttachedProperties.Add("TextEditor");
            supportedAttachedProperties.Add("CanContentScroll");
            supportedAttachedProperties.Add("HorizontalScrollBarVisibility");
            supportedAttachedProperties.Add("VerticalScrollBarVisibility");
            supportedAttachedProperties.Add("Sounds");
            supportedAttachedProperties.Add("Placement");
            supportedAttachedProperties.Add("ShowDuration");
            supportedAttachedProperties.Add("InitialShowDelay");
            supportedAttachedProperties.Add("Target");
            supportedAttachedProperties.Add("TargetName");
            supportedAttachedProperties.Add("TargetProperty");
            supportedAttachedProperties.Add("FrameWidth");
            supportedAttachedProperties.Add("FrameHeight");
            supportedAttachedProperties.Add("FramesPerSecond");
            supportedAttachedProperties.Add("Animate");
            supportedAttachedProperties.Add("Left");
            supportedAttachedProperties.Add("Right");
            supportedAttachedProperties.Add("Top");
            supportedAttachedProperties.Add("Bottom");
            supportedAttachedProperties.Add("Dock");
            supportedAttachedProperties.Add("Action");
            supportedAttachedProperties.Add("VerticalOffset");
            supportedAttachedProperties.Add("HorizontalOffset");
            supportedAttachedProperties.Add("IsDragSource");
            supportedAttachedProperties.Add("IsDropTarget");
            supportedAttachedProperties.Add("Command");
            supportedAttachedProperties.Add("CommandPath");
            supportedAttachedProperties.Add("CommandParameter");
            supportedAttachedProperties.Add("PanningMode");
            supportedAttachedProperties.Add("PanningRatio");
            supportedAttachedProperties.Add("PanningDeceleration");
            supportedAttachedProperties.Add("IsMouseWheelEnabled");
            supportedAttachedProperties.Add("SortingCommand");
            supportedAttachedProperties.Add("IsSoundEnabled");

            ignoredProperties.Add("NameScope");
            ignoredProperties.Add("BaseUri");
            ignoredProperties.Add("XmlnsDictionary");
            ignoredProperties.Add("XmlNamespaceMaps");
            ignoredProperties.Add("IsSelectionActive");
            ignoredProperties.Add("XmlSpace");
            ignoredProperties.Add("DataType");
            ignoredProperties.Add("Mode");
        }

        /// <summary>
        /// Generates the enum field.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        public static void GenerateEnumField<T>(CodeMemberMethod method, CodeExpression target, DependencyObject source, DependencyProperty property)
        {
            if (IsValidForFieldGenerator(source.ReadLocalValue(property)))
            {
                T value = (T)source.GetValue(property);
                GenerateEnumField(method, target, property.Name, typeof(T).Name, value.ToString());
            }
        }

        /// <summary>
        /// Generates the enum field.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="typeName">Name of the type.</param>
        /// <param name="value">The value.</param>
        public static void GenerateEnumField(CodeMemberMethod method, CodeExpression target, string fieldName, string typeName, string value)
        {
            CodeFieldReferenceExpression fieldReference = new CodeFieldReferenceExpression(target, fieldName);
            CodeTypeReferenceExpression typeReference = new CodeTypeReferenceExpression(typeName);
            method.Statements.Add(new CodeAssignStatement(fieldReference, new CodeFieldReferenceExpression(typeReference, value)));
        }

        /// <summary>
        /// Generates the flag enum field.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        public static void GenerateFlagEnumField<T>(CodeMemberMethod method, CodeExpression target, DependencyObject source, DependencyProperty property)
        {
            if (IsValidForFieldGenerator(source.ReadLocalValue(property)))
            {
                T value = (T)source.GetValue(property);
                CodeFieldReferenceExpression fieldReference = new CodeFieldReferenceExpression(target, property.Name);
                CodeCastExpression castExpression = new CodeCastExpression(typeof(T), new CodePrimitiveExpression(Convert.ToInt32(value)));
                method.Statements.Add(new CodeAssignStatement(fieldReference, castExpression));
            }
        }

        /// <summary>
        /// Generates the field.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        public static void GenerateField<T>(CodeMemberMethod method, CodeExpression target, DependencyObject source, DependencyProperty property)
        {
            if (IsValidForFieldGenerator(source.ReadLocalValue(property)))
            {
                T value = (T)source.GetValue(property);
                method.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(target, property.Name), new CodePrimitiveExpression(value)));
            }
        }

        /// <summary>
        /// Generates the field double to float.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        public static void GenerateFieldDoubleToFloat(CodeMemberMethod method, CodeExpression target, DependencyObject source, DependencyProperty property)
        {
            if (IsValidForFieldGenerator(source.ReadLocalValue(property)))
            {
                double value = (double)source.GetValue(property);
                method.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(target, property.Name), new CodePrimitiveExpression((float)value)));
            }
        }

        /// <summary>
        /// Generates the field.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="value">The value.</param>
        public static void GenerateFieldNonGeneric(CodeMemberMethod method, CodeExpression target, string fieldName, object value)
        {
            method.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(target, fieldName), new CodePrimitiveExpression(value)));
        }

        /// <summary>
        /// Generates the grid length field.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        public static void GenerateGridLengthField(CodeMemberMethod method, CodeExpression target, DependencyObject source, DependencyProperty property)
        {
            if (IsValidForFieldGenerator(source.ReadLocalValue(property)))
            {
                GridLength length = (GridLength)source.GetValue(property);
                CodeObjectCreateExpression lengthExpr = new CodeObjectCreateExpression(
                    "GridLength",
                    new CodePrimitiveExpression((float)length.Value),
                    new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("GridUnitType"), length.GridUnitType.ToString()));
                method.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(target, property.Name), lengthExpr));
            }
        }

        /// <summary>
        /// Generates the tool tip field.
        /// </summary>
        /// <param name="parentClass">The parent class.</param>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        public static void GenerateToolTipField(CodeTypeDeclaration parentClass, CodeMemberMethod method, CodeExpression target, DependencyObject source, DependencyProperty property)
        {
            if (IsValidForFieldGenerator(source.ReadLocalValue(property)))
            {
                object toolTipValue = source.GetValue(property);
                ToolTip toolTip = toolTipValue as ToolTip;
                object content = toolTipValue;
                if (toolTip != null)
                {
                    content = toolTip.Content;
                }

                FrameworkElement elem = source as FrameworkElement;
                string variableName = "tt_" + elem.Name;
                CodeVariableDeclarationStatement variable = new CodeVariableDeclarationStatement(
                    "ToolTip", variableName, new CodeObjectCreateExpression("ToolTip"));
                method.Statements.Add(variable);
                var variableExpr = new CodeVariableReferenceExpression(variableName);
                method.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(target, property.Name), variableExpr));

                if (toolTip != null)
                {
                    CodeComHelper.GenerateEnumField<PlacementMode>(method, variableExpr, toolTip, ToolTip.PlacementProperty);
                }

                if (content != null)
                {
                    CodeExpression contentExpr = null;
                    Type contentType = content.GetType();
                    if (contentType.IsPrimitive || content is string)
                    {
                        contentExpr = new CodePrimitiveExpression(content);
                    }
                    else if (contentType.IsSubclassOf(typeof(FrameworkElement)))
                    {
                        CodeMemberMethod initTemplateMethod = new CodeMemberMethod();
                        initTemplateMethod.Attributes = MemberAttributes.Static | MemberAttributes.Private;
                        initTemplateMethod.ReturnType = new CodeTypeReference("UIElement");
                        initTemplateMethod.Name = variableName + "_Method";

                        TypeGenerator generator = new TypeGenerator();
                        contentExpr = generator.ProcessGenerators(content, parentClass, initTemplateMethod, false);

                        if (initTemplateMethod.Statements.Count < 1)
                        {
                            contentExpr = new CodePrimitiveExpression(string.Empty);
                        }
                        else
                        {
                            CodeVariableDeclarationStatement firstStatement = initTemplateMethod.Statements[1] as CodeVariableDeclarationStatement;
                            CodeMethodReturnStatement returnInitTemplateMethod = new CodeMethodReturnStatement(new CodeVariableReferenceExpression(firstStatement.Name));
                            initTemplateMethod.Statements.Add(returnInitTemplateMethod);
                            parentClass.Members.Add(initTemplateMethod);

                            contentExpr = new CodeMethodInvokeExpression(null, initTemplateMethod.Name);
                        }
                    }

                    method.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(variableExpr, "Content"), contentExpr));
                }
            }
        }

        /// <summary>
        /// Generates the thickness field.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        public static void GenerateThicknessField(CodeMemberMethod method, CodeExpression target, DependencyObject source, DependencyProperty property)
        {
            if (IsValidForFieldGenerator(source.ReadLocalValue(property)))
            {
                Thickness thickness = (Thickness)source.GetValue(property);
                CodeObjectCreateExpression thicknessExpr = new CodeObjectCreateExpression(
                        "Thickness",
                        new CodePrimitiveExpression((float)thickness.Left),
                        new CodePrimitiveExpression((float)thickness.Top),
                        new CodePrimitiveExpression((float)thickness.Right),
                        new CodePrimitiveExpression((float)thickness.Bottom));

                method.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(target, property.Name), thicknessExpr));
            }
        }

        /// <summary>
        /// Generates the font family field.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        public static void GenerateFontFamilyField(CodeMemberMethod method, CodeExpression target, DependencyObject source, DependencyProperty property)
        {
            if (IsValidForFieldGenerator(source.ReadLocalValue(property)))
            {
                FontFamily family = (FontFamily)source.GetValue(property);
                CodeObjectCreateExpression familyExpr = GenerateFontFamilyExpression(family);
                method.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(target, property.Name), familyExpr));
            }
        }

        /// <summary>
        /// Generates the font family expression.
        /// </summary>
        /// <param name="family">The family.</param>
        /// <returns></returns>
        public static CodeObjectCreateExpression GenerateFontFamilyExpression(FontFamily family)
        {
            CodeObjectCreateExpression familyExpr = new CodeObjectCreateExpression("FontFamily", new CodePrimitiveExpression(family.Source));
            return familyExpr;
        }

        /// <summary>
        /// Generates the font style field.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        /// <param name="fontStyle">The font style.</param>
        /// <param name="fontWeight">The font weight.</param>
        public static void GenerateFontStyleField(CodeMemberMethod method, CodeExpression target, DependencyObject source, DependencyProperty fontStyle, DependencyProperty fontWeight)
        {
            if (IsValidForFieldGenerator(source.ReadLocalValue(fontStyle)) || IsValidForFieldGenerator(source.ReadLocalValue(fontWeight)))
            {
                FontStyle style = (FontStyle)source.GetValue(fontStyle);
                FontWeight weight = (FontWeight)source.GetValue(fontWeight);
                CodeExpression styleExpr = GenerateFontStyleExpression(style, weight);
                method.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(target, fontStyle.Name), styleExpr));
            }
        }

        /// <summary>
        /// Generates the font style expression.
        /// </summary>
        /// <param name="style">The style.</param>
        /// <param name="weight">The weight.</param>
        /// <returns></returns>
        public static CodeExpression GenerateFontStyleExpression(FontStyle style, FontWeight weight)
        {
            CodeExpression styleExpr = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("FontStyle"), "Regular");
            if (weight == FontWeights.Bold)
            {
                styleExpr = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("FontStyle"), "Bold");
            }

            if (style == FontStyles.Italic)
            {
                if (weight == FontWeights.Regular || weight == FontWeights.Normal)
                {
                    styleExpr = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("FontStyle"), "Italic");
                }
                if (weight == FontWeights.Bold)
                {
                    styleExpr = new CodeBinaryOperatorExpression(
                        new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("FontStyle"), "Italic"),
                        CodeBinaryOperatorType.BitwiseOr,
                        new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("FontStyle"), "Bold"));
                }
            }
            return styleExpr;
        }

        /// <summary>
        /// Generates the brush field.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        public static void GenerateBrushField(CodeMemberMethod method, CodeExpression target, DependencyObject source, DependencyProperty property)
        {
            if (IsValidForFieldGenerator(source.ReadLocalValue(property)))
            {
                Brush brush = (Brush)source.GetValue(property);
                FrameworkElement elem = source as FrameworkElement;
                string brushVarName = string.Format("{0}_{1}", elem.Name, property.Name);
                CodeExpression brushExpr = GenerateBrushInstance(method, brush, brushVarName);
                if (brushExpr != null)
                {
                    method.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(target, property.Name), brushExpr));

                    if (brush.Opacity != (double)Brush.OpacityProperty.DefaultMetadata.DefaultValue)
                    {
                        GenerateFieldNonGeneric(method, target, property.Name + ".Opacity", (float)brush.Opacity);
                    }
                }
            }
        }

        /// <summary>
        /// Generates the brush instance.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="variableName">Name of the variable.</param>
        /// <returns></returns>
        public static CodeExpression GenerateBrushInstance(CodeMemberMethod method, Brush brush, string variableName)
        {
            string brushTypeName = brush.GetType().Name;
            CodeExpression brushExpr = null;
            SolidColorBrush solid = brush as SolidColorBrush;
            if (solid != null)
            {
                CodeObjectCreateExpression colorExpr = new CodeObjectCreateExpression(
                    "ColorW",
                    new CodePrimitiveExpression(solid.Color.R),
                    new CodePrimitiveExpression(solid.Color.G),
                    new CodePrimitiveExpression(solid.Color.B),
                    new CodePrimitiveExpression(solid.Color.A));
                brushExpr = new CodeObjectCreateExpression(brushTypeName, colorExpr);
                return brushExpr;
            }

            LinearGradientBrush linear = brush as LinearGradientBrush;
            if (linear != null)
            {
                CodeVariableDeclarationStatement variable = new CodeVariableDeclarationStatement(
                    "LinearGradientBrush", variableName,
                    new CodeObjectCreateExpression(brushTypeName));
                method.Statements.Add(variable);
                brushExpr = new CodeVariableReferenceExpression(variableName);

                method.Statements.Add(new CodeAssignStatement(
                    new CodeFieldReferenceExpression(brushExpr, "StartPoint"),
                    new CodeObjectCreateExpression("PointF",
                        new CodePrimitiveExpression((float)linear.StartPoint.X),
                        new CodePrimitiveExpression((float)linear.StartPoint.Y))));

                method.Statements.Add(new CodeAssignStatement(
                    new CodeFieldReferenceExpression(brushExpr, "EndPoint"),
                    new CodeObjectCreateExpression("PointF",
                        new CodePrimitiveExpression((float)linear.EndPoint.X),
                        new CodePrimitiveExpression((float)linear.EndPoint.Y))));

                var spread = linear.SpreadMethod;
                CodeTypeReferenceExpression enumType = new CodeTypeReferenceExpression(spread.GetType().Name);
                method.Statements.Add(new CodeAssignStatement(
                    new CodeFieldReferenceExpression(brushExpr, "SpreadMethod"),
                    new CodeFieldReferenceExpression(enumType, spread.ToString())));

                foreach (var stop in linear.GradientStops)
                {
                    CodeMethodInvokeExpression gradientStop = new CodeMethodInvokeExpression(
                            brushExpr, "GradientStops.Add",
                            new CodeObjectCreateExpression("GradientStop",
                                new CodeObjectCreateExpression("ColorW",
                                    new CodePrimitiveExpression(stop.Color.R),
                                    new CodePrimitiveExpression(stop.Color.G),
                                    new CodePrimitiveExpression(stop.Color.B),
                                    new CodePrimitiveExpression(stop.Color.A)),
                                new CodePrimitiveExpression((float)stop.Offset)));
                    method.Statements.Add(gradientStop);
                }

                return brushExpr;
            }

            ImageBrush image = brush as ImageBrush;
            if (image != null)
            {
                brushExpr = GenerateImageBrush(method, variableName, brushTypeName, brushExpr, image);
                return brushExpr;
            }

            GenerateError(method, "Brush type not supported - " + brush.GetType());

            return brushExpr;
        }

        private static CodeExpression GenerateImageBrush(CodeMemberMethod method, string variableName, string brushTypeName, CodeExpression brushExpr, ImageBrush image)
        {
            CodeVariableDeclarationStatement variable = new CodeVariableDeclarationStatement(
                "ImageBrush", variableName,
                new CodeObjectCreateExpression(brushTypeName));
            method.Statements.Add(variable);
            brushExpr = new CodeVariableReferenceExpression(variableName);

            BitmapImage bitmap = image.ImageSource as BitmapImage;
            if (bitmap != null)
            {
                GenerateBitmapImageField(method, brushExpr, bitmap.UriSource, variableName + "_bm", "ImageSource");
            }

            if (BindingOperations.IsDataBound(image, ImageBrush.ImageSourceProperty))
            {
                CodeComHelper.GenerateBindings(method, brushExpr, image, variableName);
            }

            GenerateEnumField<Stretch>(method, brushExpr, image, ImageBrush.StretchProperty);
            GenerateEnumField<BrushMappingMode>(method, brushExpr, image, ImageBrush.ViewboxUnitsProperty);
            GenerateFieldDoubleToFloat(method, brushExpr, image, Brush.OpacityProperty);
            GenerateRect(method, brushExpr, image, ImageBrush.ViewboxProperty);

            GenerateAttachedProperties(method, brushExpr, image);

            return brushExpr;
        }

        private static void GenerateRect(CodeMemberMethod method, CodeExpression brushExpr, DependencyObject source, DependencyProperty property)
        {
            if (IsValidForFieldGenerator(source.ReadLocalValue(property)))
            {
                Rect value = (Rect)source.GetValue(property);

                method.Statements.Add(new CodeAssignStatement(
                    new CodeFieldReferenceExpression(brushExpr, "Viewbox"),
                    new CodeObjectCreateExpression("Rect",
                        new CodePrimitiveExpression((float)value.X),
                        new CodePrimitiveExpression((float)value.Y),
                        new CodePrimitiveExpression((float)value.Width),
                        new CodePrimitiveExpression((float)value.Height)
                        )));
            }
        }

        /// <summary>
        /// Generates the bitmap image.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="fieldReference">The field reference.</param>
        /// <param name="uriSource">The URI source.</param>
        /// <param name="variableName">Name of the variable.</param>
        /// <param name="sourceProperty">The asset property.</param>
        public static void GenerateBitmapImageField(CodeMemberMethod method, CodeExpression fieldReference, Uri uriSource, string variableName, string sourceProperty)
        {
            GenerateBitmapImageValue(method, uriSource, variableName);

            method.Statements.Add(new CodeAssignStatement(
                new CodeFieldReferenceExpression(fieldReference, sourceProperty),
                new CodeVariableReferenceExpression(variableName)));
        }

        /// <summary>
        /// Generates the bitmap image value.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="uriSource">The URI source.</param>
        /// <param name="variableName">Name of the variable.</param>
        /// <returns></returns>
        public static CodeExpression GenerateBitmapImageValue(CodeMemberMethod method, Uri uriSource, string variableName)
        {
            string imageAsset;
            if (uriSource.IsAbsoluteUri)
            {
                imageAsset = uriSource.LocalPath;
            }
            else
            {
                imageAsset = uriSource.OriginalString;
            }

            string extension = Path.GetExtension(imageAsset);
            imageAsset = imageAsset.Replace(extension, string.Empty).TrimStart(Path.DirectorySeparatorChar, '/');

            CodeVariableDeclarationStatement bitmapVariable = new CodeVariableDeclarationStatement(
                    "BitmapImage", variableName,
                    new CodeObjectCreateExpression("BitmapImage"));
            method.Statements.Add(bitmapVariable);

            method.Statements.Add(new CodeAssignStatement(
                new CodeFieldReferenceExpression(new CodeVariableReferenceExpression(variableName), "TextureAsset"),
                new CodePrimitiveExpression(imageAsset)));

            ImageAssets.Instance.AddImage(imageAsset, extension);

            return new CodeVariableReferenceExpression(variableName);
        }

        /// <summary>
        /// Generates the error.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="errorText">The error text.</param>
        public static void GenerateError(CodeMemberMethod method, string errorText)
        {
            CodeSnippetStatement error = new CodeSnippetStatement("#error " + errorText);
            method.Statements.Add(error);
        }

        /// <summary>
        /// Generates the brush to color field.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        public static void GenerateBrushToColorField(CodeMemberMethod method, CodeExpression target, DependencyObject source, DependencyProperty property)
        {
            if (IsValidForFieldGenerator(source.ReadLocalValue(property)))
            {
                Brush brush = (Brush)source.GetValue(property);
                string brushTypeName = brush.GetType().Name;
                CodeObjectCreateExpression colorExpr = null;
                SolidColorBrush solid = brush as SolidColorBrush;
                if (solid != null)
                {
                    colorExpr = new CodeObjectCreateExpression(
                        "ColorW",
                        new CodePrimitiveExpression(solid.Color.R),
                        new CodePrimitiveExpression(solid.Color.G),
                        new CodePrimitiveExpression(solid.Color.B),
                        new CodePrimitiveExpression(solid.Color.A));
                }
                else
                {
                    string errorText = "Only Solid Color Brush is supported for Foreground.";
                    Console.WriteLine(errorText);
                    CodeSnippetStatement error = new CodeSnippetStatement("#error " + errorText);
                    method.Statements.Add(error);
                }

                if (colorExpr != null)
                {
                    method.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(target, property.Name), colorExpr));
                }
            }
        }

        /// <summary>
        /// Determines whether [is valid for field generator] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool IsValidForFieldGenerator(object value)
        {
            if (value != DependencyProperty.UnsetValue &&
                !IsResourceReferenceExpression(value) &&
                !(value is Expression))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Generates the bindings.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        /// <param name="sourceName">Name of the source.</param>
        /// <param name="bindingSource">The binding source.</param>
        /// <param name="setBindingSource">if set to <c>true</c> [set binding source].</param>
        public static void GenerateBindings(CodeMemberMethod method, CodeExpression target, DependencyObject source, string sourceName, CodeExpression bindingSource = null, bool setBindingSource = false)
        {
            LocalValueEnumerator enumerator = source.GetLocalValueEnumerator();
            while (enumerator.MoveNext())
            {
                LocalValueEntry entry = enumerator.Current;
                DependencyProperty property = entry.Property;
                if (BindingOperations.IsDataBound(source, property) || entry.Value is TemplateBindingExpression)
                {
                    BindingExpression commandBindingExpr = entry.Value as BindingExpression;
                    TemplateBindingExpression templateBinding = entry.Value as TemplateBindingExpression;
                    if (commandBindingExpr != null)
                    {
                        Binding binding = commandBindingExpr.ParentBinding;
                        string varName = string.Format("{0}_{1}_{2}", "binding", sourceName, property.Name);
                        GeneratedBindingsMode generatedMode = (GeneratedBindingsMode)source.GetValue(GeneratedBindings.ModeProperty);

                        CodeVariableReferenceExpression bindingVar = GenerateBinding(method, binding, varName, generatedMode);

                        if (setBindingSource && bindingSource != null)
                        {
                            var sourceStatement = new CodeAssignStatement(
                                new CodeFieldReferenceExpression(bindingVar, "Source"), new CodeFieldReferenceExpression(bindingSource, "DataContext"));
                            method.Statements.Add(sourceStatement);
                        }
                        else if (binding.Source != null)
                        {
                            Type type = binding.Source.GetType();
                            if (type.BaseType.Name.Contains("ViewModelLocatorBase"))
                            {
                                bindingSource = new CodeObjectCreateExpression(type, new CodePrimitiveExpression(false));
                                var sourceStatement = new CodeAssignStatement(
                                    new CodeFieldReferenceExpression(bindingVar, "Source"), bindingSource);
                                method.Statements.Add(sourceStatement);
                            }
                        }

                        CodeTypeReferenceExpression typeReference = new CodeTypeReferenceExpression(source.GetType().Name);
                        DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(property, source.GetType());
                        if (dpd != null && dpd.IsAttached)
                        {
                            string ownerName = property.OwnerType.Name;
                            typeReference = new CodeTypeReferenceExpression(ownerName);
                        }

                        if (source is EmptyKeys.UserInterface.Designer.Interactions.Action && bindingSource != null)
                        {
                            // actions needs source set to its parent behavior, so we can find attached object for binding
                            var sourceStatement = new CodeAssignStatement(
                            new CodeFieldReferenceExpression(bindingVar, "Source"), bindingSource);
                            method.Statements.Add(sourceStatement);
                        }

                        CodeMethodInvokeExpression setBinding = new CodeMethodInvokeExpression(
                            target, "SetBinding", new CodeFieldReferenceExpression(typeReference, property.Name + "Property"), bindingVar);
                        method.Statements.Add(setBinding);
                    }
                    else if (templateBinding != null)
                    {
                        CodeVariableReferenceExpression bindingVar = new CodeVariableReferenceExpression(string.Format("{0}_{1}_{2}", "binding", sourceName, property.Name));
                        CodeTypeReference bindingClassRef = new CodeTypeReference("Binding");
                        CodeVariableDeclarationStatement bindingDecl = new CodeVariableDeclarationStatement(bindingClassRef, bindingVar.VariableName);
                        bindingDecl.InitExpression = new CodeObjectCreateExpression("Binding", new CodePrimitiveExpression(templateBinding.TemplateBindingExtension.Property.Name));
                        method.Statements.Add(bindingDecl);

                        method.Statements.Add(new CodeAssignStatement(
                            new CodeFieldReferenceExpression(bindingVar, "Source"),
                            new CodeVariableReferenceExpression("parent")));

                        CodeTypeReferenceExpression typeReference = new CodeTypeReferenceExpression(source.GetType().Name);
                        CodeMethodInvokeExpression setBinding = new CodeMethodInvokeExpression(
                            target, "SetBinding", new CodeFieldReferenceExpression(typeReference, property.Name + "Property"), bindingVar);
                        method.Statements.Add(setBinding);
                    }
                    else
                    {
                        GenerateError(method, string.Format("Type {0} is not supported", entry.Value.GetType()));
                    }
                }
            }
        }

        /// <summary>
        /// Generates the binding.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="binding">The binding.</param>
        /// <param name="varName">Name of the variable.</param>
        /// <param name="generatedMode">The generated mode.</param>
        /// <returns></returns>
        public static CodeVariableReferenceExpression GenerateBinding(CodeMemberMethod method, Binding binding, string varName, GeneratedBindingsMode generatedMode)
        {
            PropertyPath propertyPath = binding.Path;
            CodeVariableReferenceExpression bindingVar = new CodeVariableReferenceExpression(varName);
            CodeTypeReference bindingClassRef = new CodeTypeReference("Binding");
            CodeVariableDeclarationStatement bindingDecl = new CodeVariableDeclarationStatement(bindingClassRef, bindingVar.VariableName);

            bool isGenerated = false;
            if (propertyPath != null)
            {
                string path = propertyPath.Path;
                if (propertyPath.PathParameters != null && propertyPath.PathParameters.Count > 0)
                {
                    path = CreatePathFromParameters(propertyPath);
                }

                bindingDecl.InitExpression = new CodeObjectCreateExpression("Binding", new CodePrimitiveExpression(path));
                if (BindingGenerator.Instance.IsEnabled && generatedMode != GeneratedBindingsMode.Reflection)
                {
                    if (generatedMode != GeneratedBindingsMode.Manual)
                    {
                        isGenerated = BindingGenerator.Instance.GenerateBindingPath(propertyPath, generatedMode);
                    }
                    else
                    {
                        isGenerated = true;
                    }
                }
            }
            else
            {
                bindingDecl.InitExpression = new CodeObjectCreateExpression("Binding");
            }

            method.Statements.Add(bindingDecl);

            if (isGenerated)
            {
                GenerateFieldNonGeneric(method, bindingVar, "UseGeneratedBindings", true);
            }

            if (binding.Mode != BindingMode.Default)
            {
                GenerateEnumField(method, bindingVar, "Mode", "BindingMode", binding.Mode.ToString());
            }

            if (binding.FallbackValue != DependencyProperty.UnsetValue)
            {
                object fallBackValue = binding.FallbackValue;
                if (fallBackValue.GetType().IsPrimitive || fallBackValue is string)
                {
                    GenerateFieldNonGeneric(method, bindingVar, "FallbackValue", fallBackValue);
                }
                else
                {
                    string warningText = "Only primitive types are supported for FallbackValue.";
                    Console.WriteLine(warningText);
                    CodeSnippetStatement warning = new CodeSnippetStatement("#warning " + warningText);
                    method.Statements.Add(warning);
                }
            }

            if (!string.IsNullOrEmpty(binding.StringFormat))
            {
                GenerateFieldNonGeneric(method, bindingVar, "StringFormat", binding.StringFormat);
            }

            return bindingVar;
        }

        private static string CreatePathFromParameters(PropertyPath propertyPath)
        {
            string path = propertyPath.Path;
            for (int i = 0; i < propertyPath.PathParameters.Count; i++)
            {
                PropertyInfo parameter = propertyPath.PathParameters[i] as PropertyInfo;
                if (parameter == null)
                {
                    continue;
                }
                
                path = path.Replace(string.Format("({0})", i), parameter.Name);
            }

            return path;
        }

        /// <summary>
        /// Generates the resource references.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        public static void GenerateResourceReferences(CodeMemberMethod method, CodeExpression target, DependencyObject source)
        {
            LocalValueEnumerator enumerator = source.GetLocalValueEnumerator();
            while (enumerator.MoveNext())
            {
                LocalValueEntry entry = enumerator.Current;
                DependencyProperty property = entry.Property;
                if (IsResourceReferenceExpression(entry.Value))
                {
                    Type valueType = entry.Value.GetType();
                    string name = valueType.Name;
                    if (name == ResourceReferenceExpressionTypeName)
                    {
                        PropertyInfo resourceKeyProperty = valueType.GetProperty(ResourceKeyPropertyName);
                        object resourceKey = resourceKeyProperty.GetValue(entry.Value);

                        CodeTypeReferenceExpression typeReference = new CodeTypeReferenceExpression(source.GetType().Name);
                        DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(property, source.GetType());
                        if (dpd != null && dpd.IsAttached)
                        {
                            typeReference = new CodeTypeReferenceExpression(property.OwnerType.Name);
                        }

                        CodeMethodInvokeExpression setResourceReference = new CodeMethodInvokeExpression(
                            target, "SetResourceReference",
                            new CodeFieldReferenceExpression(typeReference, property.Name + "Property"),
                            new CodePrimitiveExpression(resourceKey));

                        method.Statements.Add(setResourceReference);
                    }
                }
            }
        }

        private static bool IsResourceReferenceExpression(object value)
        {
            Expression exp = value as Expression;
            if (exp != null)
            {
                Type valueType = exp.GetType();
                string name = valueType.Name;
                if (name == ResourceReferenceExpressionTypeName)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Generates the attached properties.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        /// <param name="propertyParentTypeName">Name of the property parent type.</param>
        public static void GenerateAttachedProperties(CodeMemberMethod method, CodeExpression target, DependencyObject source, string propertyParentTypeName = null)
        {
            List<DependencyProperty> attachedProperties = GetAttachedProperties(source);
            foreach (var property in attachedProperties)
            {
                if (IsValidForFieldGenerator(source.ReadLocalValue(property)))
                {
                    if (supportedAttachedProperties.Contains(property.Name))
                    {
                        object value = source.GetValue(property);
                        CodeTypeReferenceExpression typeReference = new CodeTypeReferenceExpression(property.OwnerType.Name);
                        FrameworkElement elem = source as FrameworkElement;
                        SoundSourceCollection sounds = value as SoundSourceCollection;
                        if (sounds != null && sounds.Count > 0)
                        {
                            string collVar = elem.Name + "_sounds";
                            CodeVariableDeclarationStatement collection =
                                    new CodeVariableDeclarationStatement("var", collVar, new CodeMethodInvokeExpression(typeReference, "Get" + property.Name, target));
                            method.Statements.Add(collection);
                            GenerateSoundSources(method, sounds, collVar);
                        }
                        else if (value is PropertyPath)
                        {
                            var path = value as PropertyPath;

                            CodeMethodInvokeExpression setValue = null;
                            if (path.PathParameters.Count == 0)
                            {
                                setValue = new CodeMethodInvokeExpression(typeReference, "Set" + property.Name, target,
                                    new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(propertyParentTypeName), path.Path + "Property"));
                            }
                            else
                            {
                                DependencyProperty dependencyProperty = path.PathParameters[0] as DependencyProperty;
                                if (dependencyProperty != null)
                                {
                                    setValue = new CodeMethodInvokeExpression(typeReference, "Set" + property.Name, target,
                                        new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(dependencyProperty.OwnerType.Name), dependencyProperty.Name + "Property"));
                                }
                                else
                                {
                                    CodeComHelper.GenerateError(method, "Event Trigger property not resolved");
                                }
                            }

                            if (setValue != null)
                            {
                                method.Statements.Add(setValue);
                            }
                        }
                        else if (value.GetType().IsEnum)
                        {
                            CodeTypeReferenceExpression enumType = new CodeTypeReferenceExpression(value.GetType().Name);
                            CodeMethodInvokeExpression setValue = new CodeMethodInvokeExpression(
                                typeReference, "Set" + property.Name, target,
                                new CodeFieldReferenceExpression(enumType, value.ToString()));
                            method.Statements.Add(setValue);
                        }
                        else
                        {
                            if (value is double)
                            {
                                value = Convert.ToSingle(value);
                            }

                            CodeMethodInvokeExpression setValue = new CodeMethodInvokeExpression(
                                typeReference, "Set" + property.Name, target,
                                new CodePrimitiveExpression(value));
                            method.Statements.Add(setValue);
                        }
                    }
                    else if (!ignoredProperties.Contains(property.Name))
                    {
                        string warningText = property.Name + " attached property not supported.";
                        Console.WriteLine(warningText);
                        CodeSnippetStatement warning = new CodeSnippetStatement("#warning " + warningText);
                        method.Statements.Add(warning);
                    }
                }
            }

        }

        /// <summary>
        /// Generates the sound sources.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="sounds">The sounds.</param>
        /// <param name="collVar">The coll variable.</param>
        public static void GenerateSoundSources(CodeMemberMethod method, SoundSourceCollection sounds, string collVar)
        {
            foreach (var sound in sounds)
            {
                var soundSource = GenerateSoundSource(method, sound);
                CodeMethodInvokeExpression addSound = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression(collVar), "Add", soundSource);
                method.Statements.Add(addSound);
            }
        }

        /// <summary>
        /// Generates the sound source.
        /// </summary>
        /// <param name="method">The method.</param>        
        /// <param name="sound">The sound.</param>
        public static CodeSnippetExpression GenerateSoundSource(CodeMemberMethod method, SoundSource sound)
        {
            CodeSnippetExpression expression = new CodeSnippetExpression(
                string.Format("new SoundSource {{ SoundType = SoundType.{0}, SoundAsset = \"{1}\", Volume = {2}f }}", sound.SoundType, sound.SoundAsset, sound.Volume));            

            method.Statements.Add(new CodeMethodInvokeExpression(
                new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("SoundManager"), "Instance"),
                "AddSound",
                new CodePrimitiveExpression(sound.SoundAsset)));

            return expression;
        }

        private static List<DependencyProperty> GetAttachedProperties(DependencyObject obj)
        {
            List<DependencyProperty> result = new List<DependencyProperty>();

            foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(obj,
                new Attribute[] { new PropertyFilterAttribute(PropertyFilterOptions.All) }))
            {
                DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(pd);

                if (dpd != null && dpd.IsAttached)
                {
                    result.Add(dpd.DependencyProperty);
                }
            }

            return result;
        }

        /// <summary>
        /// Generates the template style field.
        /// </summary>
        /// <param name="parentClass">The parent class.</param>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        public static void GenerateTemplateStyleField(CodeTypeDeclaration parentClass, CodeMemberMethod method, CodeExpression target, DependencyObject source, DependencyProperty property)
        {
            GenerateTemplateStyleField(parentClass, method, target, source, property, ((FrameworkElement)source).Name);
        }

        /// <summary>
        /// Generates the template style field.
        /// </summary>
        /// <param name="parentClass">The parent class.</param>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        /// <param name="name">The name.</param>
        public static void GenerateTemplateStyleField(CodeTypeDeclaration parentClass, CodeMemberMethod method, CodeExpression target, DependencyObject source, DependencyProperty property, string name)
        {
            if (IsValidForFieldGenerator(source.ReadLocalValue(property)))
            {
                object value = source.GetValue(property);
                CodeExpression valueExpr = GetValueExpression(parentClass, method, value, name);
                method.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(target, property.Name), valueExpr));
            }
        }

        /// <summary>
        /// Gets the resource key expression.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <returns></returns>
        public static CodeExpression GetResourceKeyExpression(object resourceKey)
        {
            CodeExpression keyExpression = null;
            Type resourceKeyType = resourceKey.GetType();
            if (resourceKeyType.IsPrimitive || resourceKeyType == typeof(string))
            {
                keyExpression = new CodePrimitiveExpression(resourceKey);
            }
            else if (resourceKeyType == typeof(DataTemplateKey))
            {
                DataTemplateKey dataTemplateKey = resourceKey as DataTemplateKey;
                keyExpression = new CodeTypeOfExpression(((Type)dataTemplateKey.DataType).FullName);
            }
            else if (resourceKey is Type)
            {
                Type type = resourceKey as Type;
                keyExpression = new CodeTypeOfExpression(type.Name);
            }

            return keyExpression;
        }

        /// <summary>
        /// Gets the value expression.
        /// </summary>
        /// <param name="parentClass">The parent class.</param>
        /// <param name="method">The method.</param>
        /// <param name="value">The value.</param>
        /// <param name="baseName">Name of the base.</param>
        /// <param name="dictionary">The dictionary.</param>
        /// <returns></returns>
        public static CodeExpression GetValueExpression(CodeTypeDeclaration parentClass, CodeMemberMethod method, object value, string baseName, ResourceDictionary dictionary = null)
        {
            ValueGenerator generator = new ValueGenerator();
            CodeExpression valueExpression = generator.ProcessGenerators(parentClass, method, value, baseName, dictionary);
            return valueExpression;
        }

        /// <summary>
        /// Generates the template.
        /// </summary>
        /// <param name="classType">Type of the class.</param>
        /// <param name="initMethod">The initialize method.</param>
        /// <param name="content">The content.</param>
        /// <param name="templateVariableName">Name of the template variable.</param>
        /// <returns></returns>
        public static string GenerateTemplate(CodeTypeDeclaration classType, CodeMemberMethod initMethod, DependencyObject content, string templateVariableName)
        {
            CodeMemberMethod initTemplateMethod = new CodeMemberMethod();
            initTemplateMethod.Attributes = MemberAttributes.Static | MemberAttributes.Private;
            initTemplateMethod.ReturnType = new CodeTypeReference("UIElement");
            initTemplateMethod.Name = templateVariableName + "Method";
            initTemplateMethod.Parameters.Add(new CodeParameterDeclarationExpression("UIElement", "parent"));
            TypeGenerator generator = new TypeGenerator();
            generator.ProcessGenerators(content, classType, initTemplateMethod, false);
            if (initTemplateMethod.Statements.Count < 2)
            {
                return string.Empty;
            }

            classType.Members.Add(initTemplateMethod);
            CodeVariableDeclarationStatement firstStatement = initTemplateMethod.Statements[1] as CodeVariableDeclarationStatement;
            initTemplateMethod.Statements.Insert(2, new CodeAssignStatement(
                new CodeFieldReferenceExpression(
                    new CodeVariableReferenceExpression(firstStatement.Name), "Parent"), new CodeVariableReferenceExpression("parent")));

            string creator = templateVariableName + "Func";
            initMethod.Statements.Add(new CodeSnippetStatement(string.Format("            Func<UIElement, UIElement> {0} = {1};", creator, initTemplateMethod.Name)));

            CodeMethodReturnStatement returnInitTemplateMethod = new CodeMethodReturnStatement(new CodeVariableReferenceExpression(firstStatement.Name));
            initTemplateMethod.Statements.Add(returnInitTemplateMethod);
            return creator;
        }

        /// <summary>
        /// Generates the triggers.
        /// </summary>
        /// <param name="parentClass">The parent class.</param>
        /// <param name="method">The method.</param>
        /// <param name="variableName">Name of the variable.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="triggers">The triggers.</param>
        public static void GenerateTriggers(CodeTypeDeclaration parentClass, CodeMemberMethod method, string variableName, Type targetType, TriggerCollection triggers)
        {
            int triggerIndex = 0;
            foreach (var triggerBase in triggers)
            {
                Trigger trigger = triggerBase as Trigger;
                if (trigger != null)
                {
                    GenerateTrigger(parentClass, method, variableName, targetType, triggerIndex, trigger);
                    triggerIndex++;
                    continue;
                }

                System.Windows.EventTrigger eventTrigger = triggerBase as System.Windows.EventTrigger;
                if (eventTrigger != null)
                {
                    GenerateEventTrigger(parentClass, method, targetType.Name, null, variableName, triggerIndex, eventTrigger);
                    triggerIndex++;
                    continue;
                }

                string errorText = string.Format("Trigger type {0} not supported.", triggerBase.GetType().Name);
                Console.WriteLine(errorText);
                GenerateError(method, errorText);
            }
        }

        private static void GenerateTrigger(CodeTypeDeclaration parentClass, CodeMemberMethod method, string parentName, Type targetType, int triggerIndex, Trigger trigger)
        {
            string triggerVarName = parentName + "_T_" + triggerIndex;
            CodeVariableDeclarationStatement triggerVar =
                new CodeVariableDeclarationStatement("Trigger", triggerVarName, new CodeObjectCreateExpression("Trigger"));
            method.Statements.Add(triggerVar);

            CodeAssignStatement triggerProperty = new CodeAssignStatement(
                new CodeFieldReferenceExpression(new CodeVariableReferenceExpression(triggerVarName), "Property"),
                new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(targetType.Name), trigger.Property.Name + "Property"));
            method.Statements.Add(triggerProperty);

            CodeExpression triggerValueExpr = GetValueExpression(parentClass, method, trigger.Value, triggerVarName);

            CodeAssignStatement triggerValue = new CodeAssignStatement(
                new CodeFieldReferenceExpression(new CodeVariableReferenceExpression(triggerVarName), "Value"),
                triggerValueExpr);
            method.Statements.Add(triggerValue);

            int setterIndex = 0;
            foreach (var setterBase in trigger.Setters)
            {
                Setter setter = setterBase as Setter;
                if (setter != null)
                {
                    string setterVarName = triggerVarName + "_S_" + setterIndex;

                    GenerateSetter(parentClass, method, targetType, setter, setterVarName);

                    CodeMethodInvokeExpression addSetter = new CodeMethodInvokeExpression(
                        new CodeVariableReferenceExpression(triggerVarName), "Setters.Add", new CodeVariableReferenceExpression(setterVarName));
                    method.Statements.Add(addSetter);

                    setterIndex++;
                }
                else
                {
                    string errorText = string.Format("Setter type {0} not supported.", setterBase.GetType().Name);
                    Console.WriteLine(errorText);

                    CodeSnippetStatement error = new CodeSnippetStatement("#error " + errorText);
                    method.Statements.Add(error);
                }
            }

            CodeMethodInvokeExpression addTrigger = new CodeMethodInvokeExpression(
                        new CodeVariableReferenceExpression(parentName), "Triggers.Add", new CodeVariableReferenceExpression(triggerVarName));
            method.Statements.Add(addTrigger);
        }

        /// <summary>
        /// Generates the setter.
        /// </summary>
        /// <param name="parentClass">The parent class.</param>
        /// <param name="method">The method.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="setter">The setter.</param>
        /// <param name="setterVarName">Name of the setter variable.</param>
        public static void GenerateSetter(CodeTypeDeclaration parentClass, CodeMemberMethod method, Type targetType, Setter setter, string setterVarName)
        {
            object setterValue = setter.Value;
            if (setterValue is double)
            {
                setterValue = Convert.ToSingle(setterValue); // TODO maybe there is better solution for this
            }
            CodeExpression setterValueExpr = GetValueExpression(parentClass, method, setterValue, setterVarName);
            if (setterValueExpr != null)
            {
                if (targetType == null || !string.IsNullOrEmpty(setter.TargetName))
                {
                    targetType = setter.Property.OwnerType;
                    if (targetType == typeof(TextElement))
                    {
                        targetType = typeof(TextBlock);
                    }

                    if (targetType == typeof(FrameworkElement))
                    {
                        targetType = typeof(UIElement);
                    }
                }

                string propertyName = setter.Property.Name;
                if (setter.Property == Control.FontWeightProperty)
                {
                    // Empty Keys UI does not have FontWeight, only FontStyle
                    propertyName = "FontStyle";
                }

                CodeVariableDeclarationStatement setterVar =
                    new CodeVariableDeclarationStatement("Setter", setterVarName,
                        new CodeObjectCreateExpression(
                            "Setter",
                            new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(targetType.Name), propertyName + "Property"),
                            setterValueExpr
                            ));

                method.Statements.Add(setterVar);

                if (!string.IsNullOrEmpty(setter.TargetName))
                {
                    CodeAssignStatement setterTargetName = new CodeAssignStatement(
                        new CodeFieldReferenceExpression(new CodeVariableReferenceExpression(setterVarName), "TargetName"),
                        new CodePrimitiveExpression(setter.TargetName));
                    method.Statements.Add(setterTargetName);
                }
            }
            else
            {
                string errorText = "Setter value type unknown - " + setter.Value.GetType();
                Console.WriteLine(errorText);

                CodeSnippetStatement error = new CodeSnippetStatement("#error " + errorText);
                method.Statements.Add(error);
            }
        }

        /// <summary>
        /// Generates the event trigger.
        /// </summary>
        /// <param name="parentClass">The parent class.</param>
        /// <param name="method">The method.</param>
        /// <param name="typeName">Name of the type.</param>
        /// <param name="fieldReference">The field reference.</param>
        /// <param name="parentName">Name of the parent.</param>
        /// <param name="index">The index.</param>
        /// <param name="trigger">The trigger.</param>
        public static void GenerateEventTrigger(CodeTypeDeclaration parentClass, CodeMemberMethod method, string typeName, CodeExpression fieldReference, string parentName, int index, System.Windows.EventTrigger trigger)
        {
            string triggerVarName = parentName + "_ET_" + index;
            var routedEvent = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typeName), trigger.RoutedEvent.Name + "Event");

            if (fieldReference != null)
            {
                var triggerVar = new CodeVariableDeclarationStatement("EventTrigger", triggerVarName, new CodeObjectCreateExpression("EventTrigger", routedEvent, fieldReference));
                method.Statements.Add(triggerVar);
            }
            else
            {
                var triggerVar = new CodeVariableDeclarationStatement("EventTrigger", triggerVarName, new CodeObjectCreateExpression("EventTrigger", routedEvent));
                method.Statements.Add(triggerVar);
            }

            var triggerVarRef = new CodeVariableReferenceExpression(triggerVarName);
            var addTrigger = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression(parentName), "Triggers.Add", triggerVarRef);
            method.Statements.Add(addTrigger);

            for (int j = 0; j < trigger.Actions.Count; j++)
            {
                BeginStoryboard beginStoryboard = trigger.Actions[j] as BeginStoryboard;
                if (beginStoryboard == null)
                {
                    continue;
                }

                string actionName = beginStoryboard.Name;
                if (string.IsNullOrEmpty(actionName))
                {
                    actionName = triggerVarName + "_AC_" + j;
                }

                var beginStoryboardVar = new CodeVariableDeclarationStatement("BeginStoryboard", actionName, new CodeObjectCreateExpression("BeginStoryboard"));
                method.Statements.Add(beginStoryboardVar);

                var nameAssign = new CodeAssignStatement(
                    new CodeFieldReferenceExpression(new CodeVariableReferenceExpression(actionName), "Name"),
                    new CodePrimitiveExpression(actionName));
                method.Statements.Add(nameAssign);

                var addAction = new CodeMethodInvokeExpression(
                    new CodeVariableReferenceExpression(triggerVarName), "AddAction", new CodeVariableReferenceExpression(actionName));
                method.Statements.Add(addAction);

                if (beginStoryboard.Storyboard != null)
                {
                    GenerateStoryboard(parentClass, method, beginStoryboard.Storyboard, actionName, typeName);
                }
            }

            GenerateAttachedProperties(method, triggerVarRef, trigger, typeName);
            GenerateBindings(method, triggerVarRef, trigger, typeName);
        }

        private static void GenerateStoryboard(CodeTypeDeclaration classType, CodeMemberMethod method, Storyboard storyboard, string actionName, string typeName)
        {
            string storyboardName = storyboard.Name;
            if (string.IsNullOrEmpty(storyboardName))
            {
                storyboardName = actionName + "_SB";
            }

            var storyboardVar = new CodeVariableDeclarationStatement("Storyboard", storyboardName, new CodeObjectCreateExpression("Storyboard"));
            method.Statements.Add(storyboardVar);

            var storyboardAssign = new CodeAssignStatement(
                new CodeFieldReferenceExpression(new CodeVariableReferenceExpression(actionName), "Storyboard"),
                new CodeVariableReferenceExpression(storyboardName));
            method.Statements.Add(storyboardAssign);

            var nameAssign = new CodeAssignStatement(
                            new CodeFieldReferenceExpression(new CodeVariableReferenceExpression(storyboardName), "Name"),
                            new CodePrimitiveExpression(storyboardName));
            method.Statements.Add(nameAssign);

            for (int i = 0; i < storyboard.Children.Count; i++)
            {
                var timeline = storyboard.Children[i];
                string timelineName = timeline.Name;
                if (string.IsNullOrEmpty(timelineName))
                {
                    timelineName = storyboardName + "_TL_" + i;
                }

                CodeExpression timelineValueExpr = CodeComHelper.GetValueExpression(classType, method, timeline, timelineName);

                CodeComHelper.GenerateAttachedProperties(method, timelineValueExpr, timeline, typeName);

                var addTimeline = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression(storyboardName), "Children.Add", timelineValueExpr);
                method.Statements.Add(addTimeline);
            }
        }

        /// <summary>
        /// Generates the easing function.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="parentAnimation">The parent animation.</param>
        /// <param name="baseName">Name of the base.</param>
        /// <param name="easingFunc">The easing function.</param>
        public static void GenerateEasingFunction(CodeMemberMethod method, CodeExpression parentAnimation, string baseName, EasingFunctionBase easingFunc)
        {
            string easingName = baseName + "_EA";
            string easingType = easingFunc.GetType().Name;
            var easingVar = new CodeVariableDeclarationStatement(easingType, easingName, new CodeObjectCreateExpression(easingType));
            method.Statements.Add(easingVar);

            var easingVarRef = new CodeVariableReferenceExpression(easingName);
            var easingAssign = new CodeAssignStatement(new CodeFieldReferenceExpression(parentAnimation, "EasingFunction"), easingVarRef);
            method.Statements.Add(easingAssign);

            CodeComHelper.GenerateEnumField<EasingMode>(method, easingVarRef, easingFunc, EasingFunctionBase.EasingModeProperty);

            BackEase backEase = easingFunc as BackEase;
            if (backEase != null)
            {
                CodeComHelper.GenerateFieldDoubleToFloat(method, easingVarRef, backEase, BackEase.AmplitudeProperty);
                return;
            }

            ElasticEase elastic = easingFunc as ElasticEase;
            if (elastic != null)
            {
                CodeComHelper.GenerateField<int>(method, easingVarRef, elastic, ElasticEase.OscillationsProperty);
                CodeComHelper.GenerateFieldDoubleToFloat(method, easingVarRef, elastic, ElasticEase.SpringinessProperty);
                return;
            }

            ExponentialEase expo = easingFunc as ExponentialEase;
            if (expo != null)
            {
                CodeComHelper.GenerateFieldDoubleToFloat(method, easingVarRef, expo, ExponentialEase.ExponentProperty);
                return;
            }

            PowerEase power = easingFunc as PowerEase;
            if (power != null)
            {
                CodeComHelper.GenerateFieldDoubleToFloat(method, easingVarRef, power, PowerEase.PowerProperty);
            }
        }

        /// <summary>
        /// Generates the color field.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        public static void GenerateColorField(CodeMemberMethod method, CodeExpression target, DependencyObject source, DependencyProperty property)
        {
            if (IsValidForFieldGenerator(source.ReadLocalValue(property)))
            {
                Color value = (Color)source.GetValue(property);
                method.Statements.Add(new CodeAssignStatement(
                    new CodeFieldReferenceExpression(target, property.Name),
                    new CodeObjectCreateExpression("ColorW",
                        new CodePrimitiveExpression(value.R),
                        new CodePrimitiveExpression(value.G),
                        new CodePrimitiveExpression(value.B),
                        new CodePrimitiveExpression(value.A))
                    ));
            }
        }

        /// <summary>
        /// Generates the rectangle field.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        public static void GenerateRectangleField(CodeMemberMethod method, CodeExpression target, RectangleGeometry source, DependencyProperty property)
        {
            if (IsValidForFieldGenerator(source.ReadLocalValue(property)))
            {
                Rect value = (Rect)source.GetValue(property);
                method.Statements.Add(new CodeAssignStatement(
                    new CodeFieldReferenceExpression(target, property.Name),
                    new CodeObjectCreateExpression("Rect",
                        new CodePrimitiveExpression(value.X),
                        new CodePrimitiveExpression(value.Y),
                        new CodePrimitiveExpression(value.Width),
                        new CodePrimitiveExpression(value.Height))
                    ));
            }
        }

        /// <summary>
        /// Generates the point field.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        public static void GeneratePointField(CodeMemberMethod method, CodeExpression target, Geometry source, DependencyProperty property)
        {
            if (IsValidForFieldGenerator(source.ReadLocalValue(property)))
            {
                Point value = (Point)source.GetValue(property);
                method.Statements.Add(new CodeAssignStatement(
                    new CodeFieldReferenceExpression(target, property.Name),
                    new CodeObjectCreateExpression("PointF",
                        new CodePrimitiveExpression(value.X),
                        new CodePrimitiveExpression(value.Y))
                    ));
            }
        }

        /// <summary>
        /// Determines whether [is default value] [the specified source].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static bool IsDefaultValue(DependencyObject source, DependencyProperty property)
        {
            object value = source.GetValue(property);
            return value == DependencyProperty.UnsetValue || (value != null && value.Equals(property.DefaultMetadata.DefaultValue));
        }
    }
}
