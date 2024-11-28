using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkyNET.Framework.Common.Interface;
using System.ComponentModel;

namespace SkyNET.Framework.Common.Abstractions;
/// <summary>
/// Базовый класс словаря
/// </summary>
public class NamedEntry : Entry, INamedEntry {
    [Description("Наименование")]
    public string Name { get; set; }

    public NamedEntry(int id, string name) {
        Id = id;
        Name = name;
    }

    public NamedEntry() {
    }
}

/// <summary>
/// Базовый класс словаря с идентификатором типа TKey
/// </summary>
public class NamedEntry<TKey> : Entry<TKey>, INamedEntry<TKey> {
    [Description("Наименование")]
    public string Name { get; set; }

    public NamedEntry(TKey id, string name) {
        Id = id;
        Name = name;
    }

    public NamedEntry() {
    }
}

public static class NamedEntryConfiguration {
    public static void MapNamedEntry<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, INamedEntry {
        builder.MapEntry();
        builder.Property(x => x.Name).HasColumnName("name");
    }
}