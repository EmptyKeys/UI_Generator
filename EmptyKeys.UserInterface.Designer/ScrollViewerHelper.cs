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
    public static class ScrollViewerHelper
    {
        private static Type typeOfThis = typeof(ScrollViewerHelper);

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
    }
}
