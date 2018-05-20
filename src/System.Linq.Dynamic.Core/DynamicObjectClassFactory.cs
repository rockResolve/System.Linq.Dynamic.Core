using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq.Dynamic.Core
{
    internal static class DynamicObjectClassFactory
    {
        internal static Expression CreateNewExpression(List<DynamicProperty> properties, List<Expression> expressions)
        {
            Type typeForKeyValuePair = typeof(KeyValuePair<string, object>);

#if NET35 || NET40
            ConstructorInfo constructorForKeyValuePair = typeForKeyValuePair.GetConstructors().First();
#else
            ConstructorInfo constructorForKeyValuePair = typeForKeyValuePair.GetTypeInfo().DeclaredConstructors.First();
#endif
            var arrayIndexParams = new List<Expression>();
            for (int i = 0; i < expressions.Count; i++)
            {
                // Just convert the expression always to an object expression.
                UnaryExpression boxingExpression = Expression.Convert(expressions[i], typeof(object));
                NewExpression parameter = Expression.New(constructorForKeyValuePair, (Expression)Expression.Constant(properties[i].Name), boxingExpression);

                arrayIndexParams.Add(parameter);
            }

            // Create an expression tree that represents creating and initializing a one-dimensional array of type KeyValuePair<string, object>.
            NewArrayExpression newArrayExpression = Expression.NewArrayInit(typeof(KeyValuePair<string, object>), arrayIndexParams);

            // Get the "public DynamicObjectClass(KeyValuePair<string, object>[] propertylist)" constructor
            Type targetType = typeof(DynamicObjectClass);
#if NET35 || NET40
            ConstructorInfo constructor = targetType.GetConstructors().First();
#else
            ConstructorInfo constructor = targetType.GetTypeInfo().DeclaredConstructors.First();
#endif
            return Expression.New(constructor, newArrayExpression);
        }
    }
}
