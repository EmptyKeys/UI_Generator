using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmptyKeys.UserInterface.Designer.Interactions
{
    public class PlaySoundAction : Action
    {
        /// <summary>
        /// The source property
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(SoundSource), typeof(PlaySoundAction),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public SoundSource Source
        {
            get { return (SoundSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
    }
}
