using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EmptyKeys.UserInterface.Designer
{
    /// <summary>
    /// Implements WPF design class for Event Trigger attached properties
    /// </summary>
    public static class EventTrigger
    {
        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        /// <summary>
        /// Sets the command.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        /// <summary>
        /// The command path property
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(EventTrigger),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets the command path.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string GetCommandPath(DependencyObject obj)
        {
            return (string)obj.GetValue(CommandPathProperty);
        }

        /// <summary>
        /// Sets the command path.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetCommandPath(DependencyObject obj, string value)
        {
            obj.SetValue(CommandPathProperty, value);
        }

        /// <summary>
        /// The command path property
        /// </summary>
        public static readonly DependencyProperty CommandPathProperty =
            DependencyProperty.RegisterAttached("CommandPath", typeof(string), typeof(EventTrigger),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets the command parameter.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static object GetCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(CommandParameterProperty);
        }

        /// <summary>
        /// Sets the command parameter.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// The command parameter property
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(EventTrigger), 
            new FrameworkPropertyMetadata(null));        
    }
}
