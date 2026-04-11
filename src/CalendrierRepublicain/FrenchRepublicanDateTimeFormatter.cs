using System.Globalization;
using System.Text.RegularExpressions;
using LinkeEngineering.NumeriRomani;


namespace LinkeEngineering.CalendrierRepublicain;


/// <summary>
/// A formatter for a <see cref="FrenchRepublicanDateTime"/>.
/// </summary>
public class FrenchRepublicanDateTimeFormatter : IFormatProvider, ICustomFormatter
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
    /// <param name="format">
    /// The format string. Use "D" for the long date format (e.g., "Décadi, 10. Nivôse XIV"), "d" 
    /// for the short date format (e.g., "10. Niv. XIV"), or custom patterns like "d. MMMM yyyy". 
    /// Note: "d" can also represent the day of the month in custom patterns.
    /// </param>
    public string Format(string? format, object? arg, IFormatProvider? formatProvider)
    {
        // Validate arguments.
        ArgumentNullException.ThrowIfNull(format, nameof(format));
        ArgumentNullException.ThrowIfNull(arg, nameof(arg));

        // Check value and convert it to a Republican date.
        FrenchRepublicanDateTime time;

        if (arg is FrenchRepublicanDateTime republicanDateTime)
        {
            time = republicanDateTime;
        }
        else if (arg is DateTime dateTime)
        {
            time = dateTime.ToFrenchRepublicanTime();
        }
        else
        {
            throw new ArgumentException($"The argument must be of type {typeof(DateTime).Name} or {typeof(FrenchRepublicanDateTime).Name}.", nameof(arg));
        }

        var standardFormatInfo = CultureInfo.CurrentCulture.DateTimeFormat;

        return format switch
        {
            "D" => FillPattern(standardFormatInfo.LongDatePattern, time),
            "d" => FillPattern(standardFormatInfo.ShortDatePattern, time),
            _ => FillPattern(format, time),
        };
    }


    /// <summary>
    /// Replaces date and time patterns by actual values.
    /// </summary>
    /// <param name="pattern">The pattern</param>
    /// <param name="time">The time in the Republican calendar.</param>
    /// <returns>The time string.</returns>
    private static string FillPattern(string pattern, FrenchRepublicanDateTime time)
    {
        var formatInfo = new FrenchRepublicanDateTimeFormatInfo();
        var replacements = new Dictionary<string, string>();

        if (FrenchRepublicanDateTime.IsLeapMonth(time.Year, time.Month, time.Era))
        {
            replacements.Add(@"\b((d+|M+)[., -]*)+(d+|M+)\.?", formatInfo.ComplementaryDayNames[time.Day - 1]);
        }
        else
        {
            replacements.Add(@"\bMMMM\b", formatInfo.MonthNames[time.Month - 1]);
            replacements.Add(@"\bMMM\b", formatInfo.AbbreviatedMonthNames[time.Month - 1]);
            replacements.Add(@"\bMM\b", time.Month.ToString("00"));
            replacements.Add(@"\bM\b", time.Month.ToString());
            replacements.Add(@"\bdddd\b", formatInfo.DayNames[(time.Day - 1) % Constants.DaysInWeek]);
            replacements.Add(@"\bddd\b", formatInfo.DayNames[(time.Day - 1) % Constants.DaysInWeek]);
            replacements.Add(@"\bdd\b", time.Day.ToString("00"));
            replacements.Add(@"\bd\b", time.Day.ToString());
        }

        replacements.Add(@"\by+\b", String.Format(new RomanNumeralsFormatter(), "{0:R}", time.Year));
        replacements.Add(@"\bHH\b", time.Hour.ToString("00"));
        replacements.Add(@"\bH\b", time.Hour.ToString());
        replacements.Add(@"\bmm\b", time.Minute.ToString("00"));
        replacements.Add(@"\bm\b", time.Minute.ToString());
        replacements.Add(@"\bss\b", time.Second.ToString("00"));
        replacements.Add(@"\bs\b", time.Second.ToString());

        var result = pattern;

        foreach (KeyValuePair<string, string> replacement in replacements)
        {
            result = Regex.Replace(result, replacement.Key, replacement.Value);
        }

        return result;
    }

}
