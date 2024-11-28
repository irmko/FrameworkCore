using System.Linq.Expressions;

namespace SkyNET.Framework.Common.Linq.Expressions {
    public sealed class PredicateBuilder<T> : IPredicateBuilder<T> {
        #region Nested types

        private class ReplaceParameterVisitor : ExpressionVisitor {
            public ParameterExpression New { private get; set; }

            public ParameterExpression Original { private get; set; }

            protected override Expression VisitParameter(ParameterExpression node) {
                return ReferenceEquals(node, Original) ? New : base.VisitParameter(node);
            }
        }

        #endregion

        #region Private fields

        private ReplaceParameterVisitor _visitor;

        private ParameterExpression _parameter;
        private Expression _body;

        private Expression<Func<T, bool>> _standalone;

        #endregion

        #region Public methods

        public PredicateBuilder<T> Or(Expression<Func<T, bool>> expression) {
            Add(expression, Expression.OrElse);
            return this;
        }

        public PredicateBuilder<T> OrNot(Expression<Func<T, bool>> expression) {
            var c = Expression.Lambda<Func<T, bool>>(Expression.Not(expression.Body), expression.Parameters);
            Add(c, Expression.OrElse);
            return this;
        }

        public PredicateBuilder<T> And(Expression<Func<T, bool>> expression) {
            Add(expression, Expression.AndAlso);
            return this;
        }

        public PredicateBuilder<T> AndNot(Expression<Func<T, bool>> expression) {
            var c = Expression.Lambda<Func<T, bool>>(Expression.Not(expression.Body), expression.Parameters);
            Add(c, Expression.AndAlso);
            return this;
        }

        public Expression<Func<T, bool>> Build() {
            return _body != null && _parameter != null ? Expression.Lambda<Func<T, bool>>(_body, _parameter) : _standalone;
        }

        IPredicateBuilder<T> IPredicateBuilder<T>.Or(Expression<Func<T, bool>> expression) {
            return Or(expression);
        }

        IPredicateBuilder<T> IPredicateBuilder<T>.And(Expression<Func<T, bool>> expression) {
            return And(expression);
        }

        IPredicateBuilder<T> IPredicateBuilder<T>.AndNot(Expression<Func<T, bool>> expression) {
            return AndNot(expression);
        }

        IPredicateBuilder<T> IPredicateBuilder<T>.OrNot(Expression<Func<T, bool>> expression) {
            return OrNot(expression);
        }

        IPredicateBuilder IPredicateBuilder.Or(LambdaExpression expression) {
            return Or((Expression<Func<T, bool>>)expression);
        }

        IPredicateBuilder IPredicateBuilder.And(LambdaExpression expression) {
            return And((Expression<Func<T, bool>>)expression);
        }

        LambdaExpression IPredicateBuilder.Build() {
            return Build();
        }

        #endregion

        #region Private methods

        private void Add(Expression<Func<T, bool>> expression, Func<Expression, Expression, Expression> aggregate) {
            if (_standalone == null) {
                _standalone = expression;
                return;
            }

            if (_parameter == null)
                _parameter = Expression.Parameter(typeof(T));

            if (_visitor == null)
                _visitor = new ReplaceParameterVisitor { New = _parameter };

            if (_body == null) {
                _visitor.Original = _standalone.Parameters.Single();
                _body = _visitor.Visit(_standalone.Body);
            }

            _visitor.Original = expression.Parameters.Single();

            _body = aggregate(_body, _visitor.Visit(expression.Body));
        }


        #endregion
    }
}