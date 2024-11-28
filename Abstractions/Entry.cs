using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkyNET.Framework.Common.Interface;
using System.ComponentModel;

namespace SkyNET.Framework.Common.Abstractions;
/// <summary>
/// Базовый класс моделей
/// </summary>
public abstract class Entry : Entry<int>, IEntry {
}

/// <summary>
/// Базовый класс моделей с идентификатором типа TKey
/// </summary>
public abstract class Entry<TKey> : IEntry<TKey> {
    [Description("Идентификатор")]
    public TKey Id { get; set; }
}

public static class EntryConfiguration {
    public static void MapEntry<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IEntry<int> {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
    }
}
