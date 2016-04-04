using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmptyKeys.UserInterface.Designer.Interactions
{
    public class CallMethodAction : Action
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly DependencyProperty MethodNameProperty = DependencyProperty.Register(
            "MethodName",
            typeof(string),
            typeof(CallMethodAction),
            new FrameworkPropertyMetadata(null));

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly DependencyProperty TargetObjectProperty = DependencyProperty.Register(
            "TargetObject",
            typeof(object),
            typeof(CallMethodAction),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the name of the method to invoke. This is a dependency property.
        /// </summary>
        /// <value>
        /// The name of the method.
        /// </value>
        public string MethodName
        {
            get
            {
                return (string)GetValue(CallMethodAction.MethodNameProperty);
            }

            set
            {
                SetValue(CallMethodAction.MethodNameProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the object that exposes the method of interest. This is a dependency property.
        /// </summary>
        /// <value>
        /// The target object.
        /// </value>
        public object TargetObject
        {
            get
            {
                return GetValue(CallMethodAction.TargetObjectProperty);
            }

            set
            {
                SetValue(CallMethodAction.TargetObjectProperty, value);
            }
        }
    }
}
