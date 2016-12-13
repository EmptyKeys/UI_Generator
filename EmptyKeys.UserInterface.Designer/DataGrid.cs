using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmptyKeys.UserInterface.Designer
{
    /// <summary>
    /// Fake designer helper class for DataGrid
    /// </summary>
    public static class DataGrid
    {
        /// <summary>
        /// Gets the sorting command.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static ICommand GetSortingCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(SortingCommandProperty);
        }

        /// <summary>
        /// Sets the sorting command.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetSortingCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(SortingCommandProperty, value);
        }

        /// <summary>
        /// The sorting command property
        /// </summary>
        public static readonly DependencyProperty SortingCommandProperty =
            DependencyProperty.RegisterAttached("SortingCommand", typeof(ICommand), typeof(DataGrid),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// The virtualized cache maximum rows property
        /// </summary>
        public static readonly DependencyProperty VirtualizedCacheMaxRowsProperty =
            DependencyProperty.RegisterAttached("VirtualizedCacheMaxRows", typeof(int), typeof(DataGrid),
            new FrameworkPropertyMetadata(50));

        /// <summary>
        /// Gets the virtualized cache maximum rows.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static int GetVirtualizedCacheMaxRows(DependencyObject obj)
        {
            return (int)obj.GetValue(VirtualizedCacheMaxRowsProperty);
        }

        /// <summary>
        /// Sets the virtualized cache maximum rows.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetVirtualizedCacheMaxRows(DependencyObject obj, int value)
        {
            obj.SetValue(VirtualizedCacheMaxRowsProperty, value);
        }
    }
}
