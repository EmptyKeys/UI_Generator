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
        /// The Namespace Property
        /// </summary>
        public static readonly DependencyProperty NamespaceProperty = 
            DependencyProperty.Register("Namespace", typeof(string), typeof(UIRoot));

        /// <summary>
        /// Gets or sets the namespace to generate the code under.
        /// </summary>
        public string Namespace
        {
            get { return (string)GetValue(NamespaceProperty); }
            set { SetValue(NamespaceProperty, value); }
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
