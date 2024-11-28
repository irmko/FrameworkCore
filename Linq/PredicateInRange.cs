using SkyNET.Framework.Common.Linq.Expressions;
using System.Linq.Expressions;

namespace SkyNET.Framework.Common.Linq;
/// <summary>
/// Строит запросы для поиска объектов в заданном дипазоне
/// </summary>
public partial class Predicate
{
    /// <summary>
    /// Найти объекты у которых {field} между {from} и {to}
    /// </summary>
    public static Expression<Func<TEnt, bool>> WhereInRange<TEnt, TProp>(
        Expression<Func<TEnt, TProp?>> field,
        TProp? from, TProp? to,
        bool excludeFrom = false,
        bool excludeTo = false)
        where TEnt : class
        where TProp : struct
    {
        if (!from.HasValue && !to.HasValue)
            return ExpressionBuilder.TrueLambda<TEnt>();

        return PredicateBuilder.And(
            field.HasVal(),
            ExprCreate(
                from: from,
                to: to,
                toExprFunc: () => ToExpr(field, to, excludeFrom),
                fromExprFunc: () => FromExpr(field, from, excludeTo)));
    }

    /// <summary>
    /// Найти объекты у которых {field} между {from} и {to}
    /// </summary>
    public static Expression<Func<TEnt, bool>> WhereInRange<TEnt, TProp>(
        Expression<Func<TEnt, TProp>> field,
        TProp? from, TProp? to,
        bool excludeFrom = false,
        bool excludeTo = false)
        where TEnt : class
        where TProp : struct
    {
        return ExprCreate(
            from: from,
            to: to,
            toExprFunc: () => ToExpr(field, to, excludeFrom),
            fromExprFunc: () => FromExpr(field, from, excludeTo));
    }
}
