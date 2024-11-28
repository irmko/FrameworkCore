namespace SkyNET.Framework.Common.Interface; 

public interface IHasFullName {
    string FirstName { get; }

    string LastName { get; }

    string MiddleName { get; }
}