using System;

namespace SkyNET.Framework.Common.Interface; 

/// <summary>
/// Базовый интерфейс справочника
/// </summary>
public interface IDictionaryEntry : INamedEntry {
    /// <summary>
    /// Код.
    /// </summary>
    string Code { get; set; }
}

/// <summary>
/// Базовый интерфейс справочника с идентификатором типа TKey
/// </summary>
public interface IDictionaryEntry<TKey> : INamedEntry<TKey> {
    /// <summary>
    /// Код.
    /// </summary>
    string Code { get; set; }
}