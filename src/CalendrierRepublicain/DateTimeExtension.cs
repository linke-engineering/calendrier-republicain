using static Sinistrius.CalendrierRepublicain.Constants;


namespace Sinistrius.CalendrierRepublicain;


/// <summary>
/// Extends the <see cref="DateTime"/> struct.
/// </summary>
internal static class DateTimeExtension
{

    #region Local Fields

    /// <summary>
    /// Array containing the JDN of the first day of each Republican year.
    /// </summary>
    private static readonly int[] firstDayOfYearJdns = new int[LastRepublicanYear];

    #endregion


    #region Constructor

    /// <summary>
    /// Initializes the <see cref="DateTimeExtension"/>.
    /// </summary>
    static DateTimeExtension()
    {
        for (int i = 0; i < LastRepublicanYear; i++)
        {
            firstDayOfYearJdns[i] = 2375839 + 365 * i + (i + 1) / 4;
        }
    }

    #endregion


    #region Methods

    /// <summary>
    /// Converts a <see cref="DateTime"/> to a <see cref="FrenchRepublicanDateTime"/>.
    /// </summary>
    /// <param name="gregTime">The <see cref="DateTime"/> to be converted.</param>
    /// <returns>The resulting <see cref="FrenchRepublicanDateTime"/>.</returns>
    internal static FrenchRepublicanDateTime ToFrenchRepublicanTime(this DateTime gregTime)
    {
        // Validate input
        gregTime.Validate();

        // Find year
        int jdn = new JulianDayNumber(gregTime).Value;
        int yearIndex = Array.BinarySearch(firstDayOfYearJdns, jdn);

        if (yearIndex < 0)
        {
            yearIndex = ~yearIndex - 1;
        }

        // Calculate Republican date
        int dayOfYear = jdn - firstDayOfYearJdns[yearIndex];
        int year = yearIndex + 1;
        int month = dayOfYear / 30 + 1;
        int day = dayOfYear % 30 + 1;

        return new FrenchRepublicanDateTime(year, month, day, gregTime.TimeOfDay, FrenchRepublicanCalendar.FrenchRepublicanEra);
    }


    /// <summary>
    /// Determines whether a <see cref="DateTime"/> fits in the validity range of the Republican calendar
    /// </summary>
    /// <param name="time">The <see cref="DateTime"/> to be validated.</param>
    /// <exception cref="ArgumentOutOfRangeException">The <see cref="DateTime"/> is invalid.</exception>
    internal static void Validate(this DateTime time)
    {
        if (time < MinSupportedDateTime || time > MaxSupportedDateTime)
        {
            throw new ArgumentOutOfRangeException(nameof(time));
        }
    }

    #endregion

}
