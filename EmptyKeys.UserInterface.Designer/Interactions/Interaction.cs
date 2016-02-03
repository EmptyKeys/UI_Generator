using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmptyKeys.UserInterface.Designer.Interactions
{
    public sealed class Interaction
    {
        private Interaction()
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="BehaviorCollection"/> associated with a specified object.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        internal static readonly DependencyProperty BehaviorsProperty = DependencyProperty.RegisterAttached(
            "InternalBehaviors",
            typeof(BehaviorCollection),
            typeof(Interaction));

        /// <summary>
        /// Gets the <see cref="BehaviorCollection" /> associated with a specified object.
        /// </summary>
        /// <param name="obj">The <see cref="EmptyKeys.UserInterface.DependencyObject" /> from which to retrieve the <see cref="BehaviorCollection" />.</param>
        /// <returns>
        /// A <see cref="BehaviorCollection" /> containing the behaviors associated with the specified object.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">obj</exception>
        internal static BehaviorCollection GetInternalBehaviors(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            BehaviorCollection behaviorCollection = (BehaviorCollection)obj.GetValue(Interaction.BehaviorsProperty);
            if (behaviorCollection == null)
            {
                behaviorCollection = new BehaviorCollection();
                obj.SetValue(Interaction.BehaviorsProperty, behaviorCollection);                
            }

            return behaviorCollection;
        }

        /// <summary>
        /// Gets the <see cref="BehaviorCollection" /> associated with a specified object.
        /// </summary>
        /// <param name="obj">The <see cref="EmptyKeys.UserInterface.DependencyObject" /> from which to retrieve the <see cref="BehaviorCollection" />.</param>
        /// <returns>
        /// A <see cref="BehaviorCollection" /> containing the behaviors associated with the specified object.
        /// </returns>
        public static BehaviorCollection GetBehaviors(DependencyObject obj)
        {
            return Interaction.GetInternalBehaviors(obj);
        }
    }
}
