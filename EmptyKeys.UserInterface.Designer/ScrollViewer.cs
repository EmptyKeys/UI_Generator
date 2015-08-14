using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmptyKeys.UserInterface.Designer
{
    /// <summary>
    /// Implements Scroll Viewer helper with custom attached property, this is fake designer class
    /// </summary>
    public static class ScrollViewer
    {
        private static Type typeOfThis = typeof(ScrollViewer);

        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static ScrollViewerAction GetAction(DependencyObject obj)
        {
            return (ScrollViewerAction)obj.GetValue(ActionProperty);
        }

        /// <summary>
        /// Sets the action.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetAction(DependencyObject obj, ScrollViewerAction value)
        {
            obj.SetValue(ActionProperty, value);
        }

        /// <summary>
        /// The action property
        /// </summary>
        public static readonly DependencyProperty ActionProperty =
            DependencyProperty.RegisterAttached("Action", typeof(ScrollViewerAction), typeOfThis,
            new FrameworkPropertyMetadata(ScrollViewerAction.None, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnActionChanged)));

        private static void OnActionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {            
        }

        /// <summary>
        /// The scroll point property
        /// </summary>
        public static readonly DependencyProperty ScrollPointProperty =
            DependencyProperty.RegisterAttached("ScrollPoint", typeof(Point), typeOfThis,
            new FrameworkPropertyMetadata(new Point()));

        /// <summary>
        /// Gets the scroll point.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static Point GetScrollPoint(DependencyObject obj)
        {
            return (Point)obj.GetValue(ScrollPointProperty);
        }

        /// <summary>
        /// Sets the scroll point.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetScrollPoint(DependencyObject obj, Point value)
        {
            obj.SetValue(ScrollPointProperty, value);
        }

        /// <summary>
        /// The is mouse wheel enabled property
        /// </summary>
        public static readonly DependencyProperty IsMouseWheelEnabledProperty =
            DependencyProperty.RegisterAttached("IsMouseWheelEnabled", typeof(bool), typeof(ScrollViewer),
            new FrameworkPropertyMetadata(true));

        /// <summary>
        /// Gets the is mouse wheel enabled.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static bool GetIsMouseWheelEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMouseWheelEnabledProperty);
        }

        /// <summary>
        /// Sets the is mouse wheel enabled.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public static void SetIsMouseWheelEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMouseWheelEnabledProperty, value);
        }
    }
}
