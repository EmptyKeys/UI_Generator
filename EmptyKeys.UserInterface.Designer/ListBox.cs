using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmptyKeys.UserInterface.Designer
{
    /// <summary>
    /// Fake class for ListBox designer features
    /// </summary>
    public static class ListBox
    {
        /// <summary>
        /// The is selected data enabled property
        /// </summary>
        public static readonly DependencyProperty IsSelectedDataEnabledProperty = DependencyProperty.RegisterAttached("IsSelectedDataEnabled", typeof(bool), typeof(ListBox),
                new FrameworkPropertyMetadata(true));

        /// <summary>
        /// Gets the is selected data enabled.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static bool GetIsSelectedDataEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsSelectedDataEnabledProperty);
        }

        /// <summary>
        /// Sets the is selected data enabled.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public static void SetIsSelectedDataEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsSelectedDataEnabledProperty, value);
        }

        /// <summary>
        /// The enable virtualization property
        /// </summary>
        public static readonly DependencyProperty EnableVirtualizationProperty =
            DependencyProperty.RegisterAttached("EnableVirtualization", typeof(bool), typeof(ListBox),
                new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Gets the enable virtualization.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static bool GetEnableVirtualization(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableVirtualizationProperty);
        }

        /// <summary>
        /// Sets the enable virtualization.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public static void SetEnableVirtualization(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableVirtualizationProperty, value);
        }

        /// <summary>
        /// The virtualized cache maximum items property
        /// </summary>
        public static readonly DependencyProperty VirtualizedCacheMaxItemsProperty =
            DependencyProperty.RegisterAttached("VirtualizedCacheMaxItems", typeof(int), typeof(ListBox),
            new FrameworkPropertyMetadata(50));

        /// <summary>
        /// Gets the virtualized cache maximum items.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static int GetVirtualizedCacheMaxItems(DependencyObject obj)
        {
            return (int)obj.GetValue(VirtualizedCacheMaxItemsProperty);
        }

        /// <summary>
        /// Sets the virtualized cache maximum items.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetVirtualizedCacheMaxItems(DependencyObject obj, int value)
        {
            obj.SetValue(VirtualizedCacheMaxItemsProperty, value);
        }
    }
}
