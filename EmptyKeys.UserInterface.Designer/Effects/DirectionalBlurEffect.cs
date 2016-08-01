using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Effects;

namespace EmptyKeys.UserInterface.Designer.Effects
{
    /// <summary>
    /// Implements Directional Blur effect
    /// </summary>    
    public class DirectionalBlurEffect : ShaderEffect
    {
        private static Type typeOfThis = typeof(DirectionalBlurEffect);

        /// <summary>
        /// The angle property
        /// </summary>
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(float), typeOfThis, 
                new FrameworkPropertyMetadata(0f, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets or sets the angle.
        /// </summary>
        /// <value>
        /// The angle.
        /// </value>
        public float Angle
        {
            get { return (float)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        /// <summary>
        /// The blur amount property
        /// </summary>
        public static readonly DependencyProperty BlurAmountProperty =
            DependencyProperty.Register("BlurAmount", typeof(float), typeOfThis,
                new FrameworkPropertyMetadata(0f, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets or sets the blur amount.
        /// </summary>
        /// <value>
        /// The blur amount.
        /// </value>
        public float BlurAmount
        {
            get { return (float)GetValue(BlurAmountProperty); }
            set { SetValue(BlurAmountProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectionalBlurEffect"/> class.
        /// </summary>
        public DirectionalBlurEffect() : base()
        {
            var pixelShader = new PixelShader();            
            var fileUri = new Uri("pack://application:,,,/EmptyKeys.UserInterface.Designer;component/Effects/FakeShader.ps", UriKind.RelativeOrAbsolute);
            pixelShader.UriSource = fileUri;            
            PixelShader = pixelShader;
        }
    }
}
