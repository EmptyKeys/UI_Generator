using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Effects;

namespace EmptyKeys.UserInterface.Designer.Effects
{
    /// <summary>
    /// Implements empty class for custom effect shader - fake class for designer
    /// </summary>
    /// <seealso cref="System.Windows.Media.Effects.ShaderEffect" />
    public class CustomEffect : ShaderEffect
    {
        /// <summary>
        /// The effect asset property
        /// </summary>
        public static readonly DependencyProperty EffectAssetProperty =
                    DependencyProperty.Register("EffectAsset", typeof(string), typeof(CustomEffect),
                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));

        /// <summary>
        /// Gets or sets the effect asset.
        /// </summary>
        /// <value>
        /// The effect asset.
        /// </value>
        public string EffectAsset
        {
            get { return (string)GetValue(EffectAssetProperty); }
            set { SetValue(EffectAssetProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomEffect"/> class.
        /// </summary>
        public CustomEffect() : base()
        {
            var pixelShader = new PixelShader();
            var fileUri = new Uri("pack://application:,,,/EmptyKeys.UserInterface.Designer;component/Effects/FakeShader.ps", UriKind.RelativeOrAbsolute);
            pixelShader.UriSource = fileUri;
            PixelShader = pixelShader;
        }        
    }
}
