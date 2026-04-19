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
                    result.Append(rule.Value(time));

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
    /// Defines the replacement rules for date and time patterns. The keys of the returned dictionary are regex 
    /// patterns that match the placeholders in the format string, and the values are functions that take a 
    /// <see cref="FrenchRepublicanDateTime"/> and return the corresponding string representation for that placeholder.
    /// </summary>
    /// <param name="time">The time in the Republican calendar.</param>
    /// <returns>A dictionary of regex patterns and their corresponding replacement functions.</returns>
    private Dictionary<Regex, Func<FrenchRepublicanDateTime, string>> GetReplacementRules(FrenchRepublicanDateTime time)
    {
        const string CommonPrefix = "^%?";
        var formatInfo = new FrenchRepublicanDateTimeFormatInfo();

        var rules = new Dictionary<Regex, Func<FrenchRepublicanDateTime, string>>
        {
            { new Regex(CommonPrefix + "y{3,5}"), t => String.Format(new RomanNumeralsFormatter(), "{0:}", t.Year) },
            { new Regex(CommonPrefix + "yy"), t => (t.Year % 100).ToString("00") },
            { new Regex(CommonPrefix + "y"), t => (t.Year % 100).ToString() }
        };

        // Special handling for the complementary days, which have their own day names and no month name.
        if (time.Month == Constants.ComplementaryMonth)
        {
            rules.Add(new Regex(CommonPrefix + "d{1,4}.*M{3,4}(.*d{1,4})?|M{3,4}.*d{1,4}(.*M{3,4})?|dddd.*M{1,3}|M{1,3}.*dddd"), t => formatInfo.ComplementaryDayNames[t.Day - 1]);
        }

        // Standard handling for date and time placeholders.
        rules.Add(new Regex(CommonPrefix + "MMMM"), t => formatInfo.MonthNames[t.Month - 1]);
        rules.Add(new Regex(CommonPrefix + "MMM\\.?"), t => formatInfo.MonthNames[t.Month - 1]);
        rules.Add(new Regex(CommonPrefix + "MM"), t => t.Month.ToString("00"));
        rules.Add(new Regex(CommonPrefix + "M"), t => t.Month.ToString());
        rules.Add(new Regex(CommonPrefix + "dddd"), t => t.Month == Constants.ComplementaryMonth ? String.Empty : formatInfo.DayNames[(t.Day - 1) % Constants.DaysInWeek]);
        rules.Add(new Regex(CommonPrefix + "ddd\\.?"), t => t.Month == Constants.ComplementaryMonth ? String.Empty : formatInfo.DayNames[(t.Day - 1) % Constants.DaysInWeek]);
        rules.Add(new Regex(CommonPrefix + "dd"), t => t.Day.ToString("00"));
        rules.Add(new Regex(CommonPrefix + "d"), t => t.Day.ToString());
        rules.Add(new Regex(CommonPrefix + "hh"), t => t.Hour.ToString("00"));  // 12-hour clock is not supported, so "hh"
        rules.Add(new Regex(CommonPrefix + "h"), t => t.Hour.ToString());       // and "h" will be treated as "HH" and "H"
        rules.Add(new Regex(CommonPrefix + "HH"), t => t.Hour.ToString("00"));
        rules.Add(new Regex(CommonPrefix + "H"), t => t.Hour.ToString());
        rules.Add(new Regex(CommonPrefix + "mm"), t => t.Minute.ToString("00"));
        rules.Add(new Regex(CommonPrefix + "m"), t => t.Minute.ToString());
        rules.Add(new Regex(CommonPrefix + "ss"), t => t.Second.ToString("00"));
        rules.Add(new Regex(CommonPrefix + "s"), t => t.Second.ToString());
        rules.Add(new Regex(CommonPrefix + "/"), _ => CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator);
        rules.Add(new Regex(CommonPrefix + ":"), _ => CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator);

        // Unsupported placeholders will be replaced with an empty string.
        rules.Add(new Regex(CommonPrefix + "f{1,7}"), t => String.Empty);
        rules.Add(new Regex(CommonPrefix + "F{1,7}"), t => String.Empty);
        rules.Add(new Regex(CommonPrefix + "g{1,2}"), t => String.Empty);
        rules.Add(new Regex(CommonPrefix + "K"), t => String.Empty);
        rules.Add(new Regex(CommonPrefix + "t{1,2}"), t => String.Empty);
        rules.Add(new Regex(CommonPrefix + "z{1,3}"), t => String.Empty);

        return rules;
    }

}
