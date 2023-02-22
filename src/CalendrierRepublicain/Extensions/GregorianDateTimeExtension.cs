using static Sinistrius.CalendrierRepublicain.Constants;
using GregorianDateTime = System.DateTime;


namespace Sinistrius.CalendrierRepublicain.Extensions;


/// <summary>
/// Extends the <see cref="GregorianDateTime"/> struct.
/// </summary>
internal static class GregorianDateTimeExtension
{

    /// <summary>
    /// Converts a <see cref="GregorianDateTime"/> to a <see cref="RepublicanDateTime"/>.
    /// </summary>
    /// <param name="gregDateTime">The <see cref="GregorianDateTime"/> to be converted.</param>
    /// <returns>The resulting <see cref="RepublicanDateTime"/>.</returns>
    internal static RepublicanDateTime ToRepublican(this GregorianDateTime gregDateTime)
    {
        // Validate input
        if (!gregDateTime.IsValid())
        {
            throw new ArgumentOutOfRangeException(nameof(gregDateTime));
        }

        // Calculate number of days since start of the Republican calendar.
        int totalDays = (int)(gregDateTime - MinSupportedDateTime).TotalDays;

        int year = (int)(totalDays / 365.25);

        if (year.IsRepublicanLeapYear())
        {
            year--;
        }

        int day = totalDays + 1 - (int)(365.25 * year) + (year + 1) / 100 - (year + 1) / 400;
        year++;
        int month = 1;
        int complementaryDayCount = year.IsRepublicanLeapYear() ? 6 : 5;

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
                    complementaryDayCount = year.IsRepublicanLeapYear() ? 6 : 5;
                }
            }
        }

        RepublicanDateTime repDateTime = new(year, month, day, gregDateTime.Hour, gregDateTime.Minute, gregDateTime.Second, gregDateTime.Millisecond);

        return repDateTime;
    }


    /// <summary>
    /// Determines whether a <see cref="GregorianDateTime"/> fits in the validity range of the Republican calendar
    /// </summary>
    /// <param name="time">The <see cref="GregorianDateTime"/> to be validated.</param>
    /// <returns>True if the specified <see cref="GregorianDateTime"/> is valid, otherwise false.</returns>
    internal static bool IsValid(this GregorianDateTime time)
    {
        return time >= MinSupportedDateTime && time <= MaxSupportedDateTime;
    }

}
