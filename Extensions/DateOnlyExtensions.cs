namespace Quarta.Framework.Common.Extensions;

public static class DateOnlyExtensions
{
    public static DateOnly Min(DateOnly first, DateOnly second) =>
        first > second ? second : first;

    public static DateOnly? Min(DateOnly? first, DateOnly? second)
    {
        if (first is null)
        {
            return second;
        }

        if (second is null)
        {
            return first;
        }

        return Min(first.Value, second.Value);
    }

    public static DateOnly Max(DateOnly first, DateOnly second) =>
        first > second ? first : second;

    public static DateOnly? Max(DateOnly? first, DateOnly? second)
    {
        if (first is null)
        {
            return second;
        }

        if (second is null)
        {
            return first;
        }

        return Max(first.Value, second.Value);
    }
}