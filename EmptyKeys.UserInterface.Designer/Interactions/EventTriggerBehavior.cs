using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmptyKeys.UserInterface.Designer.Interactions
{
    public class EventTriggerBehavior : Behavior
    {
        private static readonly Type typeOfThis = typeof(EventTriggerBehavior);

        /// <summary>
        /// Identifies the <seealso cref="Actions"/> dependency property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        internal static readonly DependencyProperty ActionsProperty = DependencyProperty.Register(
            "InternalActions",
            typeof(ActionCollection),
            typeOfThis,
            new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Identifies the <seealso cref="EventName"/> dependency property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly DependencyProperty EventNameProperty = DependencyProperty.Register(
            "EventName",
            typeof(string),
            typeOfThis,
            new FrameworkPropertyMetadata("Loaded"));

        /// <summary>
        /// Identifies the <seealso cref="SourceObject"/> dependency property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly DependencyProperty SourceObjectProperty = DependencyProperty.Register(
            "SourceObject",
            typeof(object),
            typeOfThis,
            new FrameworkPropertyMetadata(null));        

        /// <summary>
        /// Gets the collection of actions associated with the behavior. This is a dependency property.
        /// </summary>
        /// <value>
        /// The actions.
        /// </value>
        internal ActionCollection InternalActions
        {
            get
            {
                ActionCollection actionCollection = (ActionCollection)GetValue(EventTriggerBehavior.ActionsProperty);
                if (actionCollection == null)
                {
                    actionCollection = new ActionCollection();
                    SetValue(EventTriggerBehavior.ActionsProperty, actionCollection);
                }

                return actionCollection;
            }            
        }

        /// <summary>
        /// Gets the actions.
        /// </summary>
        /// <value>
        /// The actions.
        /// </value>
        public ActionCollection Actions
        {
            get
            {
                return this.InternalActions;
            }
        }

        /// <summary>
        /// Gets or sets the name of the event to listen for. This is a dependency property.
        /// </summary>
        /// <value>
        /// The name of the event.
        /// </value>
        public string EventName
        {
            get
            {
                return (string)GetValue(EventTriggerBehavior.EventNameProperty);
            }

            set
            {
                SetValue(EventTriggerBehavior.EventNameProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the source object from which this behavior listens for events.
        /// If <seealso cref="SourceObject" /> is not set, the source will default to <seealso cref="AssociatedObject" />. This is a dependency property.
        /// </summary>
        /// <value>
        /// The source object.
        /// </value>
        public object SourceObject
        {
            get
            {
                return (object)GetValue(EventTriggerBehavior.SourceObjectProperty);
            }

            set
            {
                SetValue(EventTriggerBehavior.SourceObjectProperty, value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventTriggerBehavior"/> class.
        /// </summary>
        public EventTriggerBehavior()            
        {
        }
    }
}
