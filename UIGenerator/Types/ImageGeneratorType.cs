using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EmptyKeys.UserInterface.Generator.Types
{
    /// <summary>
    /// Implements Image control generator
    /// </summary>
    public class ImageGeneratorType : ElementGeneratorType
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
                return typeof(Image);
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

            Image image = source as Image;

            BitmapImage bitmap = image.Source as BitmapImage;
            if (bitmap != null)
            {
                CodeComHelper.GenerateBitmapImageField(method, fieldReference, bitmap.UriSource, image.Name + "_bm", "Source");
            }
                     
            CodeComHelper.GenerateEnumField<Stretch>(method, fieldReference, source, Image.StretchProperty);
            return fieldReference;
        }        
    }
}
