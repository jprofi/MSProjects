namespace Thinknet.MVVM.Binding
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    using Thinknet.Core;

    /// <summary>
    /// This class serves as abstract base class for all classes in need of a PropertyChanged features
    /// </summary>
    public abstract class BindingBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Event occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Determine name of a property function.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="property">
        /// The property function to get the name from.
        /// </param>
        /// <returns>
        /// The name of the property function.
        /// </returns>
        public static string GetPropertyName<T>(Expression<Func<T>> property)
        {
            return Reflect.GetProperty(property).Name;
        }

        /// <summary>
        /// Gets the name of a property of a specific model.
        /// </summary>
        /// <typeparam name="TModel">The model type containing the property.</typeparam>
        /// <typeparam name="TProperty">The property type</typeparam>
        /// <param name="property">The property</param>
        /// <returns>The name of the property.</returns>
        public static string GetPropertyName<TModel, TProperty>(Expression<Func<TModel, TProperty>> property)
        {
            return Reflect<TModel>.GetProperty(property).Name;
        }

        /// <summary>
        /// Gets the whole access path of properties separated by dot for WPF Databinding.
        /// (E.g. Model.Measpoint.MeasVal).
        /// </summary>
        /// <typeparam name="TModel">The root model of the property path.</typeparam>
        /// <param name="property">The last property in the path.</param>
        /// <returns>The dot separted access path to the last property in path.</returns>
        public static string GetExpressionPath<TModel>(Expression<Func<TModel, object>> property) where TModel : class
        {
            List<string> propertyNames = new List<string>();
            MemberExpression me;
            switch (property.Body.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    UnaryExpression ue = property.Body as UnaryExpression;
                    me = ((ue != null) ? ue.Operand : null) as MemberExpression;
                    break;
                default:
                    me = property.Body as MemberExpression;
                    break;
            }

            while (me != null)
            {
                string propertyName = me.Member.Name;
                ////Type propertyType = me.Type;
                propertyNames.Add(propertyName);
                me = me.Expression as MemberExpression;
            }

            propertyNames.Reverse();
            string propertyPath = string.Join(".", propertyNames);
            return propertyPath;
        }        

        /// <summary>
        /// Notifies that all properties have changed and have to be updated in UI.
        /// Used for e.g. Language changes on viemodel level.
        /// </summary>
        protected void NotifyAllPropertyChanged()
        {
            NotifyPropertyChangedInternal(string.Empty);
        }

        /// <summary>
        /// Inform all registered event handlers that a property has changed.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="changedProperty">
        /// The property which has changed.
        /// </param>
        protected void NotifyPropertyChanged<T>(Expression<Func<T>> changedProperty)
        {
            string propertyName = GetPropertyName(changedProperty);
            NotifyPropertyChangedInternal(propertyName);
        }

        /// <summary>
        /// Raises a property change event without passing the callers method or property name.
        ///     Property name is automatically replaced by the compiler.
        /// </summary>
        /// <param name="propertyName">Holds the compiler generated name of the calling property or method.</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            NotifyPropertyChangedInternal(propertyName);
        }

        /// <summary>
        /// Inform all registered event handlers that a property has changed.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property which has been changed.
        /// </param>
        private void NotifyPropertyChangedInternal(string propertyName)
        {
            EventHelper.Raise(ref PropertyChanged, this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
