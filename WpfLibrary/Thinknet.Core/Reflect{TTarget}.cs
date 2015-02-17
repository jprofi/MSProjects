namespace Thinknet.Core
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Provides strong-typed <see langword="static"/> reflection of the <typeparamref name="TTarget"/>.
    /// </summary>
    /// <typeparam name="TTarget">
    /// Type to reflect.
    /// </typeparam>
    public static partial class Reflect<TTarget>
    {
        /// <summary>
        /// Gets the field represented by the lambda expression.
        /// </summary>
        /// <param name="field">
        /// An expression that accesses a field.
        /// </param>
        /// <typeparam name="TResult">
        /// The result type of the field.
        /// </typeparam>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="field"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <paramref name="field"/> is not a lambda expression or it does not represent a field access.
        /// </exception>
        /// <returns>
        /// The field info.
        /// </returns>
        public static FieldInfo GetField<TResult>(Expression<Func<TTarget, TResult>> field)
        {
            FieldInfo info = GetMemberInfo(field) as FieldInfo;
            if (info == null)
            {
                throw new ArgumentException("Member is not a field");
            }

            return info;
        }

        /// <summary>
        /// Gets the method represented by the lambda expression.
        /// </summary>
        /// <param name="method">
        /// An expression that invokes a method.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="method"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <paramref name="method"/> is not a lambda expression or it does not represent a method invocation.
        /// </exception>
        /// <returns>
        /// The method info.
        /// </returns>
        public static MethodInfo GetMethod(Expression<Action<TTarget>> method)
        {
            return GetMethodInfo(method);
        }

        /// <summary>
        /// Gets the method represented by the lambda expression.
        /// </summary>
        /// <param name="method">
        /// An expression that invokes a method.
        /// </param>
        /// <param name="args">
        /// An array of parameters of the method.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="method"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <paramref name="method"/> is not a lambda expression or it does not represent a method invocation.
        /// </exception>
        /// <returns>
        /// The method info.
        /// </returns>
        public static MethodInfo GetMethod(Expression<Action<TTarget>> method, out object[] args)
        {
            return GetMethodInfo(method, out args);
        }

        /// <summary>
        /// Gets the method represented by the lambda expression.
        /// </summary>
        /// <param name="method">
        /// An expression that invokes a method.
        /// </param>
        /// <typeparam name="TResult">
        /// The result type of the field.
        /// </typeparam>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="method"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <paramref name="method"/> is not a lambda expression or it does not represent a method invocation.
        /// </exception>
        /// <returns>
        /// The method info.
        /// </returns>
        public static MethodInfo GetMethod<TResult>(Expression<Func<TTarget, TResult>> method)
        {
            return GetMethodInfo(method);
        }

        /// <summary>
        /// Gets the method represented by the lambda expression.
        /// </summary>
        /// <param name="method">
        /// An expression that invokes a method.
        /// </param>
        /// <typeparam name="TResult">
        /// The result type of the field.
        /// </typeparam>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="method"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <paramref name="method"/> is not a lambda expression or it does not represent a method invocation.
        /// </exception>
        /// <returns>
        /// The method info.
        /// </returns>
        public static MethodInfo GetMethod<TResult>(Expression<Func<TTarget, Func<TResult>>> method)
        {
            return GetDelegateMethodInfo(method);
        }

        /// <summary>
        /// Gets the method represented by the lambda expression.
        /// </summary>
        /// <param name="method">
        /// An expression that invokes a method.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="method"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <paramref name="method"/> is not a lambda expression or it does not represent a method invocation.
        /// </exception>
        /// <returns>
        /// The method info.
        /// </returns>
        public static MethodInfo GetMethod(Expression<Func<TTarget, Action>> method)
        {
            return GetDelegateMethodInfo(method);
        }

        /// <summary>
        /// Gets the property represented by the lambda expression.
        /// </summary>
        /// <param name="property">
        /// An expression that accesses a property.
        /// </param>
        /// <typeparam name="TResult">
        /// The result type of the property.
        /// </typeparam>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="property"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <paramref name="property"/> is not a lambda expression or it does not represent a property access.
        /// </exception>
        /// <returns>
        /// The property info.
        /// </returns>
        public static PropertyInfo GetProperty<TResult>(Expression<Func<TTarget, TResult>> property)
        {
            PropertyInfo info = GetMemberInfo(property) as PropertyInfo;
            if (info == null)
            {
                throw new ArgumentException("Member is not a property");
            }

            return info;
        }

        private static object Evaluate(Expression operation)
        {
            object value;
            if (!TryEvaluate(operation, out value))
            {
                value = Expression.Lambda(operation).Compile().DynamicInvoke();
            }

            return value;
        }

        private static MethodInfo GetDelegateMethodInfo(LambdaExpression lambda)
        {
            if (lambda.Body.NodeType != ExpressionType.Convert)
            {
                throw new ArgumentException("Not a method reference", "lambda");
            }

            // Do we need all these checks here? The compiler always generates 
            // this same chain of calls for the given call pattern...
            Expression convertOperand = ((UnaryExpression)lambda.Body).Operand;

            if (convertOperand.NodeType != ExpressionType.Call)
            {
                throw new ArgumentException("Not a method reference", "lambda");
            }

            MethodCallExpression createDelegate = (MethodCallExpression)convertOperand;

            if (createDelegate.Arguments.Last().NodeType != ExpressionType.Constant)
            {
                throw new ArgumentException("Not a method reference", "lambda");
            }

            ConstantExpression methodRef = (ConstantExpression)createDelegate.Arguments.Last();

            if (!(methodRef.Value is MethodInfo))
            {
                throw new ArgumentException("Not a method reference", "lambda");
            }

            return (MethodInfo)methodRef.Value;
        }

        private static MemberInfo GetMemberInfo(LambdaExpression lambda)
        {
            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr =
                    ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr != null)
            {
                return memberExpr.Member;
            }

            throw new ArgumentException("Not a member access", "lambda");
        }

        private static MethodInfo GetMethodInfo(LambdaExpression lambda)
        {
            if (lambda.Body.NodeType != ExpressionType.Call)
            {
                throw new ArgumentException("Not a method call", "lambda");
            }

            return ((MethodCallExpression)lambda.Body).Method;
        }

        private static MethodInfo GetMethodInfo(LambdaExpression lambda, out object[] args)
        {
            if (lambda.Body.NodeType != ExpressionType.Call)
            {
                throw new ArgumentException("Not a method call", "lambda");
            }

            MethodCallExpression methodCallExpression = (MethodCallExpression)lambda.Body;

            args = new object[methodCallExpression.Arguments.Count];
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = Evaluate(methodCallExpression.Arguments[i]);
            }

            return methodCallExpression.Method;
        }

        private static bool TryEvaluate(Expression operation, out object value)
        {
            if (operation == null)
            {
                value = null;
                return true;
            }

            switch (operation.NodeType)
            {
                case ExpressionType.Constant:
                    value = ((ConstantExpression)operation).Value;
                    return true;
                case ExpressionType.MemberAccess:
                    MemberExpression me = (MemberExpression)operation;
                    object target;
                    if (TryEvaluate(me.Expression, out target))
                    {
                        switch (me.Member.MemberType)
                        {
                            case MemberTypes.Field:
                                value = ((FieldInfo)me.Member).GetValue(target);
                                return true;
                            case MemberTypes.Property:
                                value = ((PropertyInfo)me.Member).GetValue(target, null);
                                return true;
                        }
                    }

                    break;
            }

            value = null;
            return false;
        }
    }
}
