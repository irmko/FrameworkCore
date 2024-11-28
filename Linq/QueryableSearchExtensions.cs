using SkyNET.Framework.Common.Abstractions;
using SkyNET.Framework.Common.Interface;
using SkyNET.Framework.Common.Linq.Expressions;
using System.Linq.Expressions;

namespace SkyNET.Framework.Common.Linq {
    public static class QueryableSearchExtensions {
        public static IQueryable<T> Search<T>(this IQueryable<T> context, Expression<Func<T, string>> exp, string query, bool useAndCondition = false) {
            if (string.IsNullOrWhiteSpace(query))
                return context;

            return context.Where(Predicate.Search(exp, query, useAndCondition));
        }

        // <summary>
        // Поиск по нескольким свойствам, объединенным Or.
        // </summary>
        public static IQueryable<T> Search<T>(this IQueryable<T> context, string query, params Expression<Func<T, string>>[] expressions) {
            if (string.IsNullOrWhiteSpace(query))
                return context;

            return context.Where(Predicate.Search(query, expressions));
        }

        // <summary>
        // Поиск по нескольким свойствам, объединенным Or.
        // Указывается - должно совпасть одно слово входящей строки или все, по умолчанию - одно.
        // </summary>
        public static IQueryable<T> Search<T>(this IQueryable<T> context, string query, bool useAndCondition = false, params Expression<Func<T, object>>[] expressions) {
            if (string.IsNullOrWhiteSpace(query))
                return context;

            return context.Where(Predicate.Search(query, useAndCondition, expressions));
        }

        // <summary>
        // Поиск по нескольким свойствам, объединенным Or.
        // Возможность указать две поисковой строки(актуально, когда делаем транслит)
        // Указывается - должно совпасть одно слово входящей строки или все, по умолчанию - одно.
        // </summary>
        public static IQueryable<T> Search<T>(this IQueryable<T> context, string query, string nextQuery, bool useAndCondition = false, params Expression<Func<T, object>>[] expressions) {
            if (string.IsNullOrWhiteSpace(query) && string.IsNullOrWhiteSpace(nextQuery))
                return context;

            var common = new PredicateBuilder<T>();
            common.Or(Predicate.Search(query, useAndCondition, expressions));
            common.Or(Predicate.Search(nextQuery, useAndCondition, expressions));
            return context.Where(common.BuildOrTrue());
        }

        /// <summary>
        /// Для поиска не только по строковым полям
        /// </summary>
        public static IQueryable<T> Search<T>(this IQueryable<T> context, string query, bool useAndCondition = false, params string[] properties) {
            if (string.IsNullOrWhiteSpace(query))
                return context;

            return context.Where(Predicate.Search<T>(query, useAndCondition, properties));
        }

        public static IQueryable<string> Search(this IQueryable<string> context, string query) {
            if (string.IsNullOrWhiteSpace(query))
                return context;

            return context.Where(Predicate.Search(query));
        }

        // <summary>
        // Поиск по справочнику
        // </summary>
        public static IQueryable<T> SearchDictionaryItems<T>(this IQueryable<T> context, string query, params Expression<Func<T, string>>[] expressions)
            where T : NamedEntry {
            if (!expressions.Any()) {
                expressions = new Expression<Func<T, string>>[]
                {
                    o => o.Name
                };
            }

            return Search(context, query, expressions);
        }

        public static IQueryable<T> SearchDictionaryEntry<T>(this IQueryable<T> context, string query)
            where T : IDictionaryEntry {
            var pd = new PredicateBuilder<T>();
            if (!string.IsNullOrEmpty(query)) {
                var inputWords = query.ToUpper().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var word in inputWords) {
                    pd.Or(o => o.Name.ToUpper().Contains(word));
                }

                context = context.Where(pd.BuildOrTrue());
            }
            return context;
        }

        public static IQueryable<T> SearchByDictionaryName<T>(this IQueryable<T> context, string query)
            where T : NamedEntry {
            var pd = new PredicateBuilder<T>();
            if (!string.IsNullOrEmpty(query)) {
                var inputWords = query.ToUpper().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var word in inputWords) {
                    pd.Or(o => o.Name.ToUpper().Contains(word));
                }

                context = context.Where(pd.BuildOrTrue());
            }
            return context;
        }

        public static IQueryable<T> Search<T>(this IQueryable<T> context, string query) where T : DictionaryEntry {
            var pd = new PredicateBuilder<T>();
            if (!string.IsNullOrEmpty(query)) {
                var inputWords = query.ToUpper().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var word in inputWords) {
                    pd.Or(o => o.Name.ToUpper().Contains(word)).Or(o => o.Code.Contains(word));
                }

                context = context.Where(pd.BuildOrTrue());
            }

            return context;
        }
    }
}
