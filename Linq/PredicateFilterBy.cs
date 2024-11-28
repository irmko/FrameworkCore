using SkyNET.Framework.Common.Interface;
using SkyNET.Framework.Common.Linq.Expressions;
using System.Linq.Expressions;

namespace SkyNET.Framework.Common.Linq;
public partial class Predicate
{
    public static Expression<Func<T, bool>> FilterBy<T,TK>(Expression<Func<T, TK>> fieldExpr, TK entry)
    {
        if (entry == null)
            return PredicateBuilder.True<T>();
        
        var property = QueryableExtensions.GetPropertyPath(fieldExpr);
        return QueryableExtensions.GetEqualsExpression<T, TK>(property.Item1, entry);
    }
    
    public static Expression<Func<T, bool>> FilterBy<T>(Expression<Func<T, Guid?>> fieldExpr, Guid entry)
    {
        return FilterBy(fieldExpr, (Guid?)entry);
    }
    
    public static Expression<Func<T, bool>> FilterBy<T>(Expression<Func<T, Guid?>> fieldExpr, Guid? entry)
    {
        return FilterBy<T, Guid?>(fieldExpr, entry);
    }

    public static Expression<Func<T, bool>> FilterBy<T, TK>(Expression<Func<T, TK>> fieldExpr, ICollection<TK> entries)
    {
        if (entries == null || !entries.Any())
            return PredicateBuilder.True<T>();
        
        var property = QueryableExtensions.GetPropertyPath(fieldExpr);
        return QueryableExtensions.GetContainsExpression<T, TK>(property.Item1, entries);
    }
    
    public static Expression<Func<T, bool>> FilterBy<T, TM, TK>(Expression<Func<T, TK>> fieldExpr, ICollection<TM> entries) 
        where TM : IEntry<TK>
    {
        if (entries == null || !entries.Any())
            return PredicateBuilder.True<T>();
        
        var ids = entries?.Select(o => o.Id).ToList();
        var property = QueryableExtensions.GetPropertyPath(fieldExpr);
        return QueryableExtensions.GetContainsExpression<T, TK>(property.Item1, ids);
    }
    
    public static Expression<Func<T, bool>> FilterBy<T, TM>(Expression<Func<T, Guid?>> fieldExpr, ICollection<TM> entries) 
        where TM : IEntry
    {
        if (entries == null || !entries.Any())
            return PredicateBuilder.True<T>();
        
        var ids = entries?.Select(o => o.Id).Cast<Guid?>().ToList();
        
        var property = QueryableExtensions.GetPropertyPath(fieldExpr);
        return QueryableExtensions.GetContainsExpression<T, Guid?>(property.Item1, ids);
    }
    
    public static Expression<Func<T, bool>> FilterBy<T>(Expression<Func<T, Guid?>> fieldExpr, ICollection<Guid> entries)
    {
        if (entries == null || !entries.Any())
            return PredicateBuilder.True<T>();

        var ids = entries.Cast<Guid?>().ToList();
        var property = QueryableExtensions.GetPropertyPath(fieldExpr);
        
        return QueryableExtensions.GetContainsExpression<T, Guid?>(property.Item1, ids);
    }
}