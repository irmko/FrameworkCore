namespace Quarta.Framework.Common.Extensions {
    public static class CollectionExtension
    {
        
        public static TimeSpan Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, TimeSpan> selector)
        {
            return source.Select(selector).Aggregate(TimeSpan.Zero, (t1, t2) => t1 + t2);
        }
        
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }

        public static IEnumerable<TResult> Combinations<TItem, TResult>(this IEnumerable<TItem> list,
            Func<TItem, TItem, TResult> combinator)
        {
            var temp = list.ToArray();
            for (var i = 0; i < temp.Length - 1; i++)
            for (var j = i + 1; j < temp.Length; j++)
                yield return combinator(temp[i], temp[j]);
        }

        public static bool AnySafe<T>(this IEnumerable<T> source)
        {
            return source != null && source.Any();
        }

        public static bool AnySafe<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return source != null && source.Any(predicate);
        }

        public static T AddIfNotNull<T, TI>(this T collection, TI item)
            where T : ICollection<TI>
            where TI : class
        {
            if (item != null)
            {
                collection.Add(item);
            }

            return collection;
        }
    }
}