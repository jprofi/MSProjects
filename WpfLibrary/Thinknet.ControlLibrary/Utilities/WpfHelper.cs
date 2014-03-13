namespace Thinknet.ControlLibrary.Utilities
{
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Helper methods for wpf visual tree etc..
    /// </summary>
    public class WpfHelper
    {
        /// <summary>
        /// Finds the parent element of a specific type in the visual tree.
        /// </summary>
        /// <typeparam name="T">The type to find in the visual tree.</typeparam>
        /// <param name="dependencyObject">The dependency child to start navigating up.</param>
        /// <returns>The dependency object of the specific type or <code>null</code> if not existing.</returns>
        public static T FindAncestor<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            DependencyObject parent = GetParentDependencyObject(dependencyObject);

            if (parent == null)
            {
                return null;
            }

            T parentT = parent as T;
            return parentT ?? FindAncestor<T>(parent);
        }

        /// <summary>
        /// Gets the parent in the visual tree of a dependency object.
        /// Searches also FrameworkElements and ContentElements.
        /// </summary>
        /// <param name="child">The dependency child to parent lookup.</param>
        /// <returns>The parent or <code>null</code> if none.</returns>
        public static DependencyObject GetParentDependencyObject(DependencyObject child)
        {
            if (child == null)
            {
                return null;
            }

            //handle content elements separately
            ContentElement contentElement = child as ContentElement;
            if (contentElement != null)
            {
                DependencyObject parent = ContentOperations.GetParent(contentElement);
                if (parent != null)
                {
                    return parent;
                }

                FrameworkContentElement fce = contentElement as FrameworkContentElement;
                return fce != null ? fce.Parent : null;
            }

            //also try searching for parent in framework elements (such as DockPanel, etc)
            FrameworkElement frameworkElement = child as FrameworkElement;
            if (frameworkElement != null)
            {
                DependencyObject parent = frameworkElement.Parent;
                if (parent != null)
                {
                    return parent;
                }
            }

            //if it's not a ContentElement/FrameworkElement, rely on VisualTreeHelper
            return VisualTreeHelper.GetParent(child);
        }

        /// <summary>
        /// Gets the value of a dependency property in the visual tree.
        /// </summary>
        /// <param name="dependencyObject">The dependency object for search the dependency property.</param>
        /// <param name="dependencyProperty">The dependency property for getting the value.</param>
        /// <returns>The value.</returns>
        public static object GetDependencyPropertyValue(DependencyObject dependencyObject, DependencyProperty dependencyProperty)
        {
            if (dependencyObject != null)
            {
                object value = dependencyObject.GetValue(dependencyProperty);

                if (value != null)
                {
                    return value;
                }

                DependencyObject parent = GetParentDependencyObject(dependencyObject);
                return GetDependencyPropertyValue(parent, dependencyProperty);
            }

            return null;
        }
    }
}
