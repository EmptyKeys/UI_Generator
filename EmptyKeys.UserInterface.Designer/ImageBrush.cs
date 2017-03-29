using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace EmptyKeys.UserInterface.Designer
{
    /// <summary>
    /// Implements fake designer class for Image Brush
    /// </summary>
    public static class ImageBrush
    {
        /// <summary>
        /// The color overlay property
        /// </summary>
        public static readonly DependencyProperty ColorOverlayProperty =
            DependencyProperty.RegisterAttached("ColorOverlay", typeof(Color), typeof(ImageBrush),
                new FrameworkPropertyMetadata(Color.FromArgb(255,255,255,255)));

        /// <summary>
        /// Gets the color overlay.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static Color GetColorOverlay(DependencyObject obj)
        {
            return (Color)obj.GetValue(ColorOverlayProperty);
        }

        /// <summary>
        /// Sets the color overlay.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetColorOverlay(DependencyObject obj, Color value)
        {
            obj.SetValue(ColorOverlayProperty, value);
        }
    }
}
