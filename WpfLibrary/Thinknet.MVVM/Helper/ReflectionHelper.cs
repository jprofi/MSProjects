namespace Thinknet.MVVM.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Helper class for reflection operations.
    /// </summary>
    public static class ReflectionHelper
    {
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
                    var ue = property.Body as UnaryExpression;
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
    }
}
