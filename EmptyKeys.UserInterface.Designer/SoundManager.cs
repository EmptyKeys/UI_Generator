using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmptyKeys.UserInterface.Designer
{
    public class SoundManager
    {        
        /// <summary>
        /// The sounds property
        /// </summary>
        public static readonly DependencyProperty SoundsProperty =
            DependencyProperty.RegisterAttached("Sounds", typeof(SoundSourceCollection), typeof(SoundManager),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Gets the sounds.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static SoundSourceCollection GetSounds(DependencyObject obj)
        {
            var collection = (SoundSourceCollection)obj.GetValue(SoundsProperty);
            if (collection == null)
            {
                collection = new SoundSourceCollection();
                obj.SetValue(SoundsProperty, collection);
            }

            return collection;
        }

        /// <summary>
        /// Sets the sounds.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetSounds(DependencyObject obj, SoundSourceCollection value)
        {
            obj.SetValue(SoundsProperty, value);
        }
    }
}
