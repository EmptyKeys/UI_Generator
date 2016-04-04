using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;

namespace EmptyKeys.UserInterface.Generator
{
    /// <summary>
    /// Implements Font and Font asset generator
    /// </summary>
    public sealed class FontGenerator
    {
        private static FontGenerator singleton = new FontGenerator();

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static FontGenerator Instance
        {
            get
            {
                return singleton;
            }
        }

        private Dictionary<int, FontInfo> fonts = new Dictionary<int, FontInfo>();

        /// <summary>
        /// Prevents a default instance of the <see cref="FontGenerator"/> class from being created.
        /// </summary>
        private FontGenerator()
        {
        }

        /// <summary>
        /// Adds the font.
        /// </summary>
        /// <param name="family">The family.</param>
        /// <param name="size">The size.</param>
        /// <param name="style">The style.</param>
        /// <param name="weight">The weight.</param>
        /// <param name="method">The method.</param>
        public void AddFont(FontFamily family, double size, FontStyle style, FontWeight weight, CodeMemberMethod method)
        {
            FontInfo info = new FontInfo(family, size, style, weight);
            fonts[info.GetHashCode()] = info;            
        }

        /// <summary>
        /// Generates the manager code.
        /// </summary>
        /// <param name="method">The method.</param>
        public void GenerateManagerCode(CodeMemberMethod method)
        {
            foreach (var info in fonts.Values)
            {
                string fontName = GetFontName(info);
                float fontSize = GetFontSize(info);
                string fontStyle = GetFontStyle(info);
                string assetName = GetFontAssetName(fontName, fontSize, fontStyle);

                CodeExpression styleExpr = CodeComHelper.GenerateFontStyleExpression(info.FontStyle, info.FontWeight);

                method.Statements.Add(new CodeMethodInvokeExpression(
                    new CodeTypeReferenceExpression("FontManager"), "Instance.AddFont",
                        new CodePrimitiveExpression(fontName), new CodePrimitiveExpression((float)info.FontSize), styleExpr, new CodePrimitiveExpression(assetName)));
            }
        }

        /// <summary>
        /// Generates the font assets.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="renderMode">The render mode.</param>
        public void GenerateFontAssets(string path, RenderMode renderMode)
        {
            if (fonts.Count == 0)
            {
                return;
            }

            Console.WriteLine("Generating Fonts...");
            string templatePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SpriteFontTemplate.xml");
            if (renderMode == RenderMode.Xenko)
            {
                templatePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SpriteFontTemplate.xkfnt");
            }

            string template = File.OpenText(templatePath).ReadToEnd();
            foreach (var info in fonts.Values)
            {
                string fontName = GetFontName(info);
                float fontSize = GetFontSize(info);
                string fontStyle = GetFontStyle(info);

                string assetName = GetFontAssetName(fontName, fontSize, fontStyle);                

                string fileContent = string.Empty;
                string extension = ".spritefont";
                if (renderMode == RenderMode.Xenko)
                {
                    assetName = assetName.Replace(".", "-");
                    string assetGuid = Guid.NewGuid().ToString();
                    fileContent = string.Format(template, fontName, fontSize.ToString(CultureInfo.InvariantCulture), fontStyle, assetGuid);
                    extension = ".xkfnt";
                }
                else
                {
                    fileContent = string.Format(template, fontName, fontSize.ToString(CultureInfo.InvariantCulture), fontStyle);
                }

                string fullPath = Path.Combine(path, assetName) + extension;

                using (StreamWriter outfile = new StreamWriter(fullPath, false, Encoding.UTF8))
                {
                    outfile.Write(fileContent);
                }

                Console.WriteLine(string.Format("Font {0} , size {1}, style {2} generated to file {3}", fontName, fontSize, fontStyle, fullPath));
            }
        }

        private static string GetFontName(FontInfo info)
        {
            return info.FontFamily.Source;
        }

        private static float GetFontSize(FontInfo info)
        {
            float fontSize = (float)info.FontSize * 72f / 96f;
            return fontSize;
        }

        private static string GetFontAssetName(string fontName, float fontSize, string fontStyle)
        {
            return fontName.Replace(" ", "_") + "_" + fontSize.ToString(CultureInfo.InvariantCulture) + "_" + fontStyle.Replace(" ", "_").Replace(",", "_");
        }

        private static string GetFontStyle(FontInfo info)
        {
            string fontStyle = "Regular";
            if (info.FontWeight == FontWeights.Bold)
            {
                fontStyle = "Bold";
            }

            if (info.FontStyle == FontStyles.Italic)
            {
                if (info.FontWeight == FontWeights.Regular || info.FontWeight == FontWeights.Normal)
                {
                    fontStyle = "Italic";
                }
                if (info.FontWeight == FontWeights.Bold)
                {
                    fontStyle = "Bold, Italic";
                }
            }

            return fontStyle;
        }
    }
}
