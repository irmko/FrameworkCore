using System.Linq.Expressions;

namespace SkyNET.Framework.Common.Linq.Expressions {
    public interface IPredicateBuilder {
        IPredicateBuilder Or(LambdaExpression expression);

        IPredicateBuilder And(LambdaExpression expression);

        LambdaExpression Build();
    }

    public interface IPredicateBuilder<T> : IPredicateBuilder {
        IPredicateBuilder<T> Or(Expression<Func<T, bool>> expression);

        IPredicateBuilder<T> And(Expression<Func<T, bool>> expression);

        IPredicateBuilder<T> AndNot(Expression<Func<T, bool>> expression);

        IPredicateBuilder<T> OrNot(Expression<Func<T, bool>> expression);

        new Expression<Func<T, bool>> Build();
    }
}