namespace SkyNET.Framework.Common.Dto {
    public struct ModificationResult<T> {
        public ICollection<T> Added { get; init; }

        public ICollection<T> Updated { get; init; }

        public ICollection<T> Deleted { get; init; }

        public bool Success => Added.Any() || Updated.Any() || Deleted.Any();

        public ModificationResult() {
            Added = new List<T>();
            Updated = new List<T>();
            Deleted = new List<T>();
        }

        public void Add(params T[] items) {
            foreach (var item in items) {
                Added.Add(item);
            }
        }

        public void Remove(params T[] items) {
            foreach (var item in items) {
                Deleted.Add(item);
            }
        }

        public void Update(params T[] items) {
            foreach (var item in items) {
                Updated.Add(item);
            }
        }
    }
}