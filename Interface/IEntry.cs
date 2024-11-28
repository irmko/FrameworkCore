namespace SkyNET.Framework.Common.Interface;

/// <summary>
/// Базовый интерфейс моделей
/// </summary>
public interface IEntry : IEntry<int> {
}

public interface IEntryGuid : IEntry<Guid> {
}

/// <summary>
/// Базовый интерфейс моделей с идентификатором типа TKey
/// </summary>
public interface IEntry<TKey> {
    TKey Id { get; set; }
}