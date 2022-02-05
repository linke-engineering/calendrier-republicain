namespace Sinistrius.CalendrierRepublicain;


/// <summary>
/// Validates date and time parts of the Republican calendar.
/// </summary>
internal class RepublicanDateTimeValidator
{

    /// <summary>
    /// Determines whether a calendar era is valid.
    /// </summary>
    /// <param name="era">An integer that represents the calendar era.</param>
    /// <exception cref="ArgumentOutOfRangeException"/>
    internal void ValidateEra(int era)
    {
        if (era != 1)
        {
            throw new ArgumentOutOfRangeException(nameof(era));
        }
    }


    /// <summary>
    /// Determines whether a year is valid within a Republican date.
    /// </summary>
    /// <param name="year">An integer that represents the year.</param>
    /// <exception cref="ArgumentOutOfRangeException"/>
    internal void ValidateYear(int year)
    {
        if (year < 1 || year > 14)
        {
            throw new ArgumentOutOfRangeException(nameof(year));
        }
    }


    /// <summary>
    /// Determines whether a month is valid within a Republican date.
    /// </summary>
    /// <param name="year">An integer that represents the year.</param>
    /// <param name="month">An integer that represents the month of the year.</param>
    /// <exception cref="ArgumentOutOfRangeException"/>
    internal void ValidateMonth(int year, int month)
    {
        if (month < 1 ||
            month > 13 ||
            year == 14 && month > 4)
        {
            throw new ArgumentOutOfRangeException(nameof(month));
        }
    }


    /// <summary>
    /// Determines whether a day is valid within a Republican date.
    /// </summary>
    /// <param name="year">An integer that represents the year.</param>
    /// <param name="month">An integer that represents the month of the year.</param>
    /// <param name="day">An integer that represents the day of the month.</param>
    /// <exception cref="ArgumentOutOfRangeException"/>
    internal void ValidateDay(int year, int month, int day)
    {
        if (day < 1 ||
            day > 30 ||
            year == 14 && month == 4 && day > 10 ||
            !Globals.IsLeapYear(year) && month == 13 && day > 5 ||
            Globals.IsLeapYear(year) && month == 13 && day > 6)
        {
            throw new ArgumentOutOfRangeException(nameof(day));
        }
    }


    /// <summary>
    /// Determines whether a specified hour value is valid.
    /// </summary>
    /// <param name="hour">An integer that represents the hour.</param>
    /// <exception cref="ArgumentOutOfRangeException"/>
    internal void ValidateHour(int hour)
    {
        if (hour < 0 || hour > 23)
        {
            throw new ArgumentOutOfRangeException(nameof(hour));
        }
    }


    /// <summary>
    /// Determines whether a specified minute value is valid.
    /// </summary>
    /// <param name="minute">An integer that represents the minute.</param>
    /// <exception cref="ArgumentOutOfRangeException"/>
    internal void ValidateMinute(int minute)
    {
        if (minute < 0 || minute > 59)
        {
            throw new ArgumentOutOfRangeException(nameof(minute));
        }
    }


    /// <summary>
    /// Determines whether a specified second value is valid.
    /// </summary>
    /// <param name="second">An integer that represents the second.</param>
    /// <exception cref="ArgumentOutOfRangeException"/>
    internal void ValidateSecond(int second)
    {
        if (second < 0 || second > 59)
        {
            throw new ArgumentOutOfRangeException(nameof(second));
        }
    }


    /// <summary>
    /// Determines whether a specified millisecond value is valid.
    /// </summary>
    /// <param name="millisecond">An integer that represents the millisecond.</param>
    /// <exception cref="ArgumentOutOfRangeException"/>
    internal void ValidateMillisecond(int millisecond)
    {
        if (millisecond < 0 || millisecond > 999)
        {
            throw new ArgumentOutOfRangeException(nameof(millisecond));
        }
    }

}
