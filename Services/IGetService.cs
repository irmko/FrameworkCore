namespace SkyNET.Framework.Common.Services;
public interface IGetService<TDto> {
    Task<TDto> GetSingleAsync(Guid id);
}

public interface IGetService<TDto, in TKey> {
    Task<TDto> GetSingleAsync(TKey id);
}
