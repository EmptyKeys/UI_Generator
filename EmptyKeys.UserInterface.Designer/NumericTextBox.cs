using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EmptyKeys.UserInterface.Designer
{
    public class NumericTextBox : TextBox
    {
        private static readonly Type typeOfThis = typeof(NumericTextBox);
        private static DependencyObjectType dependencyType;

        /// <summary>
        /// The minimum property
        /// </summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(float), typeOfThis,
            new FrameworkPropertyMetadata(
                float.MinValue,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnMinimumChanged)
                ));

        private static void OnMinimumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NumericTextBox numeric = d as NumericTextBox;
            numeric.CoerceValue(MaximumProperty);
            numeric.CoerceValue(ValueProperty);            
        }

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>
        /// The minimum.
        /// </value>
        public float Minimum
        {
            get { return (float)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        /// <summary>
        /// The maximum property
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(float), typeOfThis,
            new FrameworkPropertyMetadata(
                float.MaxValue,
                FrameworkPropertyMetadataOptions.None,
                new PropertyChangedCallback(OnMaximumChanged),
                new CoerceValueCallback(CoerceMaximum)
                ));

        private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NumericTextBox numeric = d as NumericTextBox;
            numeric.CoerceValue(ValueProperty);            
        }

        private static object CoerceMaximum(DependencyObject d, object baseValue)
        {
            NumericTextBox numeric = d as NumericTextBox;
            float min = numeric.Minimum;
            if (baseValue == null || min > (float)baseValue)
            {
                return min;
            }

            return baseValue;
        }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>
        /// The maximum.
        /// </value>
        public float Maximum
        {
            get { return (float)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        /// <summary>
        /// The value property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(float), typeOfThis,
            new FrameworkPropertyMetadata(0f, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnValueChanged), new CoerceValueCallback(CoerceValue)));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NumericTextBox numeric = d as NumericTextBox;
            if (numeric != null)
            {
                numeric.SetText(e.NewValue);
            }
        }

        private void SetText(object value)
        {
            float newValue = 0f;
            if (value != null)
            {
                newValue = (float)value;
            }
            string newText = newValue.ToString("N", CultureInfo.CurrentCulture);
            Text = newText;
        }

        private static object CoerceValue(DependencyObject d, object baseValue)
        {
            NumericTextBox numeric = d as NumericTextBox;
            float min = numeric.Minimum;
            if (baseValue == null || min > (float)baseValue)
            {
                numeric.SetText(min);
                return min;
            }

            float max = numeric.Maximum;
            if ((float)baseValue > max)
            {
                numeric.SetText(max);
                return max;
            }

            return baseValue;
        }

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
        /// The increment property
        /// </summary>
        public static readonly DependencyProperty IncrementProperty =
            DependencyProperty.Register("Increment", typeof(float), typeOfThis,
            new FrameworkPropertyMetadata(1f, FrameworkPropertyMetadataOptions.None));

        /// <summary>
        /// Gets or sets the increment.
        /// </summary>
        /// <value>
        /// The increment.
        /// </value>
        public float Increment
        {
            get { return (float)GetValue(IncrementProperty); }
            set { SetValue(IncrementProperty, value); }
        }

        /// <summary>
        /// Texts the box.
        /// </summary>
        static NumericTextBox()
        {
            dependencyType = DependencyObjectType.FromSystemType(typeOfThis);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericTextBox"/> class.
        /// </summary>
        public NumericTextBox()
            : base()
        {
        }
    }
}
