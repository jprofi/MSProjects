namespace Thinknet.Core
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static partial class Reflect
    {
        /// <summary>
        /// Gets the field represented by the lambda expression.
        /// </summary>
        /// <param name="field">
        /// An expression that accesses a field.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="field"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <paramref name="field"/> is not a lambda expression or it does not represent a field access.
        /// </exception>
        /// <typeparam name="TResult">
        /// The result type of the field.
        /// </typeparam>
        /// <returns>
        /// The field info.
        /// </returns>
        public static FieldInfo GetField<TResult>(Expression<Func<TResult>> field)
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
        public static MethodInfo GetMethod(Expression<Action> method)
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
        /// The passed arguments a method.
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
        public static MethodInfo GetMethod(Expression<Action> method, out object[] args)
        {
            return GetMethodInfo(method, out args);
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
        /// <typeparam name="TResult">
        /// The result type of the method.
        /// </typeparam>
        /// <returns>
        /// The method info.
        /// </returns>
        public static MethodInfo GetMethod<TResult>(Expression<Func<TResult>> method)
        {
            return GetMethodInfo(method);
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
        public static MethodInfo GetMethod(Expression<Func<Action>> method)
        {
            return GetDelegateMethodInfo(method);
        }

        /// <summary>
        /// Gets the method represented by the lambda expression.
        /// </summary>
        /// <typeparam name="TResult">
        /// The result type of the method.
        /// </typeparam>
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
        public static MethodInfo GetMethod<TResult>(Expression<Func<Func<TResult>>> method)
        {
            return GetDelegateMethodInfo(method);
        }

        /// <summary>
        /// The get parameter.
        /// </summary>
        /// <param name="reference">
        /// The reference.
        /// </param>
        /// <returns>
        /// The <see cref="MemberInfo"/>.
        /// </returns>
        public static MemberInfo GetParameter(Expression reference)
        {
            return GetMemberInfo(reference as LambdaExpression);
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
        public static PropertyInfo GetProperty<TResult>(Expression<Func<TResult>> property)
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
            if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                return ((MemberExpression)lambda.Body).Member;
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
