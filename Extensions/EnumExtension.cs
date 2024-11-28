using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;

namespace SkyNET.Framework.Common.Extensions;

public static class EnumExtension {
    public static string GetDescription<T>(this T? value) where T : struct, IConvertible =>
        value?.GetDescription();

    public static string GetDescription<T>(this T value) where T : struct, IConvertible {
        var type = value.GetType();

        if (!Enum.IsDefined(type, value)) {
            return string.Empty;
        }

        var index = value.ToInt32(CultureInfo.InvariantCulture);

        var enumName = type.GetEnumName(index);
        if (string.IsNullOrWhiteSpace(enumName)) {
            return string.Empty;
        }

        var memberInfos = type.GetMember(enumName);

        if (memberInfos[0]
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .FirstOrDefault() is DescriptionAttribute descriptionAttribute) {
            return descriptionAttribute.Description;
        }

        return string.Empty;
    }

    public static string GetXmlName<T>(this T? value) where T : struct, IConvertible =>
        value?.GetXmlName();

    public static string GetXmlName<T>(this T value) where T : struct, IConvertible {
        var type = value.GetType();

        if (!Enum.IsDefined(type, value)) {
            return string.Empty;
        }

        var index = value.ToInt32(CultureInfo.InvariantCulture);

        var enumName = type.GetEnumName(index);
        if (string.IsNullOrWhiteSpace(enumName)) {
            return string.Empty;
        }

        var memberInfos = type.GetMember(enumName);

        if (memberInfos[0]
                .GetCustomAttributes(typeof(XmlEnumAttribute), false)
                .FirstOrDefault() is XmlEnumAttribute xmlEnumAttribute) {
            return xmlEnumAttribute.Name ?? string.Empty;
        }

        return string.Empty;
    }
}