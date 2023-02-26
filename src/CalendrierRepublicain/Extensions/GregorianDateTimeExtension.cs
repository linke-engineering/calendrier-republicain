using static Sinistrius.CalendrierRepublicain.Constants;
using GregorianDateTime = System.DateTime;


namespace Sinistrius.CalendrierRepublicain.Extensions;


/// <summary>
/// Extends the <see cref="GregorianDateTime"/> struct.
/// </summary>
internal static class GregorianDateTimeExtension
{

    /// <summary>
    /// Converts a <see cref="GregorianDateTime"/> to a <see cref="FrenchRepublicanDateTime"/>.
    /// </summary>
    /// <param name="gregDateTime">The <see cref="GregorianDateTime"/> to be converted.</param>
    /// <returns>The resulting <see cref="FrenchRepublicanDateTime"/>.</returns>
    internal static FrenchRepublicanDateTime ToRepublican(this GregorianDateTime gregDateTime)
    {
        // Validate input
        gregDateTime.Validate();

        // Calculate number of days since start of the Republican calendar.
        int totalDays = (int)(gregDateTime - MinSupportedDateTime).TotalDays;

        int year = (int)(totalDays / 365.25);
        bool isRepLeapYear = year > 0 && new FrenchRepublicanCalendar().IsLeapYear(year);

        if (isRepLeapYear)
        {
            year--;
        }

        int day = totalDays + 1 - (int)(365.25 * year) + (year + 1) / 100 - (year + 1) / 400;
        year++;
        int month = 1;
        int complementaryDayCount =isRepLeapYear ? 6 : 5;

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
                    complementaryDayCount = isRepLeapYear ? 6 : 5;
                }
            }
        }

        FrenchRepublicanDateTime repDateTime = new(year, month, day, gregDateTime.Hour, gregDateTime.Minute, gregDateTime.Second, gregDateTime.Millisecond);

        return repDateTime;
    }


    /// <summary>
    /// Determines whether a <see cref="GregorianDateTime"/> fits in the validity range of the Republican calendar
    /// </summary>
    /// <param name="time">The <see cref="GregorianDateTime"/> to be validated.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the <see cref="GregorianDateTime"/> is invalid.</exception>
    internal static void Validate(this GregorianDateTime time)
    {
        if (time < MinSupportedDateTime || time > MaxSupportedDateTime)
        {
            throw new ArgumentOutOfRangeException(nameof(time));
        }
    }

}
