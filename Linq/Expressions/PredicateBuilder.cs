using System.Linq.Expressions;

namespace SkyNET.Framework.Common.Linq.Expressions {
    /// <summary>
    /// Постороитель Expression-ов
    /// Обертка над PredicateBuilder<TEnt> для короткой записи
    /// </summary>
    public static class PredicateBuilder {
        public static Expression<Func<TEnt, bool>> And<TEnt>(params Expression<Func<TEnt, bool>>[] conditions) {
            var pb = new PredicateBuilder<TEnt>();
            foreach (var cond in conditions)
                pb.And(cond);

            return pb.Build();
        }

        public static Expression<Func<TEnt, bool>> Or<TEnt>(params Expression<Func<TEnt, bool>>[] conditions) {
            var conds = conditions?.Where(cc => cc != null)?.ToArray();
            if (conds?.Any() != true)
                return null;

            var pb = new PredicateBuilder<TEnt>();
            foreach (var cond in conds) {
                if (cond != null)
                    pb.Or(cond);
            }

            return pb.Build();
        }

        public static Expression<Func<TEnt, bool>> True<TEnt>() {
            return ExpressionBuilder.TrueLambda<TEnt>();
        }
    }
}
