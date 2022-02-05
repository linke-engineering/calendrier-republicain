namespace Sinistrius.CalendrierRepublicain;


/// <summary>
/// Represents an instance in time expressed in the Republican calendar.
/// </summary>
internal struct RepublicanDateTime
{

    #region Local Fields

    /// <summary>
    /// A validator for date and time parts of the Republican calendar.
    /// </summary>
    private readonly RepublicanDateTimeValidator _validator = new();

    #endregion


    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="RepublicanDateTime"/> class from a Gregorian <see cref="DateTime"/>.
    /// </summary>
    /// <param name="gregDateTime">A <see cref="DateTime"/> that represents a date and time in the Gregorian calendar.</param>
    internal RepublicanDateTime(DateTime gregDateTime)
    {
        if ((gregDateTime < Globals.MinSupportedDateTime) || (gregDateTime > Globals.MaxSupportedDateTime))
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
        _validator.ValidateYear(year);
        _validator.ValidateMonth(year, month);
        _validator.ValidateDay(year, month, day);
        _validator.ValidateHour(hour);
        _validator.ValidateMinute(minute);
        _validator.ValidateSecond(second);
        _validator.ValidateMillisecond(millisecond);

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
