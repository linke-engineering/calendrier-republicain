using System.Globalization;
using System.Text;
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

        // Check for invalid format strings (single-character formats other than "d" and "D")
        if (format.Length == 1 && format != "d" && format != "D")
        {
            throw new FormatException($"The format string '{format}' is not supported.");
        }

        var standardFormatInfo = CultureInfo.CurrentCulture.DateTimeFormat;

        switch (format)
        {
            case "D":
                return FillPattern(standardFormatInfo.LongDatePattern, time);

            case "d":
                string shortDatePattern = Regex.Replace(standardFormatInfo.ShortDatePattern, "y{1,4}", "yy");
                return FillPattern(shortDatePattern, time);

            default:
                return FillPattern(format, time);
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
        const string CommonPrefix = "^%?";

        FrenchRepublicanDateTimeFormatInfo formatInfo = new();
        StringBuilder result = new();

        // Define regex patterns for known placeholders
        var replacements = new Dictionary<Regex, Func<FrenchRepublicanDateTime, string>>()
        {
            [new Regex(CommonPrefix + "y{3,5}")] = t => String.Format(new RomanNumeralsFormatter(), "{0:}", t.Year),
            [new Regex(CommonPrefix + "yy")] = t => (t.Year % 100).ToString("00"),
            [new Regex(CommonPrefix + "y")] = t => (t.Year % 100).ToString(),
            [new Regex(CommonPrefix + "MMMM")] = t => formatInfo.MonthNames[t.Month - 1],
            [new Regex(CommonPrefix + "MMM")] = t => String.Empty,
            [new Regex(CommonPrefix + "MM")] = t => t.Month.ToString("00"),
            [new Regex(CommonPrefix + "M")] = t => t.Month.ToString(),
            [new Regex(CommonPrefix + "dddd")] = t => formatInfo.DayNames[(t.Day - 1) % Constants.DaysInWeek],
            [new Regex(CommonPrefix + "ddd")] = t => String.Empty,
            [new Regex(CommonPrefix + "dd")] = t => t.Day.ToString("00"),
            [new Regex(CommonPrefix + "d")] = t => t.Day.ToString(),

            [new Regex(CommonPrefix + "hh")] = t => (t.Hour % 12 == 0 ? 12 : t.Hour % 12).ToString("00"),
            [new Regex(CommonPrefix + "h")] = t => (t.Hour % 12 == 0 ? 12 : t.Hour % 12).ToString(),
            [new Regex(CommonPrefix + "HH")] = t => t.Hour.ToString("00"),
            [new Regex(CommonPrefix + "H")] = t => t.Hour.ToString(),
            [new Regex(CommonPrefix + "mm")] = t => t.Minute.ToString("00"),
            [new Regex(CommonPrefix + "m")] = t => t.Minute.ToString(),
            [new Regex(CommonPrefix + "ss")] = t => t.Second.ToString("00"),
            [new Regex(CommonPrefix + "s")] = t => t.Second.ToString(),

            [new Regex(CommonPrefix + "/")] = _ => CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator,
            [new Regex(CommonPrefix + ":")] = _ => CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator,

            [new Regex(CommonPrefix + "f{1,7}")] = t => String.Empty,
            [new Regex(CommonPrefix + "F{1,7}")] = t => String.Empty,
            [new Regex(CommonPrefix + "g{1,2}")] = t => String.Empty,
            [new Regex(CommonPrefix + "K")] = t => String.Empty,
            [new Regex(CommonPrefix + "t{1,2}")] = t => String.Empty,
            [new Regex(CommonPrefix + "z{1,3}")] = t => String.Empty,
        };

        // If the date is in the complementary month, remove month and day names from the pattern since they don't apply.
        if (time.Month == Constants.ComplementaryMonth)
        {
            replacements[new Regex(CommonPrefix + "MMMM")] = t => String.Empty;
            replacements[new Regex(CommonPrefix + "dddd")] = t => String.Empty;
        }

        // Process the pattern from left to right
        while (pattern.Length > 0)
        {
            bool matched = false;

            // Check for escape sequences and interpret the next character as a literal
            if (pattern[0] == '\\' && pattern.Length > 1)
            {
                result.Append(pattern[1]);
                pattern = pattern.Substring(2);
                continue;
            }

            // Check for literals enclosed in double or single quotes
            if (pattern[0] == '"' || pattern[0] == '\'')
            {
                int endIndex = pattern.IndexOf(pattern[0], 1);

                if (endIndex > 0)
                {
                    result.Append(pattern.AsSpan(1, endIndex - 1));
                    pattern = pattern[(endIndex + 1)..];
                    continue;
                }
            }

            // Check for known placeholders
            foreach (var replacement in replacements)
            {
                var match = replacement.Key.Match(pattern);

                if (match.Success)
                {
                    // Append the replacement result
                    result.Append(replacement.Value(time));

                    // Remove the matched part from the pattern
                    pattern = pattern.Substring(match.Length);
                    matched = true;
                    break;
                }
            }

            if (!matched)
            {
                // Append the first character as-is and move to the next
                result.Append(pattern[0]);
                pattern = pattern.Substring(1);
            }
        }

        return result.ToString();
    }

}
