namespace SkyNET.Framework.Common.Interface;

/// <summary>
/// Базовый интерфейс словаря
/// </summary>
public interface INamedEntry : IEntry {
    string Name { get; set; }
}

/// <summary>
/// Базовый интерфейс словаря с идентификатором типа TKey
/// </summary>
public interface INamedEntry<TKey> : IEntry<TKey> {
    string Name { get; set; }
}
