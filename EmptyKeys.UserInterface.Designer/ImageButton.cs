using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EmptyKeys.UserInterface.Designer
{
    /// <summary>
    /// Implements button with image
    /// </summary>
    public class ImageButton : Button
    {
        private static readonly Type typeOfThis = typeof(ImageButton);
        private static DependencyObjectType dependencyType;

        /// <summary>
        /// The image normal property
        /// </summary>
        public static readonly DependencyProperty ImageNormalProperty =
            DependencyProperty.Register("ImageNormal", typeof(BitmapImage), typeOfThis,
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the image normal.
        /// </summary>
        /// <value>
        /// The image normal.
        /// </value>
        public BitmapImage ImageNormal
        {
            get { return (BitmapImage)GetValue(ImageNormalProperty); }
            set { SetValue(ImageNormalProperty, value); }
        }

        /// <summary>
        /// The image pressed property
        /// </summary>
        public static readonly DependencyProperty ImagePressedProperty =
            DependencyProperty.Register("ImagePressed", typeof(BitmapImage), typeOfThis,
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the image pressed.
        /// </summary>
        /// <value>
        /// The image pressed.
        /// </value>
        public BitmapImage ImagePressed
        {
            get { return (BitmapImage)GetValue(ImagePressedProperty); }
            set { SetValue(ImagePressedProperty, value); }
        }

        /// <summary>
        /// The image hover property
        /// </summary>
        public static readonly DependencyProperty ImageHoverProperty =
            DependencyProperty.Register("ImageHover", typeof(BitmapImage), typeOfThis,
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the image hover.
        /// </summary>
        /// <value>
        /// The image hover.
        /// </value>
        public BitmapImage ImageHover
        {
            get { return (BitmapImage)GetValue(ImageHoverProperty); }
            set { SetValue(ImageHoverProperty, value); }
        }

        /// <summary>
        /// The image disabled property
        /// </summary>
        public static readonly DependencyProperty ImageDisabledProperty =
            DependencyProperty.Register("ImageDisabled", typeof(BitmapImage), typeOfThis,
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the image disabled.
        /// </summary>
        /// <value>
        /// The image disabled.
        /// </value>
        public BitmapImage ImageDisabled
        {
            get { return (BitmapImage)GetValue(ImageDisabledProperty); }
            set { SetValue(ImageDisabledProperty, value); }
        }

        /*
        /// <summary>
        /// The image focused property
        /// </summary>
        public static readonly DependencyProperty ImageFocusedProperty =
            DependencyProperty.Register("ImageFocused", typeof(BitmapImage), typeOfThis,
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the image focused.
        /// </summary>
        /// <value>
        /// The image focused.
        /// </value>
        public BitmapImage ImageFocused
        {
            get { return (BitmapImage)GetValue(ImageFocusedProperty); }
            set { SetValue(ImageFocusedProperty, value); }
        }
        */

        /// <summary>
        /// The image stretch property
        /// </summary>
        public static readonly DependencyProperty ImageStretchProperty =
            DependencyProperty.Register("ImageStretch", typeof(Stretch), typeOfThis,
            new FrameworkPropertyMetadata(Stretch.Uniform, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the image stretch.
        /// </summary>
        /// <value>
        /// The image stretch.
        /// </value>
        public Stretch ImageStretch
        {
            get { return (Stretch)GetValue(ImageStretchProperty); }
            set { SetValue(ImageStretchProperty, value); }
        }

        /// <summary>
        /// Initializes the <see cref="ImageButton"/> class.
        /// </summary>
        static ImageButton()
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageButton"/> class.
        /// </summary>
        public ImageButton()
            : base()
        {
        }
    }
}
