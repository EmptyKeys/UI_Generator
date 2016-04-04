using System;
using System.Collections;
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
        /// The owned windows content property
        /// </summary>
        public static readonly DependencyProperty OwnedWindowsContentProperty =
            DependencyProperty.Register("OwnedWindowsContent", typeof(IEnumerable), typeof(UIRoot),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Gets or sets the content of the owned windows.
        /// </summary>
        /// <value>
        /// The content of the owned windows.
        /// </value>
        public IEnumerable OwnedWindowsContent
        {
            get { return (IEnumerable)GetValue(OwnedWindowsContentProperty); }
            set { SetValue(OwnedWindowsContentProperty, value); }
        }

        /// <summary>
        /// The is tab navigation enabled property
        /// </summary>
        public static readonly DependencyProperty IsTabNavigationEnabledProperty =
            DependencyProperty.Register("IsTabNavigationEnabled", typeof(bool), typeof(UIRoot),
            new FrameworkPropertyMetadata(true));

        /// <summary>
        /// Gets or sets a value indicating whether this instance is tab navigation enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is tab navigation enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsTabNavigationEnabled
        {
            get { return (bool)GetValue(IsTabNavigationEnabledProperty); }
            set { SetValue(IsTabNavigationEnabledProperty, value); }
        }

        /// <summary>
        /// The message box overlay property{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
        /// </summary>
        public static readonly DependencyProperty MessageBoxOverlayProperty =
            DependencyProperty.Register("MessageBoxOverlay", typeof(Color), typeof(UIRoot),
            new FrameworkPropertyMetadata(new Color { A = 127 }));

        /// <summary>
        /// Gets or sets the message box overlay.
        /// </summary>
        /// <value>
        /// The message box overlay.
        /// </value>
        public Color MessageBoxOverlay
        {
            get { return (Color)GetValue(MessageBoxOverlayProperty); }
            set { SetValue(MessageBoxOverlayProperty, value); }
        }

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
