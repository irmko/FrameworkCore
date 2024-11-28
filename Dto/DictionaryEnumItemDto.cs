using SkyNET.Framework.Common.Interface;

namespace SkyNET.Framework.Common.Dto {
    /// <summary>
    /// Базовый интерфейс словаря (по enum'у)
    /// </summary>
    public record DictionaryEnumItemDto : INamedEntry<int> {
        public int Id { get; set; }

        public string Name { get; set; }

        public DictionaryEnumItemDto() {

        }

        public DictionaryEnumItemDto(int id, string name) {
            Id = id;
            Name = name;
        }
    }
}
