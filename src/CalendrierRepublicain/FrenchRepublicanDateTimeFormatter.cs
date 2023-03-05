using Sinistrius.NumeriRomani;
using System.Globalization;
using System.Text.RegularExpressions;


namespace Sinistrius.CalendrierRepublicain;


/// <summary>
/// A formatter for a <see cref="FrenchRepublicanDateTime"/>.
/// </summary>
internal class FrenchRepublicanDateTimeFormatter : IFormatProvider, ICustomFormatter
{

    /// <inheritdoc/>
    public object? GetFormat(Type? formatType)
    {
        if (formatType == typeof(ICustomFormatter))
        {
            return this;
        }
        else
        {
            return null;
        }
    }


    /// <inheritdoc/>
    public string Format(string? format, object? arg, IFormatProvider? formatProvider)
    {
        // Validate arguments
        if (format == null)
        {
            throw new ArgumentNullException(nameof(format));
        }

        if (arg == null)
        {
            throw new ArgumentNullException(nameof(arg));
        }

        // Check value and convert it to a Republican date.
        FrenchRepublicanDateTime time;

        if (arg.GetType() == typeof(FrenchRepublicanDateTime))
        {
            time = (FrenchRepublicanDateTime)arg;
        }
        else if (arg.GetType() == typeof(DateTime))
        {
            time = ((DateTime)arg).ToFrenchRepublicanTime();
        }
        else
        {
            throw new ArgumentException($"The argument must be of type {typeof(DateTime).Name} or {typeof(FrenchRepublicanDateTime).Name}.", nameof(arg));
        }

        // Format date according to format string
        DateTimeFormatInfo standardFormatInfo = CultureInfo.CurrentCulture.DateTimeFormat;

        switch (format)
        {
            case "D":
                return FillPattern(standardFormatInfo.LongDatePattern, time);

            case "d":
                return FillPattern(standardFormatInfo.ShortDatePattern, time);

            default:
                return FillPattern(format, time);
        }
    }


    /// <summary>
    /// Replaces date and time patterns by actual values.
    /// </summary>
    /// <param name="pattern">The pattern</param>
    /// <param name="time">The time in the Republican calendar.</param>
    /// <returns>The time string.</returns>
    private static string FillPattern(string pattern, FrenchRepublicanDateTime time)
    {
        Dictionary<string, string> replacements = new();

        if (FrenchRepublicanDateTime.IsLeapMonth(time.Year, time.Month, time.Era))
        {
            replacements.Add(@"(\b(d|dddd|MMMM?)\b)?(d|dddd|MMMM?|[ .,-])*\b(d|dddd|MMMM?)\b[.,-]?", Constants.ComplementaryDayNames[time.Day - 1]);
        }
        else
        {
            replacements.Add(@"\bdddd\b", Constants.DayOfWeekNames[(time.Day - 1) % 10]);
            replacements.Add(@"\bd\b", time.Day.ToString());
            replacements.Add(@"\bMMMM\b", Constants.MonthNames[time.Month - 1]);
            replacements.Add(@"\bMMM\b", Constants.AbbreviatedMonthNames[time.Month - 1]);
        }

        replacements.Add(@"\byyyy\b", time.Year.ToRoman());
        replacements.Add(@"\bHH\b", time.Hour.ToString("00"));
        replacements.Add(@"\bH\b", time.Hour.ToString());
        replacements.Add(@"\bmm\b", time.Minute.ToString("00"));
        replacements.Add(@"\bss\b", time.Second.ToString("00"));

        string result = pattern;

        foreach (KeyValuePair<string, string> replacement in replacements)
        {
            result = Regex.Replace(result, replacement.Key, replacement.Value);
        }

        return result;
    }

}
