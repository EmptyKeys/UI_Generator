using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EmptyKeys.UserInterface.Designer
{
    public static class AnimatedImageBrush 
    {
        private static Type typeOfThis = typeof(AnimatedImageBrush);

        /// <summary>
        /// The frame width property
        /// </summary>
        public static readonly DependencyProperty FrameWidthProperty =
            DependencyProperty.RegisterAttached("FrameWidth", typeof(int), typeOfThis,
            new FrameworkPropertyMetadata(0));

        /// <summary>
        /// Gets the width of the frame.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static int GetFrameWidth(DependencyObject obj)
        {
            return (int)obj.GetValue(FrameWidthProperty);
        }

        /// <summary>
        /// Sets the width of the frame.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetFrameWidth(DependencyObject obj, int value)
        {
            obj.SetValue(FrameWidthProperty, value);
        }

        /// <summary>
        /// The frame height property
        /// </summary>
        public static readonly DependencyProperty FrameHeightProperty =
            DependencyProperty.RegisterAttached("FrameHeight", typeof(int), typeOfThis,
            new FrameworkPropertyMetadata(0));

        /// <summary>
        /// Gets the height of the frame.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static int GetFrameHeight(DependencyObject obj)
        {
            return (int)obj.GetValue(FrameHeightProperty);
        }

        /// <summary>
        /// Sets the height of the frame.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetFrameHeight(DependencyObject obj, int value)
        {
            obj.SetValue(FrameHeightProperty, value);
        }

        /// <summary>
        /// The frames per second property
        /// </summary>
        public static readonly DependencyProperty FramesPerSecondProperty =
            DependencyProperty.RegisterAttached("FramesPerSecond", typeof(int), typeOfThis,
            new FrameworkPropertyMetadata(60));

        /// <summary>
        /// Gets the frames per second.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static int GetFramesPerSecond(DependencyObject obj)
        {
            return (int)obj.GetValue(FramesPerSecondProperty);
        }

        /// <summary>
        /// Sets the frames per second.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetFramesPerSecond(DependencyObject obj, int value)
        {
            obj.SetValue(FramesPerSecondProperty, value);
        }

        /// <summary>
        /// The animate property
        /// </summary>
        public static readonly DependencyProperty AnimateProperty =
            DependencyProperty.RegisterAttached("Animate", typeof(bool), typeOfThis,
            new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Gets the animate.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static bool GetAnimate(DependencyObject obj)
        {
            return (bool)obj.GetValue(AnimateProperty);
        }

        /// <summary>
        /// Sets the animate.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetAnimate(DependencyObject obj, bool value)
        {
            obj.SetValue(AnimateProperty, value);
        }
    }
}
