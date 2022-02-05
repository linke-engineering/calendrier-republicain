using GregorianDateTime = System.DateTime;


namespace Sinistrius.CalendrierRepublicain;


/// <summary>
/// Extends the <see cref="GregorianDateTime"/> struct.
/// </summary>
internal static class GregorianDateTimeExtension
{

    /// <summary>
    /// Converts a Gregorian to a Republican date time.
    /// </summary>
    /// <param name="gregDateTime">The Gregorian date time.</param>
    /// <returns>The Republican date time.</returns>
    internal static RepublicanDateTime ToRepublican(this GregorianDateTime gregDateTime)
    {
        // Calculate number of days since start of the Republican calendar.
        int totalDays = (int)(gregDateTime - Globals.MinSupportedDateTime).TotalDays;

        int year = (int)(totalDays / 365.25);

        if (Globals.IsLeapYear(year))
        {
            year--;
        }

        int day = totalDays + 1 - (int)(365.25 * year) + (year + 1) / 100 - (year + 1) / 400;
        year++;
        int month = 1;
        int complementaryDayCount = Globals.IsLeapYear(year) ? 6 : 5;

        while (day > 30)
        {
            day -= 30;
            month++;

            if (month == 13)
            {
                if (day > complementaryDayCount)
                {
                    day -= complementaryDayCount;
                    month = 1;
                    year++;
                    complementaryDayCount = Globals.IsLeapYear(year) ? 6 : 5;
                }
            }
        }

        RepublicanDateTime repDateTime = new(year, month, day, gregDateTime.Hour, gregDateTime.Minute, gregDateTime.Second, gregDateTime.Millisecond);

        return repDateTime;
    }

}
