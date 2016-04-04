using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmptyKeys.UserInterface.Designer.Interactions
{
    public class ChangePropertyAction : Action
    {
        /// <summary>
        /// Identifies the <seealso cref="PropertyName"/> dependency property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register(
            "PropertyName",
            typeof(PropertyPath),
            typeof(ChangePropertyAction),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Identifies the <seealso cref="TargetObject"/> dependency property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly DependencyProperty TargetObjectProperty = DependencyProperty.Register(
            "TargetObject",
            typeof(object),
            typeof(ChangePropertyAction),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Identifies the <seealso cref="Value"/> dependency property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(object),
            typeof(ChangePropertyAction),
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the name of the property to change. This is a dependency property.
        /// </summary>
        public PropertyPath PropertyName
        {
            get
            {
                return (PropertyPath)GetValue(ChangePropertyAction.PropertyNameProperty);
            }
            set
            {
                SetValue(ChangePropertyAction.PropertyNameProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the value to set. This is a dependency property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public object Value
        {
            get
            {
                return GetValue(ChangePropertyAction.ValueProperty);
            }
            set
            {
                SetValue(ChangePropertyAction.ValueProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the object whose property will be changed.
        /// If <seealso cref="TargetObject"/> is not set or cannot be resolved, the sender of <seealso cref="Execute"/> will be used. This is a dependency property.
        /// </summary>
        public object TargetObject
        {
            get
            {
                return (object)GetValue(ChangePropertyAction.TargetObjectProperty);
            }
            set
            {
                SetValue(ChangePropertyAction.TargetObjectProperty, value);
            }
        }

        public ChangePropertyAction()
            : base()
        {
        }
    }
}
