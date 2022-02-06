using GregorianDateTime = System.DateTime;


namespace Sinistrius.CalendrierRepublicain.Extensions;


/// <summary>
/// Extends the <see cref="RepublicanDateTime"/> struct.
/// </summary>
internal static class RepublicanDateTimeExtension
{

    /// <summary>
    /// Converts a Republican to a Gregorian date time.
    /// </summary>
    /// <param name="repDateTime">The Republican date time.</param>
    /// <returns>The Gregorian date time.</returns>
    internal static GregorianDateTime ToGregorian(this RepublicanDateTime repDateTime)
    {
        // Extract date parts.
        int years = repDateTime.Year;
        int months = repDateTime.Month;
        int days = repDateTime.Day;

        // Append sansculottides to previous month.
        if (months == 13)
        {
            months--;
            days += 30;
        }

        // Calculate days passed since start of the Republican calendar.
        int totalDays = 365 * (years - 1) +  // days in years
                        (years + 1) / 4 +    // leap days
                        30 * (months - 1) +  // days in months
                        (days - 1);          // days

        // Add passed days and time to the start date of the Republican calendar.
        GregorianDateTime gregDateTime = Constants.MinSupportedDateTime.AddDays(totalDays)
                                                                     .Add(repDateTime.TimeOfDay);

        return gregDateTime;
    }

}
