using SkyNET.Framework.Common.Interface;
using System.Text;

namespace SkyNET.Framework.Common.Extensions;

public static class HasFullNameExtensions {
    private record Model : IHasFullName {
        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string MiddleName { get; init; }
    }

    /// <summary>
    /// Фамилия И.О.
    /// </summary>
    public static string GetShortFIO(string lastName, string firstName, string middleName = null) => GetShortFIO(new Model {
        FirstName = firstName,
        MiddleName = middleName,
        LastName = lastName
    });

    /// <summary>
    /// Фамилия И.О.
    /// </summary>
    public static string GetShortFIO(this IHasFullName data) {
        if (data is null) {
            return string.Empty;
        }

        var builder = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(data.LastName)) {
            builder.Append(data.LastName);
        }

        if (builder.Length > 0) {
            builder.Append(' ');
        }

        if (!string.IsNullOrWhiteSpace(data.FirstName)) {
            builder.Append($"{data.FirstName[0]}.");
        }

        if (!string.IsNullOrWhiteSpace(data.MiddleName)) {
            builder.Append($"{data.MiddleName[0]}.");
        }

        return builder.ToString();
    }

    /// <summary>
    /// И.Фамилия | И. Фамилия (addGap = true)
    /// </summary>
    public static string GetShortIF(this IHasFullName data, bool addGap = false) {
        if (data is null) {
            return string.Empty;
        }

        if (string.IsNullOrWhiteSpace(data.FirstName)) {
            return data.LastName;
        }

        return addGap
            ? $"{data.FirstName[0]}.{data.LastName}"
            : $"{data.FirstName[0]}. {data.LastName}";
    }

    /// <summary>
    /// Фамилия И.
    /// </summary>
    public static string GetShortFI(this IHasFullName data) {
        if (data is null) {
            return string.Empty;
        }

        if (string.IsNullOrWhiteSpace(data.FirstName)) {
            return data.LastName;
        }

        return $"{data.LastName} {data.FirstName[0]}.";
    }

    /// <summary>
    /// Ф. И. 0.
    /// </summary>
    public static string GetInitialsFIO(this IHasFullName lg) {
        var builder = new StringBuilder();

        if (!string.IsNullOrWhiteSpace(lg.LastName)) {
            builder.Append($"{lg.LastName[..1].ToUpper()}. ");
        }

        if (!string.IsNullOrWhiteSpace(lg.FirstName)) {
            builder.Append($"{lg.FirstName[..1].ToUpper()}. ");
        }

        if (!string.IsNullOrWhiteSpace(lg.MiddleName)) {
            builder.Append($"{lg.MiddleName[..1].ToUpper()}.");
        }

        return builder.ToString().Trim();
    }

    /// <summary>
    /// И.О. Фамилия
    /// </summary>
    public static string GetShortIOF(this IHasFullName lg, bool addGapBeforeLastName = false) {
        if (lg == null) {
            return string.Empty;
        }

        if (string.IsNullOrWhiteSpace(lg.FirstName)) {
            return lg.LastName;
        }

        var firstName = $"{lg.FirstName[..1]}.";

        var middleName = string.IsNullOrWhiteSpace(lg.MiddleName)
            ? string.Empty
            : $"{lg.MiddleName[..1]}.";

        return $"{firstName}{middleName}{(addGapBeforeLastName ? " " : string.Empty)}{lg.LastName}";
    }

    /// <summary>
    /// Фамилия Имя Отчество
    /// </summary>
    public static string GetFullFIO(string lastName, string firstName, string middleName = null) {
        return string.Join(" ", new[]
        {
            lastName, firstName, middleName
        }.Where(name => !string.IsNullOrWhiteSpace(name)));
    }

    /// <summary>
    /// Фамилия Имя Отчество
    /// </summary>
    public static string GetFullFIO(this IHasFullName data) {
        return data == null
            ? string.Empty
            : GetFullFIO(data.LastName, data.FirstName, data.MiddleName);
    }

    /// <summary>
    /// Имя Отчество Фамилия  
    /// </summary>
    public static string GetFullIOF(this IHasFullName lg) {
        if (lg == null)
            return string.Empty;

        var builder = new StringBuilder();

        builder.Append($"{lg.FirstName} {lg.MiddleName} {lg.LastName}");

        return builder.ToString();
    }
}