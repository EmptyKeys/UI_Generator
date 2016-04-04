using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace EmptyKeys.UserInterface.Generator
{
    /// <summary>
    /// Describes font
    /// </summary>
    public class FontInfo
    {
        /// <summary>
        /// Gets or sets the font family.
        /// </summary>
        /// <value>
        /// The font family.
        /// </value>
        public FontFamily FontFamily { get; set; }        

        /// <summary>
        /// Gets or sets the size of the font.
        /// </summary>
        /// <value>
        /// The size of the font.
        /// </value>
        public double FontSize { get; set; }

        /// <summary>
        /// Gets or sets the font style.
        /// </summary>
        /// <value>
        /// The font style.
        /// </value>
        public FontStyle FontStyle { get; set; }

        /// <summary>
        /// Gets or sets the font weight.
        /// </summary>
        /// <value>
        /// The font weight.
        /// </value>
        public FontWeight FontWeight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether manager code is generated.
        /// </summary>
        /// <value>
        /// <c>true</c> if manager code is generated; otherwise, <c>false</c>.
        /// </value>
        public bool IsManagerCodeGenerated { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FontInfo"/> class.
        /// </summary>
        /// <param name="family">The family.</param>
        /// <param name="size">The size.</param>
        /// <param name="style">The style.</param>
        /// <param name="weight">The weight.</param>
        public FontInfo(FontFamily family, double size, FontStyle style, FontWeight weight)
        {
            FontFamily = family;
            FontSize = size;
            FontStyle = style;
            FontWeight = weight;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            FontInfo info = obj as FontInfo;
            if (info == null)
            {
                return false;
            }

            if (info.FontFamily.Source == this.FontFamily.Source &&
                info.FontSize == this.FontSize &&
                info.FontStyle == this.FontStyle &&
                info.FontWeight == this.FontWeight)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return FontFamily.Source.GetHashCode() ^ FontSize.GetHashCode() ^ FontStyle.GetHashCode() ^ FontWeight.GetHashCode();
        }
    }
}
