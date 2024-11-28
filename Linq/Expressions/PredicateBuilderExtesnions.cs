using System.Linq.Expressions;

namespace SkyNET.Framework.Common.Linq.Expressions {
    public static class PredicateBuilderExtesnions {
        public static Expression<Func<T, bool>> BuildOrTrue<T>(this IPredicateBuilder<T> builder) {
            return builder.Build() ?? ExpressionBuilder.TrueLambda<T>();
        }
    }
}