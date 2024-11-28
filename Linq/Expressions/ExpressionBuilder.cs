using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace SkyNET.Framework.Common.Linq.Expressions {
    public static class ExpressionBuilder {
        private static readonly ConcurrentDictionary<Type, Func<IPredicateBuilder>> _predicateBuilderFactoryMap = new ConcurrentDictionary<Type, Func<IPredicateBuilder>>();

        public static IPredicateBuilder CreatePredicateBuilder(Type elementType) {
            if (elementType == null)
                throw new ArgumentNullException("elementType");

            var factory = _predicateBuilderFactoryMap.GetOrAdd(elementType, GeneratePredeicateBuilderFactory);
            return factory();
        }

        public static readonly ConstantExpression True = Expression.Constant(true, typeof(bool));

        public static readonly ConstantExpression False = Expression.Constant(false, typeof(bool));

        public static LambdaExpression TrueLambda(Type type) {
            return Expression.Lambda(typeof(Func<,>).MakeGenericType(type, typeof(bool)), True,
                Expression.Parameter(type));
        }

        public static Expression<Func<T, bool>> TrueLambda<T>() {
            return x => true;
        }

        public static bool IsTrue(ConstantExpression expression) {
            return expression != null && expression.Type == typeof(bool) && (bool)expression.Value;
        }

        public static bool IsTrue(LambdaExpression expression) {
            return IsTrue(expression.Body as ConstantExpression);
        }

        public static LambdaExpression FalseLambda(Type type) {
            return Expression.Lambda(typeof(Func<,>).MakeGenericType(type, typeof(bool)), False,
                Expression.Parameter(type));
        }

        public static Expression<Func<T, bool>> FalseLambda<T>() {
            return x => false;
        }

        public static bool IsFalse(ConstantExpression expression) {
            return expression != null && expression.Type == typeof(bool) && !(bool)expression.Value;
        }

        public static bool IsFalse(LambdaExpression expression) {
            return IsFalse(expression.Body as ConstantExpression);
        }

        public static Expression MakeWhere(Type elementType, Expression source, LambdaExpression predicate, bool isQueryable) {
            var type = isQueryable ? typeof(Queryable) : typeof(Enumerable);

            return Expression.Call(type, "Where", new[] { elementType }, new[] { source, predicate });
        }

        public static IQueryable MakeWhere(IQueryable source, LambdaExpression predicate) {
            var where = MakeWhere(source.ElementType, source.Expression, predicate, true);
            return source.Provider.CreateQuery(where);
        }

        public static Expression MakeSelect(Type elementType, Expression source, Type resultType, LambdaExpression selector, bool isQueryable) {
            var type = isQueryable ? typeof(Queryable) : typeof(Enumerable);

            return Expression.Call(type, "Select", new[] { elementType, resultType }, new[] { source, selector });
        }

        public static IQueryable MakeSelect(IQueryable source, Type resultType, LambdaExpression selector) {
            var select = MakeSelect(source.ElementType, source.Expression, resultType, selector, true);
            return source.Provider.CreateQuery(select);
        }

        public static Expression MakeAny(Type elementType, Expression source, LambdaExpression predicate, bool isQueryable) {
            var type = isQueryable ? typeof(Queryable) : typeof(Enumerable);

            var arguments = predicate != null ? new[] { source, predicate } : new[] { source };
            return Expression.Call(type, "Any", new[] { elementType }, arguments);
        }

        public static Expression MakeAll(Type elementType, Expression source, LambdaExpression predicate, bool isQueryable) {
            var type = isQueryable ? typeof(Queryable) : typeof(Enumerable);

            var arguments = predicate != null ? new[] { source, predicate } : new[] { source };
            return Expression.Call(type, "All", new[] { elementType }, arguments);
        }

        public static Expression MakeContains(Expression valueExpression, Array values) {
            return Expression.Call(
                typeof(Enumerable),
                "Contains",
                new[] { values.GetType().GetElementType() },
                Expression.Constant(values),
                valueExpression);
        }

        private static Func<IPredicateBuilder> GeneratePredeicateBuilderFactory(Type type) {
            var lambda = Expression.Lambda<Func<IPredicateBuilder>>(Expression.New(typeof(PredicateBuilder<>).MakeGenericType(type)));
            return lambda.Compile();
        }
    }
}