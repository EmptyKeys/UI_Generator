using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EmptyKeys.UserInterface.Designer;

namespace EmptyKeys.UserInterface.Generator.Types.Controls
{
    /// <summary>
    /// Implements Image Button generator
    /// </summary>
    /// <seealso cref="EmptyKeys.UserInterface.Generator.Types.ButtonGeneratorType" />
    public class ImageButtonGeneratorType : ButtonGeneratorType
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
                return typeof(ImageButton);
            }
        }

        /// <summary>
        /// Generates code
        /// </summary>
        /// <param name="source">The dependence object</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="initMethod">The initialize method.</param>
        /// <param name="generateField"></param>
        /// <returns></returns>
        public override CodeExpression Generate(DependencyObject source, CodeTypeDeclaration classType, CodeMemberMethod initMethod, bool generateField)
        {
            CodeExpression fieldReference = base.Generate(source, classType, initMethod, generateField);
            CodeComHelper.GenerateEnumField<Stretch>(initMethod, fieldReference, source, ImageButton.ImageStretchProperty);

            ImageButton imageButton = source as ImageButton;
            BitmapImage bitmap = imageButton.ImageNormal as BitmapImage;
            if (bitmap != null)
            {
                CodeComHelper.GenerateBitmapImageField(initMethod, fieldReference, source, bitmap.UriSource, imageButton.Name + "_normal_bm", ImageButton.ImageNormalProperty);
            }

            bitmap = imageButton.ImageDisabled as BitmapImage;
            if (bitmap != null)
            {
                CodeComHelper.GenerateBitmapImageField(initMethod, fieldReference, source, bitmap.UriSource, imageButton.Name + "_disabled_bm", ImageButton.ImageDisabledProperty);
            }

            bitmap = imageButton.ImageHover as BitmapImage;
            if (bitmap != null)
            {
                CodeComHelper.GenerateBitmapImageField(initMethod, fieldReference, source, bitmap.UriSource, imageButton.Name + "_hover_bm", ImageButton.ImageHoverProperty);
            }

            bitmap = imageButton.ImagePressed as BitmapImage;
            if (bitmap != null)
            {
                CodeComHelper.GenerateBitmapImageField(initMethod, fieldReference, source, bitmap.UriSource, imageButton.Name + "_pressed_bm", ImageButton.ImagePressedProperty);
            }

            return fieldReference;
        }
    }
}
