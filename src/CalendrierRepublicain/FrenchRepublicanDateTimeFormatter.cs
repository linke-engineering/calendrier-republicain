using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using LinkeEngineering.NumeriRomani;


namespace LinkeEngineering.CalendrierRepublicain;


/// <summary>
/// A formatter for a <see cref="FrenchRepublicanDateTime"/>.
/// </summary>
public partial class FrenchRepublicanDateTimeFormatter : IFormatProvider, ICustomFormatter
{

    #region Precompiled Regular Expressions

    /// <summary>
    /// Returns a regular expression that matches a long year pattern.
    /// <returns>A <see cref="Regex"/> instance.</returns>
    [GeneratedRegex("^%?y{3,5}")]
    private static partial Regex LongYearRegex();


    /// <summary>
    /// Returns a regular expression that matches a two-digit year pattern.
    /// <returns>A <see cref="Regex"/> instance.</returns>
    [GeneratedRegex("^%?yy")]
    private static partial Regex TwoDigitYearRegex();


    /// <summary>
    /// Returns a regular expression that matches a one-digit year pattern.
    /// <returns>A <see cref="Regex"/> instance.</returns>
    [GeneratedRegex("^%?y")]
    private static partial Regex OneDigitYearRegex();


    /// <summary>
    /// Returns a regular expression that matches a month name pattern.
    /// <returns>A <see cref="Regex"/> instance.</returns>
    [GeneratedRegex("^%?MMM(M|\\.?)")]
    private static partial Regex MonthNameRegex();


    /// <summary>
    /// Returns a regular expression that matches a two-digit month pattern.
    /// <returns>A <see cref="Regex"/> instance.</returns>
    [GeneratedRegex("^%?MM")]
    private static partial Regex TwoDigitMonthRegex();


    /// <summary>
    /// Returns a regular expression that matches a one-digit month pattern.
    /// <returns>A <see cref="Regex"/> instance.</returns>
    [GeneratedRegex("^%?M")]
    private static partial Regex OneDigitMonthRegex();
    [GeneratedRegex("^%?ddd(d|\\.?)")]


    /// <summary>
    /// Returns a regular expression that matches a day name pattern.
    /// <returns>A <see cref="Regex"/> instance.</returns>
    private static partial Regex DayNameRegex();


    /// <summary>
    /// Returns a regular expression that matches a two-digit day pattern.
    /// <returns>A <see cref="Regex"/> instance.</returns>
    [GeneratedRegex("^%?dd")]
    private static partial Regex TwoDigitDayRegex();
    [GeneratedRegex("^%?d")]


    /// <summary>
    /// Returns a regular expression that matches a one-digit day pattern.
    /// <returns>A <see cref="Regex"/> instance.</returns>
    private static partial Regex OneDigitDayRegex();


    /// <summary>
    /// Returns a regular expression that matches a two-digit hour pattern.
    /// <returns>A <see cref="Regex"/> instance.</returns>
    [GeneratedRegex("^%?(hh|HH)")]
    private static partial Regex TwoDigitHourRegex();


    /// <summary>
    /// Returns a regular expression that matches a one-digit hour pattern.
    /// <returns>A <see cref="Regex"/> instance.</returns>
    [GeneratedRegex("^%?(h|H)")]
    private static partial Regex OneDigitHourRegex();


    /// <summary>
    /// Returns a regular expression that matches a two-digit minute pattern.
    /// <returns>A <see cref="Regex"/> instance.</returns>
    [GeneratedRegex("^%?mm")]
    private static partial Regex TwoDigitMinuteRegex();


    /// <summary>
    /// Returns a regular expression that matches a one-digit minute pattern.
    /// <returns>A <see cref="Regex"/> instance.</returns>
    [GeneratedRegex("^%?m")]
    private static partial Regex OneDigitMinuteRegex();


    /// <summary>
    /// Returns a regular expression that matches a two-digit second pattern.
    /// <returns>A <see cref="Regex"/> instance.</returns>
    [GeneratedRegex("^%?ss")]
    private static partial Regex TwoDigitSecondRegex();


    /// <summary>
    /// Returns a regular expression that matches a one-digit second pattern.
    /// <returns>A <see cref="Regex"/> instance.</returns>
    [GeneratedRegex("^%?s")]
    private static partial Regex OneDigitSecondRegex();


    /// <summary>
    /// Returns a regular expression that matches a date separator pattern.
    /// <returns>A <see cref="Regex"/> instance.</returns>
    [GeneratedRegex("^%?/")]
    private static partial Regex DateSeparatorRegex();


    /// <summary>
    /// Returns a regular expression that matches a time separator pattern.
    /// <returns>A <see cref="Regex"/> instance.</returns>
    [GeneratedRegex("^%?:")]
    private static partial Regex TimeSeparatorRegex();


    /// <summary>
    /// Returns a regular expression that matches an unsupported format string.
    /// <returns>A <see cref="Regex"/> instance.</returns>
    [GeneratedRegex("^%?([fF]{1,7}|g{1,2}|K|t{1,2}|z{1,3})")]
    private static partial Regex UnsupportedFormatStringRegex();


    /// <summary>
    /// Returns a regular expression that matches a complementary day pattern.
    /// <returns>A <see cref="Regex"/> instance.</returns>
    [GeneratedRegex("^%?d{1,4}.*M{3,4}(.*d{1,4})?|M{3,4}.*d{1,4}(.*M{3,4})?|dddd.*M{1,3}|M{1,3}.*dddd")]
    private static partial Regex ComplementaryDayRegex();

    #endregion


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

        // Check for invalid format strings (single-character formats other than the supported standard format strings).
        const string supportedStandardFormats = "dDfFgGmMtTyY";

        if (format.Length == 1 && !supportedStandardFormats.Contains(format))
        {
            throw new FormatException($"The format string '{format}' is not supported.");
        }

        var standardFormatInfo = CultureInfo.CurrentCulture.DateTimeFormat;

        return format switch
        {
            "d" => FillPattern(Regex.Replace(standardFormatInfo.ShortDatePattern, "y{3,4}", "yy"), time),
            "D" => FillPattern(standardFormatInfo.LongDatePattern, time),
            "f" => FillPattern(standardFormatInfo.LongDatePattern + " " + standardFormatInfo.ShortTimePattern, time),
            "F" => FillPattern(standardFormatInfo.FullDateTimePattern, time),
            "g" => FillPattern(Regex.Replace(standardFormatInfo.ShortDatePattern, "y{3,4}", "yy") + " " + standardFormatInfo.ShortTimePattern, time),
            "G" => FillPattern(Regex.Replace(standardFormatInfo.ShortDatePattern, "y{3,4}", "yy") + " " + standardFormatInfo.LongTimePattern, time),
            "m" or "M" => FillPattern(standardFormatInfo.MonthDayPattern, time),
            "t" => FillPattern(standardFormatInfo.ShortTimePattern, time),
            "T" => FillPattern(standardFormatInfo.LongTimePattern, time),
            "y" or "Y" => FillPattern(standardFormatInfo.YearMonthPattern, time),
            _ => FillPattern(format, time),
        };
    }


    /// <summary>
    /// Replaces date and time patterns by actual values.
    /// </summary>
    /// <param name="pattern">The pattern</param>
    /// <param name="time">The time in the Republican calendar.</param>
    /// <returns>The time string.</returns>
    private string FillPattern(string pattern, FrenchRepublicanDateTime time)
    {
        StringBuilder result = new();

        // Define replacement rules for known placeholders
        var rules = GetReplacementRules(time);

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
            foreach (var rule in rules)
            {
                var match = rule.Key.Match(pattern);

                if (match.Success)
                {
                    // Append the replacement result
                    result.Append(rule.Replacement(time));

                    // Remove the matched part from the pattern
                    pattern = pattern[match.Length..];
                    matched = true;
                    break;
                }
            }

            if (!matched)
            {
                // Append the first character as-is and move to the next
                result.Append(pattern[0]);
                pattern = pattern[1..];
            }
        }

        return result.ToString().Trim();
    }


    /// <summary>
    /// Defines the replacement rules for known placeholders. Each rule consists of a regex to identify the placeholder
    /// and a function to compute the replacement value based on the given time.
    /// </summary>
    /// <param name="time">The time in the Republican calendar.</param>
    /// <returns>A list of regex patterns and their corresponding replacement functions.</returns>
    private IReadOnlyList<(Regex Key, Func<FrenchRepublicanDateTime, string> Replacement)> GetReplacementRules(FrenchRepublicanDateTime time)
    {
        var formatInfo = new FrenchRepublicanDateTimeFormatInfo();

        var rules = new List<(Regex Key, Func<FrenchRepublicanDateTime, string> Replacement)>
        {
            (LongYearRegex(), t => String.Format(new RomanNumeralsFormatter(), "{0:}", t.Year)),
            (TwoDigitYearRegex(), t => (t.Year % 100).ToString("00")),
            (OneDigitYearRegex(), t => (t.Year % 100).ToString())
        };

        // Special handling for the complementary days, which have their own day names and no month name.
        if (time.Month == Constants.ComplementaryMonth)
        {
            rules.Add((ComplementaryDayRegex(), t => formatInfo.ComplementaryDayNames[t.Day - 1]));
        }

        // Standard handling for date and time placeholders.
        rules.Add((MonthNameRegex(), t => formatInfo.MonthNames[t.Month - 1]));
        rules.Add((TwoDigitMonthRegex(), t => t.Month.ToString("00")));
        rules.Add((OneDigitMonthRegex(), t => t.Month.ToString()));
        rules.Add((DayNameRegex(), t => t.Month == Constants.ComplementaryMonth ? String.Empty : formatInfo.DayNames[(t.Day - 1) % Constants.DaysInWeek]));
        rules.Add((TwoDigitDayRegex(), t => t.Day.ToString("00")));
        rules.Add((OneDigitDayRegex(), t => t.Day.ToString()));
        rules.Add((TwoDigitHourRegex(), t => t.Hour.ToString("00")));
        rules.Add((OneDigitHourRegex(), t => t.Hour.ToString()));    
        rules.Add((TwoDigitMinuteRegex(), t => t.Minute.ToString("00")));
        rules.Add((OneDigitMinuteRegex(), t => t.Minute.ToString()));
        rules.Add((TwoDigitSecondRegex(), t => t.Second.ToString("00")));
        rules.Add((OneDigitSecondRegex(), t => t.Second.ToString()));
        rules.Add((DateSeparatorRegex(), _ => CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator));
        rules.Add((TimeSeparatorRegex(), _ => CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator));

        // Unsupported placeholders will be replaced with an empty string.
        rules.Add((UnsupportedFormatStringRegex(), t => String.Empty));

        return rules;
    }

}
