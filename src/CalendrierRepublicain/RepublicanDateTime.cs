using Sinistrius.CalendrierRepublicain.Extensions;

namespace Sinistrius.CalendrierRepublicain;


/// <summary>
/// Represents an instance in time expressed in the Republican calendar.
/// </summary>
internal struct RepublicanDateTime
{

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="RepublicanDateTime"/> class from a Gregorian <see cref="DateTime"/>.
    /// </summary>
    /// <param name="gregDateTime">A <see cref="DateTime"/> that represents a date and time in the Gregorian calendar.</param>
    internal RepublicanDateTime(DateTime gregDateTime)
    {
        if ((gregDateTime < Constants.MinSupportedDateTime) || (gregDateTime > Constants.MaxSupportedDateTime))
        {
            throw new ArgumentOutOfRangeException(nameof(gregDateTime));
        }

        RepublicanDateTime repDateTime = gregDateTime.ToRepublican();

        Year = repDateTime.Year;
        Month = repDateTime.Month;
        Day = repDateTime.Day;
        Hour = repDateTime.Hour;
        Minute = repDateTime.Minute;
        Second = repDateTime.Second;
        Millisecond = repDateTime.Millisecond;
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="RepublicanDateTime"/> class to the specified Republican year, month, day, hour, minute, second and millisecond.
    /// </summary>
    /// <param name="year">An integer that represents the year.</param>
    /// <param name="month">An integer that represents the month.</param>
    /// <param name="day">An integer that represents the day.</param>
    /// <param name="hour">An integer that represents the hour.</param>
    /// <param name="minute">An integer that represents the minute.</param>
    /// <param name="second">An integer that represents the second.</param>
    /// <param name="millisecond">An integer that represents the millisecond.</param>
    internal RepublicanDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
    {
        year.ValidateYear();
        month.ValidateMonth(year);
        day.ValidateDay(year, month);
        hour.ValidateHour();
        minute.ValidateMinute();
        second.ValidateSecond();
        millisecond.ValidateMillisecond();

        Year = year;
        Month = month;
        Day = day;
        Hour = hour;
        Minute = minute;
        Second = second;
        Millisecond = millisecond;
    }

    #endregion


    #region DateTime Parts

    /// <summary>
    /// The years.
    /// </summary>
    internal int Year { get; set; }


    /// <summary>
    /// The months.
    /// </summary>
    internal int Month { get; set; }


    /// <summary>
    /// The days.
    /// </summary>
    internal int Day { get; set; }


    /// <summary>
    /// The hours.
    /// </summary>
    internal int Hour { get; }


    /// <summary>
    /// The minutes.
    /// </summary>
    internal int Minute { get; }


    /// <summary>
    /// The seconds.
    /// </summary>
    internal int Second { get; }


    /// <summary>
    /// The milliseconds.
    /// </summary>
    internal int Millisecond { get; }

    #endregion


    #region Other Properties

    /// <summary>
    /// The time part of the <see cref="RepublicanDateTime"/>.
    /// </summary>
    internal TimeSpan TimeOfDay => new(0, Hour, Minute, Second, Millisecond);

    #endregion

}
