namespace Sinistrius.CalendrierRepublicain.Extensions;


/// <summary>
/// Extends the <see cref="Int32"/> class used for date and time properties.
/// </summary>
internal static class Int32Extension
{

    /// <summary>
    /// Determines whether the specified era is valid in the Republican calendar.
    /// </summary>
    /// <param name="era">An integer that represents the calendar era.</param>
    /// <exception cref="ArgumentOutOfRangeException"/>
    internal static void ValidateEra(this int era)
    {
        if (era != 1)
        {
            throw new ArgumentOutOfRangeException(nameof(era));
        }
    }


    /// <summary>
    /// Determines whether the specified year is valid in the Republican calendar.
    /// </summary>
    /// <param name="year">An integer that represents the year.</param>
    /// <exception cref="ArgumentOutOfRangeException"/>
    internal static void ValidateYear(this int year)
    {
        if (year < 1 || year > 14)
        {
            throw new ArgumentOutOfRangeException(nameof(year));
        }
    }


    /// <summary>
    /// Determines whether the specified year in the Republican calendar is a leap year.
    /// </summary>
    /// <param name="year">An integer that represents the year .</param>
    /// <returns>True if the year is a leap year, otherwise false.</returns>
    internal static bool IsLeapYear(this int year)
    {
        return (year + 1) % 4 == 0;
    }


    /// <summary>
    /// Determines whether the specified month is valid in the Republican calendar.
    /// </summary>
    /// <param name="month">An integer that represents the month of the year.</param>
    /// <param name="year">An integer that represents the year.</param>
    /// <exception cref="ArgumentOutOfRangeException"/>
    internal static void ValidateMonth(this int month, int year)
    {
        if ((month < 1) ||
            // The complementary days at the end of each year are considered as 13th month
            (month > 13) ||
            // The Republican calender was abolished in the 4th month of the 14th year
            ((year == 14) && (month > 4)))
        {
            throw new ArgumentOutOfRangeException(nameof(month));
        }
    }


    /// <summary>
    /// Determines whether the specified day is valid in the Republican calendar.
    /// </summary>
    /// <param name="day">An integer that represents the day of the month.</param>
    /// <param name="year">An integer that represents the year.</param>
    /// <param name="month">An integer that represents the month of the year.</param>
    /// <exception cref="ArgumentOutOfRangeException"/>
    internal static void ValidateDay(this int day, int year, int month)
    {
        if ((day < 1) ||
            // Each month has 30 days
            (day > 30) ||
            // The Republican calender was abolished on the 10th day of the 4th month of the 14th year
            ((year == 14) && (month == 4) && (day > 10)) ||
            // In a non-leap year there are 5 complementary days in the assumed 13th month
            (!year.IsLeapYear() && (month == 13) && (day > 5)) ||
            // In a leap year there are 6 complementary days in the assumed 13th month
            (year.IsLeapYear() && (month == 13) && (day > 6)))
        {
            throw new ArgumentOutOfRangeException(nameof(day));
        }
    }


    /// <summary>
    /// Determines whether the specified hour is valid.
    /// </summary>
    /// <param name="hour">An integer that represents the hour of the day.</param>
    /// <exception cref="ArgumentOutOfRangeException"/>
    internal static void ValidateHour(this int hour)
    {
        if (hour < 0 || hour > 23)
        {
            throw new ArgumentOutOfRangeException(nameof(hour));
        }
    }


    /// <summary>
    /// Determines whether the specified minute is valid.
    /// </summary>
    /// <param name="minute">An integer that represents the minute.</param>
    /// <exception cref="ArgumentOutOfRangeException"/>
    internal static void ValidateMinute(this int minute)
    {
        if (minute < 0 || minute > 59)
        {
            throw new ArgumentOutOfRangeException(nameof(minute));
        }
    }


    /// <summary>
    /// Determines whether the specified second is valid.
    /// </summary>
    /// <param name="second">An integer that represents the second.</param>
    /// <exception cref="ArgumentOutOfRangeException"/>
    internal static void ValidateSecond(this int second)
    {
        if (second < 0 || second > 59)
        {
            throw new ArgumentOutOfRangeException(nameof(second));
        }
    }


    /// <summary>
    /// Determines whether a specified millisecond is valid.
    /// </summary>
    /// <param name="millisecond">An integer that represents the millisecond.</param>
    /// <exception cref="ArgumentOutOfRangeException"/>
    internal static void ValidateMillisecond(this int millisecond)
    {
        if (millisecond < 0 || millisecond > 999)
        {
            throw new ArgumentOutOfRangeException(nameof(millisecond));
        }
    }

}
