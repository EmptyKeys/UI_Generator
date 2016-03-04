using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmptyKeys.UserInterface.Designer
{
    /// <summary>
    /// Implements attached properties for binding generator
    /// </summary>
    public sealed class GeneratedBindings
    {
        private static Type typeOfThis = typeof(GeneratedBindings);

        /// <summary>
        /// The data type property
        /// </summary>
        public static readonly DependencyProperty DataTypeProperty =
            DependencyProperty.RegisterAttached("DataType", typeof(Type), typeOfThis,
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));

        /// <summary>
        /// Gets the type of the data.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static Type GetDataType(DependencyObject obj)
        {
            return (Type)obj.GetValue(DataTypeProperty);
        }

        /// <summary>
        /// Sets the type of the data.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetDataType(DependencyObject obj, Type value)
        {
            obj.SetValue(DataTypeProperty, value);
        }


        /// <summary>
        /// The mode property
        /// </summary>
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.RegisterAttached("Mode", typeof(GeneratedBindingsMode), typeOfThis,
            new FrameworkPropertyMetadata(GeneratedBindingsMode.Generated));

        /// <summary>
        /// Gets the mode.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static GeneratedBindingsMode GetMode(DependencyObject obj)
        {
            return (GeneratedBindingsMode)obj.GetValue(ModeProperty);
        }

        /// <summary>
        /// Sets the mode.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetMode(DependencyObject obj, GeneratedBindingsMode value)
        {
            obj.SetValue(ModeProperty, value);
        }        
    }
}
