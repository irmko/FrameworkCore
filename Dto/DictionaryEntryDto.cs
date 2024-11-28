using SkyNET.Framework.Common.Interface;

namespace SkyNET.Framework.Common.Dto {
    /// <summary>
    /// Базовый dto словаря с кодом
    /// </summary>
    public record DictionaryEntryDto : NamedEntryDto<int>, IDictionaryEntry<int> {
        //public Dictionary<string, object> Property { get; set; }
        public string Code { get; set; }

        public DictionaryEntryDto() {

        }

        public DictionaryEntryDto(int id, string name, string code = null) {
            Id = id;
            Name = name;
            Code = code;
        }
    }

    public record DictionaryEntryDto<TKey> : NamedEntryDto<TKey>, IDictionaryEntry<TKey> {
        public string Code { get; set; }

        public DictionaryEntryDto() {

        }

        public DictionaryEntryDto(TKey id, string name, string code = null) {
            Id = id;
            Name = name;
            Code = code;
        }
    }
}
