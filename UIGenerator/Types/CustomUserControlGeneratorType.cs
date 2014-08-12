using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyKeys.UserInterface.Generator.Types
{
    /// <summary>
    /// Implements fake generator for custom User Controls (in data templates for MVVM support)
    /// </summary>
    public class CustomUserControlGeneratorType : ContentControlGeneratorType
    {
        /// <summary>
        /// Gets the type of the xaml.
        /// </summary>
        /// <value>
        /// The type of the xaml.
        /// </value>
        public override Type XamlType
        {
            get
            {
                return this.GetType();
            }
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <param name="source">source</param>
        /// <returns></returns>
        public override System.Collections.IEnumerable GetChildren(System.Windows.DependencyObject source)
        {
            // we don't have to generate children, because custom user control is already generated
            return null;
        }
    }
}
