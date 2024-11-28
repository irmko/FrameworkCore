namespace SkyNET.Framework.Common.Interface; 

public interface ITreeItem<T> where T : ITreeItem<T> {
    ICollection<T> Items { get; set; }
}
