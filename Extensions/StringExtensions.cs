using System.Security.Cryptography;
using System.Text;

namespace SkyNET.Framework.Common.Extensions;
public static class StringExtensions {
    public static StringBuilder AppendIfNotNullOrEmpty(this StringBuilder builder, string? value, string? separator = null) {
        if (value.IsNullOrWhiteSpace())
            return builder;

        builder.Append($"{separator ?? ""}{value}");
        return builder;
    }

    public static bool IsNullOrEmpty(this string value) {
        return string.IsNullOrEmpty(value);
    }

    public static bool IsNullOrWhiteSpace(this string value) {
        return string.IsNullOrWhiteSpace(value);
    }

    public static string ToSha256Hash(this string value, int substringLenth) {
        if (substringLenth < 0)
            throw new ArgumentException($"{nameof(substringLenth)} < 0");

        return value.ToSha256Hash().Substring(0, substringLenth);
    }

    public static string ToSha256Hash(this string value) {
        var sb = new StringBuilder();

        using (var hash = SHA256.Create()) {
            var enc = Encoding.UTF8;
            var result = hash.ComputeHash(enc.GetBytes(value));

            foreach (var b in result)
                sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
    }

    public static string[] emptySplit = new string[] { "\r\n", "\n", " " };

    public static IEnumerable<string> SplitOnEmpty(this string value) {
        return value?.Split(emptySplit, StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
    }

    public static string CombineUri(this string uri1, string uri2) {
        uri1 = uri1.TrimEnd('/');
        uri2 = uri2.TrimStart('/');
        return $"{uri1}/{uri2}";
    }
}
