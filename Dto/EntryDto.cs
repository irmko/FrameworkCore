using SkyNET.Framework.Common.Interface;

namespace SkyNET.Framework.Common.Dto {
    public record EntryDto : IEntry {
        public int Id { get; set; }
    }

    public record EntryDto<TKey> : IEntry<TKey> {
        public TKey Id { get; set; }
    }
}