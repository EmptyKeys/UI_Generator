using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmptyKeys.UserInterface.Designer
{
    public class RoutedEvents : UIElement
    {
        /// <summary>
        /// The visible event
        /// </summary>
        public static readonly RoutedEvent VisibleEvent;

        /// <summary>
        /// The hidden event
        /// </summary>
        public static readonly RoutedEvent HiddenEvent;

        /// <summary>
        /// The collapsed event
        /// </summary>
        public static readonly RoutedEvent CollapsedEvent;

        static RoutedEvents()
        {
            VisibleEvent = EventManager.RegisterRoutedEvent("Visible", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(RoutedEvents));
            HiddenEvent = EventManager.RegisterRoutedEvent("Hidden", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(RoutedEvents));
            CollapsedEvent = EventManager.RegisterRoutedEvent("Collapsed", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(RoutedEvents));
        }
    }
}
