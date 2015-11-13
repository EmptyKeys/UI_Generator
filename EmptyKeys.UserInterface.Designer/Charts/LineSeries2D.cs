using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace EmptyKeys.UserInterface.Designer.Charts
{
    /// <summary>
    /// Implements line 2D chart series - fake designer class
    /// </summary>
    public class LineSeries2D : Control
    {
        private static readonly Type typeOfThis = typeof(LineSeries2D);
        private static DependencyObjectType dependencyType;        

        /// <summary>
        /// The points property
        /// </summary>
        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(SeriesPointCollection), typeOfThis,
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>
        /// The points.
        /// </value>
        public SeriesPointCollection Points
        {
            get { return (SeriesPointCollection)GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }

        /// <summary>
        /// The line thickness property
        /// </summary>
        public static readonly DependencyProperty LineThicknessProperty =
            DependencyProperty.Register("LineThickness", typeof(float), typeOfThis,
            new FrameworkPropertyMetadata(1f, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnLineThicknessChange)));

        private static void OnLineThicknessChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }

        /// <summary>
        /// Gets or sets the line thickness.
        /// </summary>
        /// <value>
        /// The line thickness.
        /// </value>
        public float LineThickness
        {
            get { return (float)GetValue(LineThicknessProperty); }
            set { SetValue(LineThicknessProperty, value); }
        }

        /// <summary>
        /// The data source property
        /// </summary>
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(object), typeOfThis,
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(OnDataSourceChange)));

        private static void OnDataSourceChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }

        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        /// <value>
        /// The data source.
        /// </value>
        public object DataSource
        {
            get { return (object)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }        
        
        /// <summary>
        /// Initializes the <see cref="LineSeries2D"/> class.
        /// </summary>
        static LineSeries2D()
        {
            dependencyType = DependencyObjectType.FromSystemType(typeOfThis);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSeries2D"/> class.
        /// </summary>
        public LineSeries2D()
            : base()
        {
            Points = new SeriesPointCollection();
        }

        /// <summary>
        /// Measure layout pass
        /// </summary>
        /// <param name="availableSize">available size for element</param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            return new Size();
        }
    }
}
