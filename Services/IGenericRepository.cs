namespace SkyNET.Framework.Common.Services;
public interface IGenericRepository<TSrc> where TSrc : class {
    Task<int> Add(TSrc entity, CancellationToken cancellationToken);
    Task<int> Update(TSrc entity, CancellationToken cancellationToken);
    Task<int> Remove(TSrc entity, CancellationToken cancellationToken);
}
