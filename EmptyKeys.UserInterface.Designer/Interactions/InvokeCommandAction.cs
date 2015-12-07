using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace EmptyKeys.UserInterface.Designer.Interactions
{
    public class InvokeCommandAction : Action
    {
        /// <summary>
        /// Identifies the <seealso cref="Command"/> dependency property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(InvokeCommandAction),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Identifies the <seealso cref="CommandParameter"/> dependency property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            "CommandParameter",
            typeof(object),
            typeof(InvokeCommandAction),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Identifies the <seealso cref="InputConverter"/> dependency property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly DependencyProperty InputConverterProperty = DependencyProperty.Register(
            "InputConverter",
            typeof(IValueConverter),
            typeof(InvokeCommandAction),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Identifies the <seealso cref="InputConverterParameter"/> dependency property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly DependencyProperty InputConverterParameterProperty = DependencyProperty.Register(
            "InputConverterParameter",
            typeof(object),
            typeof(InvokeCommandAction),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Identifies the <seealso cref="InputConverterLanguage"/> dependency property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly DependencyProperty InputConverterLanguageProperty = DependencyProperty.Register(
            "InputConverterLanguage",
            typeof(string),
            typeof(InvokeCommandAction),
            new FrameworkPropertyMetadata(string.Empty)); // Empty string means the invariant culture.

        /// <summary>
        /// Gets or sets the command this action should invoke. This is a dependency property.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(InvokeCommandAction.CommandProperty);
            }
            set
            {
                SetValue(InvokeCommandAction.CommandProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the command parameter.
        /// </summary>
        /// <value>
        /// The command parameter.
        /// </value>
        public object CommandParameter
        {
            get
            {
                return GetValue(InvokeCommandAction.CommandParameterProperty);
            }
            set
            {
                SetValue(InvokeCommandAction.CommandParameterProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the input converter.
        /// </summary>
        /// <value>
        /// The input converter.
        /// </value>
        public IValueConverter InputConverter
        {
            get
            {
                return (IValueConverter)GetValue(InvokeCommandAction.InputConverterProperty);
            }
            set
            {
                SetValue(InvokeCommandAction.InputConverterProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the input converter parameter.
        /// </summary>
        /// <value>
        /// The input converter parameter.
        /// </value>
        public object InputConverterParameter
        {
            get
            {
                return GetValue(InvokeCommandAction.InputConverterParameterProperty);
            }
            set
            {
                SetValue(InvokeCommandAction.InputConverterParameterProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the input converter language.
        /// </summary>
        /// <value>
        /// The input converter language.
        /// </value>
        public string InputConverterLanguage
        {
            get
            {
                return (string)GetValue(InvokeCommandAction.InputConverterLanguageProperty);
            }
            set
            {
                SetValue(InvokeCommandAction.InputConverterLanguageProperty, value);
            }
        }
    }
}
