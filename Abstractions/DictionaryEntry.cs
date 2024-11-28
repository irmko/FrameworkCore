using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkyNET.Framework.Common.Interface;
using System.ComponentModel;

namespace SkyNET.Framework.Common.Abstractions;

/// <summary>
/// Базовый класс справочника
/// </summary>
public class DictionaryEntry : NamedEntry, IDictionaryEntry, IDictionaryEntry<int> {
    public DictionaryEntry(int id, string name, string code = null) : base(id, name) {
        Code = code;
    }

    public DictionaryEntry() : base() {
    }

    [Description("Код")]
    public string Code { get; set; }
}

/// <summary>
/// Базовый класс справочника с идентификатором типа TKey
/// </summary>
public class DictionaryEntry<TKey> : NamedEntry<TKey>, IDictionaryEntry<TKey> {
    [Description("Код")]
    public string Code { get; set; }

    public DictionaryEntry(TKey id, string name, string code) {
        Id = id;
        Name = name;
        Code = code;
    }

    public DictionaryEntry() {
    }
}

public static class DictionaryEntryConfiguration {
    public static void MapDictionaryEntry<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, IDictionaryEntry {
        builder.MapNamedEntry();
        builder.Property(x => x.Code).HasColumnName("code");
    }
}