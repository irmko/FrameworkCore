using SkyNET.Framework.Common.Extensions;

namespace SkyNET.Framework.Common.Query; 
public class Order {
    public string Column { get; set; }

    public string Dir { get; set; }

    public Order() {

    }

    public Order(string column, string dir) {
        Column = column;
        Dir = dir;
    }

    public override string ToString() {
        return string.Join(' ', new[] { Column, Dir }.Where(l => !l.IsNullOrEmpty()));
    }
}
