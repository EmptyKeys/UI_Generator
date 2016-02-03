using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EmptyKeys.UserInterface.Designer.Charts
{
    /// <summary>
    /// Implements Chart data visualization control - fake designer class
    /// </summary>
    public class Chart : Control
    {
        private static readonly Type typeOfThis = typeof(Chart);
        private static DependencyObjectType dependencyType;        

        /// <summary>
        /// The series property
        /// </summary>
        public static readonly DependencyProperty SeriesProperty =
            DependencyProperty.Register("Series", typeof(UIElementCollection), typeOfThis,
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the series.
        /// </summary>
        /// <value>
        /// The series.
        /// </value>
        public UIElementCollection Series
        {
            get { return (UIElementCollection)GetValue(SeriesProperty); }
            set { SetValue(SeriesProperty, value); }
        }

        /// <summary>
        /// The tickmark length property
        /// </summary>
        public static readonly DependencyProperty TickmarkLengthProperty =
            DependencyProperty.Register("TickmarkLength", typeof(float), typeOfThis,
            new FrameworkPropertyMetadata(0f, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the length of the tickmark.
        /// </summary>
        /// <value>
        /// The length of the tickmark.
        /// </value>
        public float TickmarkLength
        {
            get { return (float)GetValue(TickmarkLengthProperty); }
            set { SetValue(TickmarkLengthProperty, value); }
        }

        /// <summary>
        /// The tickmark thickness property
        /// </summary>
        public static readonly DependencyProperty TickmarkThicknessProperty =
            DependencyProperty.Register("TickmarkThickness", typeof(float), typeOfThis,
            new FrameworkPropertyMetadata(0f, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the tickmark thickness.
        /// </summary>
        /// <value>
        /// The tickmark thickness.
        /// </value>
        public float TickmarkThickness
        {
            get { return (float)GetValue(TickmarkThicknessProperty); }
            set { SetValue(TickmarkThicknessProperty, value); }
        }

        /// <summary>
        /// The axis thickness property
        /// </summary>
        public static readonly DependencyProperty AxisThicknessProperty =
            DependencyProperty.Register("AxisThickness", typeof(float), typeOfThis,
            new FrameworkPropertyMetadata(0f, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the axis thickness.
        /// </summary>
        /// <value>
        /// The axis thickness.
        /// </value>
        public float AxisThickness
        {
            get { return (float)GetValue(AxisThicknessProperty); }
            set { SetValue(AxisThicknessProperty, value); }
        }

        /// <summary>
        /// The axis brush property
        /// </summary>
        public static readonly DependencyProperty AxisBrushProperty =
            DependencyProperty.Register("AxisBrush", typeof(Brush), typeOfThis,
            new FrameworkPropertyMetadata(Brushes.Transparent));

        /// <summary>
        /// Gets or sets the axis brush.
        /// </summary>
        /// <value>
        /// The axis brush.
        /// </value>
        public Brush AxisBrush
        {
            get { return (Brush)GetValue(AxisBrushProperty); }
            set { SetValue(AxisBrushProperty, value); }
        }

        /// <summary>
        /// The axis visible property
        /// </summary>
        public static readonly DependencyProperty AxisVisibleProperty =
            DependencyProperty.Register("AxisVisible", typeof(bool), typeOfThis,
            new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets a value indicating whether [axis visible].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [axis visible]; otherwise, <c>false</c>.
        /// </value>
        public bool AxisVisible
        {
            get { return (bool)GetValue(AxisVisibleProperty); }
            set { SetValue(AxisVisibleProperty, value); }
        }

        /// <summary>
        /// The axis y major unit
        /// </summary>
        public static readonly DependencyProperty AxisYMajorUnitProperty =
            DependencyProperty.Register("AxisYMajorUnit", typeof(float), typeOfThis,
            new FrameworkPropertyMetadata(0f, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the axis y major unit.
        /// </summary>
        /// <value>
        /// The axis y major unit.
        /// </value>
        public float AxisYMajorUnit
        {
            get { return (float)GetValue(AxisYMajorUnitProperty); }
            set { SetValue(AxisYMajorUnitProperty, value); }
        }

        /// <summary>
        /// The axis x major unit property
        /// </summary>
        public static readonly DependencyProperty AxisXMajorUnitProperty =
            DependencyProperty.Register("AxisXMajorUnit", typeof(float), typeOfThis,
            new FrameworkPropertyMetadata(0f, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the axis x major unit.
        /// </summary>
        /// <value>
        /// The axis x major unit.
        /// </value>
        public float AxisXMajorUnit
        {
            get { return (float)GetValue(AxisXMajorUnitProperty); }
            set { SetValue(AxisXMajorUnitProperty, value); }
        }

        /// <summary>
        /// The cursor lines enabled property
        /// </summary>
        public static readonly DependencyProperty CursorLinesEnabledProperty =
            DependencyProperty.Register("CursorLinesEnabled", typeof(bool), typeOfThis,
            new FrameworkPropertyMetadata(true));

        /// <summary>
        /// Gets or sets a value indicating whether [cursor lines enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [cursor lines enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool CursorLinesEnabled
        {
            get { return (bool)GetValue(CursorLinesEnabledProperty); }
            set { SetValue(CursorLinesEnabledProperty, value); }
        }

        /// <summary>
        /// The cursor lines brush property
        /// </summary>
        public static readonly DependencyProperty CursorLinesBrushProperty =
            DependencyProperty.Register("CursorLinesBrush", typeof(Brush), typeOfThis,
            new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnCursorLinesBrushChanged)));

        private static void OnCursorLinesBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }

        /// <summary>
        /// Gets or sets the cursor lines brush.
        /// </summary>
        /// <value>
        /// The cursor lines brush.
        /// </value>
        public Brush CursorLinesBrush
        {
            get { return (Brush)GetValue(CursorLinesBrushProperty); }
            set { SetValue(CursorLinesBrushProperty, value); }
        }

        /// <summary>
        /// The axis label margin property
        /// </summary>
        public static readonly DependencyProperty AxisLabelMarginProperty =
            DependencyProperty.Register("AxisLabelMargin", typeof(float), typeOfThis,
            new FrameworkPropertyMetadata(0f, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the axis label margin.
        /// </summary>
        /// <value>
        /// The axis label margin.
        /// </value>
        public float AxisLabelMargin
        {
            get { return (float)GetValue(AxisLabelMarginProperty); }
            set { SetValue(AxisLabelMarginProperty, value); }
        }

        /// <summary>
        /// The axis labels visible property
        /// </summary>
        public static readonly DependencyProperty AxisLabelsVisibleProperty =
            DependencyProperty.Register("AxisLabelsVisible", typeof(bool), typeOfThis,
            new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets a value indicating whether [axis labels visible].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [axis labels visible]; otherwise, <c>false</c>.
        /// </value>
        public bool AxisLabelsVisible
        {
            get { return (bool)GetValue(AxisLabelsVisibleProperty); }
            set { SetValue(AxisLabelsVisibleProperty, value); }
        }

        /// <summary>
        /// The axis label format property
        /// </summary>
        public static readonly DependencyProperty AxisLabelFormatProperty =
            DependencyProperty.Register("AxisLabelFormat", typeof(string), typeOfThis,
            new FrameworkPropertyMetadata("{0}", FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the axis label format.
        /// </summary>
        /// <value>
        /// The axis label format.
        /// </value>
        public string AxisLabelFormat
        {
            get { return (string)GetValue(AxisLabelFormatProperty); }
            set { SetValue(AxisLabelFormatProperty, value); }
        }

        /// <summary>
        /// The axis interlaced property
        /// </summary>
        public static readonly DependencyProperty AxisInterlacedProperty =
            DependencyProperty.Register("AxisInterlaced", typeof(bool), typeOfThis,
            new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets a value indicating whether [axis interlaced].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [axis interlaced]; otherwise, <c>false</c>.
        /// </value>
        public bool AxisInterlaced
        {
            get { return (bool)GetValue(AxisInterlacedProperty); }
            set { SetValue(AxisInterlacedProperty, value); }
        }

        /// <summary>
        /// The axis interlaced brush property
        /// </summary>
        public static readonly DependencyProperty AxisInterlacedBrushProperty =
            DependencyProperty.Register("AxisInterlacedBrush", typeof(Brush), typeOfThis,
            new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the axis interlaced brush.
        /// </summary>
        /// <value>
        /// The axis interlaced brush.
        /// </value>
        public Brush AxisInterlacedBrush
        {
            get { return (Brush)GetValue(AxisInterlacedBrushProperty); }
            set { SetValue(AxisInterlacedBrushProperty, value); }
        }

        /// <summary>
        /// Initializes the <see cref="Chart"/> class.
        /// </summary>
        static Chart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Chart), new FrameworkPropertyMetadata(typeof(Chart)));
            dependencyType = DependencyObjectType.FromSystemType(typeOfThis);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Chart"/> class.
        /// </summary>
        public Chart()
            : base()
        {
            Series = new UIElementCollection(this, this);
        }             
    }
}
