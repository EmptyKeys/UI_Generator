using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmptyKeys.UserInterface.Designer
{
    /// <summary>
    /// Implements UI Root designer control
    /// </summary>
    public class UIRoot : ContentControl
    {
        /// <summary>
        /// Initializes the <see cref="UIRoot"/> class.
        /// </summary>
        static UIRoot()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(UIRoot), new FrameworkPropertyMetadata(typeof(UIRoot)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UIRoot"/> class.
        /// </summary>
        public UIRoot()
            : base()
        {            
        }
    }
}
