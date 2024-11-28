using SkyNET.Framework.Common.Interface;

namespace SkyNET.Framework.Common.Dto {
    /// <summary>
    /// Базовый dto словаря
    /// </summary>
    public record NamedEntryDto : INamedEntry {
        public int Id { get; set; }

        public string Name { get; set; }

        public NamedEntryDto() {

        }

        public NamedEntryDto(int id, string name) {
            Id = id;
            Name = name;
        }
    }

    /// <summary>
    /// Базовый dto словаря
    /// </summary>
    public record NamedEntryDto<TKey> : INamedEntry<TKey> {
        public TKey Id { get; set; }

        public string Name { get; set; }

        public NamedEntryDto() {

        }

        public NamedEntryDto(TKey id, string name) {
            Id = id;
            Name = name;
        }
    }
}
