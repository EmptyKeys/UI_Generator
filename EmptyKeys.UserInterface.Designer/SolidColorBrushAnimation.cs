using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EmptyKeys.UserInterface.Designer
{
    public class SolidColorBrushAnimation : AnimationTimeline
    {
        /// <summary>
        /// From property
        /// </summary>
        public static readonly DependencyProperty FromProperty =
            DependencyProperty.Register("From", typeof(Nullable<Color>), typeof(SolidColorBrushAnimation), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        public Nullable<Color> From
        {
            get { return (Nullable<Color>)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        /// <summary>
        /// To property
        /// </summary>
        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register("To", typeof(Nullable<Color>), typeof(SolidColorBrushAnimation), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        public Nullable<Color> To
        {
            get { return (Nullable<Color>)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        /// <summary>
        /// The by property
        /// </summary>
        public static readonly DependencyProperty ByProperty =
            DependencyProperty.Register("By", typeof(Nullable<Color>), typeof(SolidColorBrushAnimation), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the by.
        /// </summary>
        /// <value>
        /// The by.
        /// </value>
        public Nullable<Color> By
        {
            get { return (Nullable<Color>)GetValue(ByProperty); }
            set { SetValue(ByProperty, value); }
        }        

        /// <summary>
        /// Gets or sets a value indicating whether animation is additive.
        /// </summary>
        /// <value>
        /// <c>true</c> if animation is additive; otherwise, <c>false</c>.
        /// </value>
        public bool IsAdditive
        {
            get { return (bool)GetValue(IsAdditiveProperty); }
            set { SetValue(IsAdditiveProperty, value); }
        }

        /// <summary>
        /// The easing function property
        /// </summary>
        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(SolidColorBrushAnimation),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the easing function.
        /// </summary>
        /// <value>
        /// The easing function.
        /// </value>
        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }
        
        /// <summary>
        /// When overridden in a derived class, gets the <see cref="T:System.Type" /> of property that can be animated.
        /// </summary>
        public override Type TargetPropertyType
        {
            get
            {
                return typeof(SolidColorBrush);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SolidColorBrushAnimation"/> class.
        /// </summary>
        public SolidColorBrushAnimation()
            : base()
        {
        }

        /// <summary>
        /// When implemented in a derived class, creates a new instance of the <see cref="T:System.Windows.Freezable" /> derived class.
        /// </summary>
        /// <returns>
        /// The new instance.
        /// </returns>
        protected override Freezable CreateInstanceCore()
        {
            return new SolidColorBrushAnimation();
        }
    }
}
