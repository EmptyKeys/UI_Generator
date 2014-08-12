using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmptyKeys.UserInterface.Designer
{
    /// <summary>
    /// Implements sound source for WPF designer
    /// </summary>
    public class SoundSource : Freezable
    {
        /// <summary>
        /// The sound asset property
        /// </summary>
        public static readonly DependencyProperty SoundAssetProperty =
            DependencyProperty.Register("SoundAsset", typeof(string), typeof(SoundSource),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the sound asset.
        /// </summary>
        /// <value>
        /// The sound asset.
        /// </value>
        public string SoundAsset
        {
            get { return (string)GetValue(SoundAssetProperty); }
            set { SetValue(SoundAssetProperty, value); }
        }

        /// <summary>
        /// The sound type property
        /// </summary>
        public static readonly DependencyProperty SoundTypeProperty =
            DependencyProperty.Register("SoundType", typeof(SoundType), typeof(SoundSource),
            new FrameworkPropertyMetadata(SoundType.None));

        /// <summary>
        /// Gets or sets the type of the sound.
        /// </summary>
        /// <value>
        /// The type of the sound.
        /// </value>
        public SoundType SoundType
        {
            get { return (SoundType)GetValue(SoundTypeProperty); }
            set { SetValue(SoundTypeProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SoundSource"/> class.
        /// </summary>
        public SoundSource()
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
            return new SoundSource();
        }
    }
}
