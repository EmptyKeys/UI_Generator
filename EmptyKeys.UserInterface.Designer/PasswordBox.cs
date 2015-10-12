using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EmptyKeys.UserInterface.Designer
{
    public class PasswordBox : TextBox
    {
        private static readonly Type typeOfThis = typeof(PasswordBox);        

        /// <summary>
        /// The password character property
        /// </summary>
        public static readonly DependencyProperty PasswordCharProperty =
            DependencyProperty.Register("PasswordChar", typeof(char), typeOfThis,
            new FrameworkPropertyMetadata('*', FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(OnPasswordCharChanged)));

        private static void OnPasswordCharChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {            
        }

        /// <summary>
        /// Gets or sets the password character.
        /// </summary>
        /// <value>
        /// The password character.
        /// </value>
        public char PasswordChar
        {
            get { return (char)GetValue(PasswordCharProperty); }
            set { SetValue(PasswordCharProperty, value); }
        }

        /// <summary>
        /// The password property
        /// </summary>
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeOfThis,
            new FrameworkPropertyMetadata(string.Empty));

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }                

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordBox"/> class.
        /// </summary>
        public PasswordBox()
            : base()
        {        
        }        
    }
}
