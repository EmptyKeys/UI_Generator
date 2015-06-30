using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmptyKeys.UserInterface.Designer
{
    public sealed class DragDrop
    {
        private static Type typeOfThis = typeof(DragDrop);

        /// <summary>
        /// The is drag source property
        /// </summary>
        public static readonly DependencyProperty IsDragSourceProperty =
            DependencyProperty.RegisterAttached("IsDragSource", typeof(bool), typeOfThis,
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None));
        
        /// <summary>
        /// Gets the is drag source.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static bool GetIsDragSource(UIElement target)
        {
            return (bool)target.GetValue(IsDragSourceProperty);
        }

        /// <summary>
        /// Sets the is drag source.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public static void SetIsDragSource(UIElement target, bool value)
        {
            target.SetValue(IsDragSourceProperty, value);
        }

        /// <summary>
        /// The is drop target property
        /// </summary>
        public static readonly DependencyProperty IsDropTargetProperty =
          DependencyProperty.RegisterAttached("IsDropTarget", typeof(bool), typeOfThis,
          new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None));        

        /// <summary>
        /// Gets the is drop target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static bool GetIsDropTarget(UIElement target)
        {
            return (bool)target.GetValue(IsDropTargetProperty);
        }

        /// <summary>
        /// Sets the is drop target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public static void SetIsDropTarget(UIElement target, bool value)
        {
            target.SetValue(IsDropTargetProperty, value);
        }
    }
}
