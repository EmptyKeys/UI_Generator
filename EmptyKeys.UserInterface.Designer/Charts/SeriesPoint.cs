using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace EmptyKeys.UserInterface.Designer.Charts
{
    /// <summary>
    /// Implements single chart point - fake designer class
    /// </summary>
    public class SeriesPoint : DependencyObject
    {
        private static readonly Type typeOfThis = typeof(SeriesPoint);
        private static DependencyObjectType dependencyType;

        /// <summary>
        /// The argument property
        /// </summary>
        public static readonly DependencyProperty ArgumentProperty =
            DependencyProperty.Register("Argument", typeof(float), typeOfThis,
            new FrameworkPropertyMetadata(float.NaN));

        /// <summary>
        /// Gets or sets the argument.
        /// </summary>
        /// <value>
        /// The argument.
        /// </value>
        public float Argument
        {
            get { return (float)GetValue(ArgumentProperty); }
            set { SetValue(ArgumentProperty, value); }
        }

        /// <summary>
        /// The value property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(float), typeOfThis,
            new FrameworkPropertyMetadata(float.NaN));

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public float Value
        {
            get { return (float)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        
        /// <summary>
        /// Initializes the <see cref="SeriesPoint"/> class.
        /// </summary>
        static SeriesPoint()
        {
            dependencyType = DependencyObjectType.FromSystemType(typeOfThis);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SeriesPoint"/> class.
        /// </summary>
        public SeriesPoint()
            : base()
        {
        }
    }
}
