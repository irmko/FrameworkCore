using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace SkyNET.Framework.Common.Linq {
    public static class QueryableExtensions {
        internal static PropertyInfo GetPropertyInfo(Type baseType, string propertyName) {
            var parts = propertyName.Split('.');

            for (var i = 0; i < parts.Length; i++)
                parts[i] = char.ToUpper(parts[i][0]) + parts[i][1..];

            return parts.Length > 1
                ? GetPropertyInfo(baseType.GetProperty(parts[0])?.PropertyType, parts.Skip(1).Aggregate((a, i) => a + "." + i))
                : baseType.GetProperty(parts[0]);
        }

        internal static T[] SwapValues<T>(this T[] source, long index1, long index2) {
            (source[index1], source[index2]) = (source[index2], source[index1]);
            return source;
        }

        internal static Expression<Func<T, bool>> GetContainsExpression<T>(string propertyName, PropertyInfo property, string propertyValue) {
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            MethodInfo toUpperMethod = typeof(string).GetMethod("ToUpper", new Type[0]);


            var parameterExp = Expression.Parameter(typeof(T), "type");
            var propertyExp = BuildPropertyExpression(propertyName, parameterExp);

            MethodCallExpression toUpperExpr;
            if (property.PropertyType != typeof(string)) {
                var toStringMethod = property.PropertyType.GetMethod("ToString", new Type[0]);
                var toStringExp = Expression.Call(propertyExp, toStringMethod);
                toUpperExpr = Expression.Call(toStringExp, toUpperMethod);
            } else {
                toUpperExpr = Expression.Call(propertyExp, toUpperMethod);
            }

            var someValue = Expression.Constant(propertyValue, typeof(string));
            var containsMethodExp = Expression.Call(toUpperExpr, method, someValue);

            return Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);
        }

        internal static Expression<Func<T, bool>> GetEqualsExpression<T, TK>(string propertyName, TK propertyValue) {
            var parameterExp = Expression.Parameter(typeof(T), "type");
            var propertyExp = BuildPropertyExpression(propertyName, parameterExp);

            var someValue = Expression.Convert(Expression.Constant(propertyValue), propertyExp.Type);
            var containsMethodExp = Expression.Equal(propertyExp, someValue);

            return Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);
        }

        internal static Expression<Func<T, bool>> GetContainsExpression<T, TK>(string propertyName, IEnumerable<TK> propertyValue) {
            var collectionType = typeof(IEnumerable<>);
            MethodInfo containsMethod = typeof(Enumerable).GetMethods()
                .Where(m => m.Name == nameof(Enumerable.Contains))
                .Single(m => m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(TK));

            var parameterExp = Expression.Parameter(typeof(T), "type");
            var propertyExp = BuildPropertyExpression(propertyName, parameterExp);

            var someValue = Expression.Constant(propertyValue);

            var containsMethodExp = Expression.Call(containsMethod, someValue, propertyExp);  //Expression.Call(propertyExp, containsMethod, someValue);

            return Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);
        }

        internal static Expression BuildPropertyExpression(string propertyName, ParameterExpression parameterExp) {
            Expression propertyExp = parameterExp;
            foreach (var member in propertyName.Split('.')) {
                propertyExp = Expression.PropertyOrField(propertyExp, member);
            }

            return propertyExp;
        }

        internal static PropertyInfo GetPropertyInfo<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyLambda) {
            Type type = typeof(TSource);

            if (!(propertyLambda.Body is MemberExpression member)) {
                throw new ArgumentException(
                    $"Expression '{propertyLambda}' refers to a method, not a property.");
            }

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null) {
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a field, not a property.");
            }

            if (type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType) &&
                !propInfo.ReflectedType.IsAssignableFrom(type)) {
                throw new ArgumentException(
                    $"Expression '{propertyLambda.ToString()}' refers to a property that is not from type {type}.");
            }

            return propInfo;
        }

        internal static MemberExpression GetMemberExpression(Expression expression) {
            if (expression is MemberExpression) {
                return (MemberExpression)expression;
            } else if (expression is LambdaExpression) {
                var lambdaExpression = expression as LambdaExpression;
                if (lambdaExpression.Body is MemberExpression) {
                    return (MemberExpression)lambdaExpression.Body;
                } else if (lambdaExpression.Body is UnaryExpression) {
                    return (MemberExpression)((UnaryExpression)lambdaExpression.Body).Operand;
                }
            }
            return null;
        }

        internal static (string, PropertyInfo) GetPropertyPath(Expression expr) {
            var path = new StringBuilder();
            PropertyInfo info = null;
            MemberExpression memberExpression = GetMemberExpression(expr);
            do {
                if (path.Length > 0) {
                    path.Insert(0, ".");
                } else {
                    // TODO  разобраться
                    info = memberExpression.Member as PropertyInfo;
                }
                path.Insert(0, memberExpression.Member.Name);
                memberExpression = GetMemberExpression(memberExpression.Expression);
            }
            while (memberExpression != null);
            return (path.ToString(), info);
        }

    }
}
