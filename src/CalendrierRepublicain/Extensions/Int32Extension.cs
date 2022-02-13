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
    /// <returns>True if the specified era is valid, otherwise false.</returns>
    internal static bool IsValidRepublicanCalenderEra(this int era)
    {
        return era == 1;
    }


    /// <summary>
    /// Determines whether the specified year is valid in the Republican calendar.
    /// </summary>
    /// <param name="year">An integer that represents the year.</param>
    /// <returns>True if the specified year is valid, otherwise false.</returns>
    internal static bool IsValidRepublicanYear(this int year)
    {
        return year.IsInRange(1, 14);
    }


    /// <summary>
    /// Determines whether the specified year in the Republican calendar is a leap year.
    /// </summary>
    /// <param name="year">An integer that represents the year .</param>
    /// <returns>True if the specified year is a leap year, otherwise false.</returns>
    internal static bool IsRepublicanLeapYear(this int year)
    {
        return year.IsValidRepublicanYear() &&
               (year + 1) % 4 == 0;
    }


    /// <summary>
    /// Determines whether the specified month is valid in the Republican calendar.
    /// </summary>
    /// <param name="month">An integer that represents the month of the year.</param>
    /// <param name="year">An integer that represents the year.</param>
    /// <returns>True if the specified month is valid, otherwise false.</returns>
    internal static bool IsValidRepublicanMonth(this int month, int year)
    {
        return year.IsValidRepublicanYear() &&
               // The complementary days at the end of each year are considered as 13th month;
               month.IsInRange(1, 13) &&
               // The Republican calender was abolished in the 4th month of the 14th year;
               !(year == 14 && month > 4);
    }


    /// <summary>
    /// Determines whether the specified day of month is valid in the Republican calendar.
    /// </summary>
    /// <param name="day">An integer that represents the day of the month.</param>
    /// <param name="year">An integer that represents the year.</param>
    /// <param name="month">An integer that represents the month of the year.</param>
    /// <returns>True if the specified day is valid, otherwise false.</returns>
    internal static bool IsValidRepublicanDay(this int day, int year, int month)
    {
        return year.IsValidRepublicanYear() &&
               month.IsValidRepublicanMonth(year) &&
               // Each month has 30 days
               day.IsInRange(1, 30) &&
               // The Republican calender was abolished on the 10th day of the 4th month of the 14th year
               !(year == 14 && month == 4 && day > 10) &&
               // In a non-leap year there are 5 complementary days in the assumed 13th month
               !(month == 13 && day > 5 && !year.IsRepublicanLeapYear()) &&
               // In a leap year there are 6 complementary days in the assumed 13th month;
               !(month == 13 && day > 6 && year.IsRepublicanLeapYear());
    }


    /// <summary>
    /// Determines whether the specified hour is valid.
    /// </summary>
    /// <param name="hour">An integer that represents the hour of the day.</param>
    /// <returns>True if the specified hour is valid, otherwise false.</returns>
    internal static bool IsValidHour(this int hour)
    {
        return hour.IsInRange(0, 23);
    }


    /// <summary>
    /// Determines whether the specified minute is valid.
    /// </summary>
    /// <param name="minute">An integer that represents the minute.</param>
    /// <returns>True if the specified minute is valid, otherwise false.</returns>
    internal static bool IsValidMinute(this int minute)
    {
        return minute.IsInRange(0, 59);
    }


    /// <summary>
    /// Determines whether the specified second is valid.
    /// </summary>
    /// <param name="second">An integer that represents the second.</param>
    /// <returns>True if the specified second is valid, otherwise false.</returns>
    internal static bool IsValisSecond(this int second)
    {
        return second.IsInRange(0, 59);
    }


    /// <summary>
    /// Determines whether a specified millisecond is valid.
    /// </summary>
    /// <param name="millisecond">An integer that represents the millisecond.</param>
    /// <returns>True if the specified millisecond is valid, otherwise false.</returns>
    internal static bool IsValidMillisecond(this int millisecond)
    {
        return millisecond.IsInRange(0, 999);
    }


    /// <summary>
    /// Determines whether a specified value is inside the specified range.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <param name="lowerBound">The lower bound.</param>
    /// <param name="upperBound">The upper bound.</param>
    /// <returns></returns>
    private static bool IsInRange(this int value, int lowerBound, int upperBound)
    {
        return value >= lowerBound && value <= upperBound;
    }

}
