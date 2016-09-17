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
    }
}
