using SkyNET.Framework.Common.Linq.Expressions;
using System.Linq.Expressions;

namespace SkyNET.Framework.Common.Linq {
    public partial class Predicate {
        public static Expression<Func<T, bool>> Search<T>(Expression<Func<T, string>> exp, string query, bool useAndCondition = false) {
            if (string.IsNullOrWhiteSpace(query))
                return PredicateBuilder.True<T>();

            var pd = new PredicateBuilder<T>();

            var property = QueryableExtensions.GetPropertyPath(exp);

            var inputWords = query.ToUpper().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in inputWords) {
                var lambda = QueryableExtensions.GetContainsExpression<T>(property.Item1, property.Item2, word);
                if (useAndCondition) {
                    pd.And(lambda);
                } else {
                    pd.Or(lambda);
                }
            }

            return pd.BuildOrTrue();
        }

        // <summary>
        // Поиск по нескольким свойствам, объединенным Or.
        // </summary>
        public static Expression<Func<T, bool>> Search<T>(string query, params Expression<Func<T, string>>[] expressions) {
            if (string.IsNullOrWhiteSpace(query))
                return PredicateBuilder.True<T>();

            var pd = new PredicateBuilder<T>();

            var inputWords = query.ToUpper().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in expressions) {
                var property = QueryableExtensions.GetPropertyPath(item);

                foreach (var word in inputWords) {
                    var lambda = QueryableExtensions.GetContainsExpression<T>(property.Item1, property.Item2, word);
                    pd.Or(lambda);
                }
            }

            return pd.BuildOrTrue();
        }

        // <summary>
        // Поиск по нескольким свойствам, объединенным Or.
        // Указывается - должно совпасть одно слово входящей строки или все, по умолчанию - одно.
        // </summary>
        public static Expression<Func<T, bool>> Search<T>(string query, bool useAndCondition = false, params Expression<Func<T, object>>[] expressions) {
            if (string.IsNullOrWhiteSpace(query))
                return PredicateBuilder.True<T>();

            var globalPd = new PredicateBuilder<T>();

            var inputWords = query.ToUpper().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in inputWords) {
                var expressionPd = new PredicateBuilder<T>();

                foreach (var item in expressions) {
                    var property = QueryableExtensions.GetPropertyPath(item);
                    if (property.Item2.PropertyType == typeof(string)) {
                        var lambda = QueryableExtensions.GetContainsExpression<T>(property.Item1, property.Item2, word);
                        expressionPd.Or(lambda);
                    } else if (property.Item2.PropertyType == typeof(int)) {
                        if (int.TryParse(word, out var v)) {
                            var lambda = QueryableExtensions.GetEqualsExpression<T, int>(property.Item1, v);
                            expressionPd.Or(lambda);
                        }
                    }
                }

                if (useAndCondition) {
                    globalPd.And(expressionPd.BuildOrTrue());
                } else {
                    globalPd.Or(expressionPd.BuildOrTrue());
                }
            }

            return globalPd.BuildOrTrue();
        }

        /// <summary>
        /// Для поиска не только по строковым полям
        /// </summary>
        public static Expression<Func<T, bool>> Search<T>(string query, bool useAndCondition = false, params string[] properties) {
            if (string.IsNullOrWhiteSpace(query))
                return PredicateBuilder.True<T>();

            var globalPd = new PredicateBuilder<T>();

            var inputWords = query.ToUpper().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in inputWords) {
                var expressionPd = new PredicateBuilder<T>();

                foreach (var propertyName in properties) {
                    var property = QueryableExtensions.GetPropertyInfo(typeof(T), propertyName);
                    var isDateProperty = property.PropertyType == typeof(DateTime);
                    var formattedWord = word;

                    if (isDateProperty) {
                        var dateParts = formattedWord.Split(".");
                        if (dateParts.Length > 1 && dateParts.Length < 4) {
                            dateParts = dateParts.SwapValues(0, dateParts.Length - 1);
                            formattedWord = string.Join('-', dateParts);
                        }
                    }

                    var lambda = QueryableExtensions.GetContainsExpression<T>(propertyName, property, !isDateProperty ? word : formattedWord);
                    expressionPd.Or(lambda);
                }

                if (useAndCondition)
                    globalPd.And(expressionPd.BuildOrTrue());
                else
                    globalPd.Or(expressionPd.BuildOrTrue());
            }

            return globalPd.BuildOrTrue();
        }

        public static Expression<Func<string, bool>> Search(string query) {
            if (string.IsNullOrWhiteSpace(query))
                return PredicateBuilder.True<string>();

            var pd = new PredicateBuilder<string>();

            var inputWords = query.ToUpper().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in inputWords) {
                pd.Or(o => o.ToUpper().Contains(word));
            }

            return pd.BuildOrTrue();
        }
    }
}