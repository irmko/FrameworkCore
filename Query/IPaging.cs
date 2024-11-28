namespace SkyNET.Framework.Common.Query; 
public interface IPaging {
    int Skip { get; }
    int Take { get; }
    bool? TakeAll { get; set; }
}
