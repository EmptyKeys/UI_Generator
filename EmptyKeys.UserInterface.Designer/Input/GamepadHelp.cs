using System.Windows;

namespace EmptyKeys.UserInterface.Designer.Input
{
    /// <summary>
    /// Implements fake GamepadHelp class to support XAML designer
    /// </summary>
    public static class GamepadHelp
    {
        /// <summary>
        /// Get Target Name
        /// </summary>
        /// <param name="obj">dependency object</param>
        /// <returns></returns>
        public static string GetTargetName(DependencyObject obj)
        {
            return (string)obj.GetValue(TargetNameProperty);
        }

        /// <summary>
        /// Set Target Name
        /// </summary>
        /// <param name="obj">dependency object</param>
        /// <param name="value">value</param>
        public static void SetTargetName(DependencyObject obj, string value)
        {
            obj.SetValue(TargetNameProperty, value);
        }

        /// <summary>
        /// Target Name Property
        /// </summary>
        public static readonly DependencyProperty TargetNameProperty =
            DependencyProperty.RegisterAttached("TargetName", typeof(string), typeof(GamepadHelp), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Gets tab index left property
        /// </summary>
        /// <param name="obj">dependency object</param>
        /// <returns></returns>
        public static int GetTabIndexLeft(DependencyObject obj)
        {
            return (int)obj.GetValue(TabIndexLeftProperty);
        }

        /// <summary>
        /// Sets tab index left property
        /// </summary>
        /// <param name="obj">dependency object</param>
        /// <param name="value">value</param>
        public static void SetTabIndexLeft(DependencyObject obj, int value)
        {
            obj.SetValue(TabIndexLeftProperty, value);
        }

        /// <summary>
        /// Tab Index Left property
        /// </summary>
        public static readonly DependencyProperty TabIndexLeftProperty =
            DependencyProperty.RegisterAttached("TabIndexLeft", typeof(int), typeof(GamepadHelp), new PropertyMetadata(-1));

        /// <summary>
        /// Gets tab index right property
        /// </summary>
        /// <param name="obj">dependency object</param>
        /// <returns></returns>
        public static int GetTabIndexRight(DependencyObject obj)
        {
            return (int)obj.GetValue(TabIndexRightProperty);
        }

        /// <summary>
        /// Sets tab index right property
        /// </summary>
        /// <param name="obj">dependency object</param>
        /// <param name="value">value</param>
        public static void SetTabIndexRight(DependencyObject obj, int value)
        {
            obj.SetValue(TabIndexRightProperty, value);
        }

        /// <summary>
        /// Tab Index Right property
        /// </summary>
        public static readonly DependencyProperty TabIndexRightProperty =
            DependencyProperty.RegisterAttached("TabIndexRight", typeof(int), typeof(GamepadHelp), new PropertyMetadata(-1));

        /// <summary>
        /// Gets tab index up property
        /// </summary>
        /// <param name="obj">dependency object</param>
        /// <returns></returns>
        public static int GetTabIndexUp(DependencyObject obj)
        {
            return (int)obj.GetValue(TabIndexUpProperty);
        }

        /// <summary>
        /// Sets tab index up property
        /// </summary>
        /// <param name="obj">dependency object</param>
        /// <param name="value">value</param>
        public static void SetTabIndexUp(DependencyObject obj, int value)
        {
            obj.SetValue(TabIndexUpProperty, value);
        }

        /// <summary>
        /// Tab Index Up property
        /// </summary>
        public static readonly DependencyProperty TabIndexUpProperty =
            DependencyProperty.RegisterAttached("TabIndexUp", typeof(int), typeof(GamepadHelp), new PropertyMetadata(-1));

        /// <summary>
        /// Gets tab index down property
        /// </summary>
        /// <param name="obj">dependency object</param>
        /// <returns></returns>
        public static int GetTabIndexDown(DependencyObject obj)
        {
            return (int)obj.GetValue(TabIndexDownProperty);
        }

        /// <summary>
        /// Sets tab index down property
        /// </summary>
        /// <param name="obj">dependency object</param>
        /// <param name="value">value</param>
        public static void SetTabIndexDown(DependencyObject obj, int value)
        {
            obj.SetValue(TabIndexDownProperty, value);
        }

        /// <summary>
        /// Tab Index Down property
        /// </summary>
        public static readonly DependencyProperty TabIndexDownProperty =
            DependencyProperty.RegisterAttached("TabIndexDown", typeof(int), typeof(GamepadHelp), new PropertyMetadata(-1));
    }
}
