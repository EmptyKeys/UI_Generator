using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EmptyKeys.UserInterface.Designer
{
    public class AnimatedImage : Image
    {
        private static readonly Type typeOfThis = typeof(AnimatedImage);
        private static DependencyObjectType dependencyType;

        /// <summary>
        /// The frame width property
        /// </summary>
        public static readonly DependencyProperty FrameWidthProperty =
            DependencyProperty.Register("FrameWidth", typeof(int), typeOfThis,
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the width of one frame in image atlas
        /// </summary>
        /// <value>
        /// The width of the frame.
        /// </value>
        public int FrameWidth
        {
            get { return (int)GetValue(FrameWidthProperty); }
            set { SetValue(FrameWidthProperty, value); }
        }

        /// <summary>
        /// The frame height property
        /// </summary>
        public static readonly DependencyProperty FrameHeightProperty =
            DependencyProperty.Register("FrameHeight", typeof(int), typeOfThis,
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the height of the frame.
        /// </summary>
        /// <value>
        /// The height of the frame.
        /// </value>
        public int FrameHeight
        {
            get { return (int)GetValue(FrameHeightProperty); }
            set { SetValue(FrameHeightProperty, value); }
        }

        /// <summary>
        /// The frames per second property
        /// </summary>
        public static readonly DependencyProperty FramesPerSecondProperty =
            DependencyProperty.Register("FramesPerSecond", typeof(int), typeOfThis,
            new FrameworkPropertyMetadata(60));

        /// <summary>
        /// Gets or sets the frames per second.
        /// </summary>
        /// <value>
        /// The frames per second.
        /// </value>
        public int FramesPerSecond
        {
            get { return (int)GetValue(FramesPerSecondProperty); }
            set { SetValue(FramesPerSecondProperty, value); }
        }

        static AnimatedImage()
        {
            dependencyType = DependencyObjectType.FromSystemType(typeOfThis);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimatedImage"/> class.
        /// </summary>
        public AnimatedImage()
            : base()
        {
        }
    }
}
