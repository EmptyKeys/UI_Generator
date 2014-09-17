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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EmptyKeys.UserInterface.Designer;

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

            ignoredProperties.Add("NameScope");
            ignoredProperties.Add("BaseUri");
            ignoredProperties.Add("XmlnsDictionary");
            ignoredProperties.Add("XmlNamespaceMaps");
            ignoredProperties.Add("IsSelectionActive");
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

        private static void GenerateEnumField(CodeMemberMethod method, CodeExpression target, string fieldName, string typeName, string value)
        {
            CodeFieldReferenceExpression fieldReference = new CodeFieldReferenceExpression(target, fieldName);
            CodeTypeReferenceExpression typeReference = new CodeTypeReferenceExpression(typeName);
            method.Statements.Add(new CodeAssignStatement(fieldReference, new CodeFieldReferenceExpression(typeReference, value)));
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

        private static void GenerateField(CodeMemberMethod method, CodeExpression target, string fieldName, object value)
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

                CodeExpression contentExpr = null;
                Type contentType = content.GetType();
                if (contentType.IsPrimitive || content is string)
                {
                    contentExpr = new CodePrimitiveExpression(content);
                }
                else if (contentType.IsSubclassOf(typeof(FrameworkElement)))
                {
                    CodeMemberMethod initTemplateMethod = new CodeMemberMethod();
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

                        contentExpr = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), initTemplateMethod.Name);
                    }
                }

                method.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(variableExpr, "Content"), contentExpr));

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
                        GenerateField(method, target, property.Name + ".Opacity", (float)brush.Opacity);
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
                    "Color",
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
                    new CodeObjectCreateExpression("Vector2",
                        new CodePrimitiveExpression((float)linear.StartPoint.X),
                        new CodePrimitiveExpression((float)linear.StartPoint.Y))));

                method.Statements.Add(new CodeAssignStatement(
                    new CodeFieldReferenceExpression(brushExpr, "EndPoint "),
                    new CodeObjectCreateExpression("Vector2",
                        new CodePrimitiveExpression((float)linear.EndPoint.X),
                        new CodePrimitiveExpression((float)linear.EndPoint.Y))));

                foreach (var stop in linear.GradientStops)
                {
                    CodeMethodInvokeExpression gradientStop = new CodeMethodInvokeExpression(
                            brushExpr, "GradientStops.Add",
                            new CodeObjectCreateExpression("GradientStop",
                                new CodeObjectCreateExpression("Color",
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
                CodeVariableDeclarationStatement variable = new CodeVariableDeclarationStatement(
                    "ImageBrush", variableName,
                    new CodeObjectCreateExpression(brushTypeName));
                method.Statements.Add(variable);
                brushExpr = new CodeVariableReferenceExpression(variableName);

                BitmapImage bitmap = image.ImageSource as BitmapImage;
                if (bitmap != null)
                {
                    GenerateBitmapImageField(method, brushExpr, bitmap, variableName + "_bm", "ImageSource");
                }

                return brushExpr;
            }

            GenerateError(method, "Brush type not supported - " + brush.GetType());

            return brushExpr;
        }

        /// <summary>
        /// Generates the bitmap image.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="fieldReference">The field reference.</param>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="variableName">Name of the variable.</param>
        /// <param name="sourceProperty">The asset property.</param>
        public static void GenerateBitmapImageField(CodeMemberMethod method, CodeExpression fieldReference, BitmapImage bitmap, string variableName, string sourceProperty)
        {
            GenerateBitmapImageValue(method, bitmap, variableName);

            method.Statements.Add(new CodeAssignStatement(
                new CodeFieldReferenceExpression(fieldReference, sourceProperty),
                new CodeVariableReferenceExpression(variableName)));
        }

        /// <summary>
        /// Generates the bitmap image value.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="variableName">Name of the variable.</param>
        /// <returns></returns>
        public static CodeExpression GenerateBitmapImageValue(CodeMemberMethod method, BitmapImage bitmap, string variableName)
        {
            string imageAsset;
            if (bitmap.UriSource.IsAbsoluteUri)
            {
                imageAsset = bitmap.UriSource.LocalPath;
            }
            else
            {
                imageAsset = bitmap.UriSource.OriginalString;
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

            method.Statements.Add(new CodeMethodInvokeExpression(
                new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("ImageManager"), "Instance"),
                "AddImage",
                new CodePrimitiveExpression(imageAsset)));

            ImageAssets.Instance.AddImage(imageAsset + extension);

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
                        "Color",
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

        private static bool IsValidForFieldGenerator(object value)
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
        public static void GenerateBindings(CodeMemberMethod method, CodeExpression target, FrameworkElement source)
        {
            LocalValueEnumerator enumerator = source.GetLocalValueEnumerator();
            while (enumerator.MoveNext())
            {
                LocalValueEntry entry = enumerator.Current;
                DependencyProperty property = entry.Property;
                if (BindingOperations.IsDataBound(source, property) || entry.Value is TemplateBindingExpression)
                {
                    BindingExpression commandBindingExpr = entry.Value as BindingExpression;
                    if (commandBindingExpr != null)
                    {
                        PropertyPath path = commandBindingExpr.ParentBinding.Path;

                        CodeVariableReferenceExpression bindingVar = new CodeVariableReferenceExpression(string.Format("{0}_{1}_{2}", "binding", source.Name, property.Name));
                        CodeTypeReference bindingClassRef = new CodeTypeReference("Binding");
                        CodeVariableDeclarationStatement bindingDecl = new CodeVariableDeclarationStatement(bindingClassRef, bindingVar.VariableName);
                        bindingDecl.InitExpression = new CodeObjectCreateExpression("Binding", new CodePrimitiveExpression(path.Path));
                        method.Statements.Add(bindingDecl);

                        CodeTypeReferenceExpression typeReference = new CodeTypeReferenceExpression(source.GetType().Name);
                        CodeMethodInvokeExpression setBinding = new CodeMethodInvokeExpression(
                            target, "SetBinding", new CodeFieldReferenceExpression(typeReference, property.Name + "Property"), bindingVar);
                        method.Statements.Add(setBinding);

                        if (commandBindingExpr.ParentBinding.Mode != BindingMode.Default)
                        {
                            GenerateEnumField(method, bindingVar, "Mode", "BindingMode", commandBindingExpr.ParentBinding.Mode.ToString());
                        }                        
                    }

                    TemplateBindingExpression templateBinding = entry.Value as TemplateBindingExpression;
                    if (templateBinding != null)
                    {
                        CodeVariableReferenceExpression bindingVar = new CodeVariableReferenceExpression(string.Format("{0}_{1}_{2}", "binding", source.Name, property.Name));
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
                }
            }
        }

        /// <summary>
        /// Generates the resource references.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="source">The source.</param>
        public static void GenerateResourceReferences(CodeMemberMethod method, CodeExpression target, FrameworkElement source)
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
        public static void GenerateAttachedProperties(CodeMemberMethod method, CodeExpression target, DependencyObject source)
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
                        else
                        {
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
                CodeMethodInvokeExpression addSound = new CodeMethodInvokeExpression(
                    new CodeVariableReferenceExpression(collVar), "Add",
                    new CodeSnippetExpression(
                        string.Format("new SoundSource {{ SoundType = SoundType.{0}, SoundAsset = \"{1}\" }}", sound.SoundType, sound.SoundAsset)));
                method.Statements.Add(addSound);

                method.Statements.Add(new CodeMethodInvokeExpression(
                    new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("SoundManager"), "Instance"),
                    "AddSound",
                    new CodePrimitiveExpression(sound.SoundAsset)));
            }
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
            if (IsValidForFieldGenerator(source.ReadLocalValue(property)))
            {
                object value = source.GetValue(property);
                CodeExpression valueExpr = GetValueExpression(parentClass, method, value, ((FrameworkElement)source).Name);
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
                keyExpression = new CodeTypeOfExpression(((Type)dataTemplateKey.DataType).Name);
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
            initTemplateMethod.ReturnType = new CodeTypeReference("UIElement");
            initTemplateMethod.Name = templateVariableName + "Method";
            initTemplateMethod.Parameters.Add(new CodeParameterDeclarationExpression("UIElement", "parent"));
            TypeGenerator generator = new TypeGenerator();
            generator.ProcessGenerators(content, classType, initTemplateMethod, false);

            if (initTemplateMethod.Statements.Count < 1)
            {
                return string.Empty;
            }

            classType.Members.Add(initTemplateMethod);

            CodeVariableDeclarationStatement firstStatement = initTemplateMethod.Statements[1] as CodeVariableDeclarationStatement;
            initTemplateMethod.Statements.Insert(2, new CodeAssignStatement(
                new CodeFieldReferenceExpression(
                    new CodeVariableReferenceExpression(firstStatement.Name), "Parent"), new CodeVariableReferenceExpression("parent")));

            string creator = templateVariableName + "Func";
            initMethod.Statements.Add(new CodeSnippetStatement("            Func<UIElement, UIElement> " + creator + " = (parent) => {"));

            CodeMethodReturnStatement returnStatement = new CodeMethodReturnStatement(
                new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), initTemplateMethod.Name, new CodeVariableReferenceExpression("parent")));
            initMethod.Statements.Add(returnStatement);

            initMethod.Statements.Add(new CodeSnippetStatement("            };"));

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
                }
                else
                {
                    string errorText = string.Format("Trigger type {0} not supported.", triggerBase.GetType().Name);
                    Console.WriteLine(errorText);

                    CodeSnippetStatement error = new CodeSnippetStatement("#error " + errorText);
                    method.Statements.Add(error);
                }
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
                if (targetType == null)
                {
                    targetType = setter.Property.OwnerType;
                }

                CodeVariableDeclarationStatement setterVar =
                    new CodeVariableDeclarationStatement("Setter", setterVarName,
                        new CodeObjectCreateExpression(
                            "Setter",
                            new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(targetType.Name), setter.Property.Name + "Property"),
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
    }
}
