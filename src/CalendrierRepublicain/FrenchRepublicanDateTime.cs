using Sinistrius.CalendrierRepublicain.Extensions;


namespace Sinistrius.CalendrierRepublicain;


/// <summary>
/// Represents an instance in time expressed in the Republican calendar.
/// </summary>
internal class FrenchRepublicanDateTime
{

    #region Local Fields

    /// <summary>
    /// A Republican calendar object.
    /// </summary>
    private readonly FrenchRepublicanCalendar _calendar = new();

    #endregion


    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FrenchRepublicanDateTime"/> class from a Gregorian <see cref="DateTime"/>.
    /// </summary>
    /// <param name="gregDateTime">A <see cref="DateTime"/> that represents a date and time in the Gregorian calendar.</param>
    internal FrenchRepublicanDateTime(DateTime gregDateTime)
    {
        gregDateTime.Validate();

        FrenchRepublicanDateTime repDateTime = gregDateTime.ToRepublican();

        Year = 
            repDateTime.Year;
        Month = repDateTime.Month;
        Day = repDateTime.Day;
        TimeOfDay = gregDateTime.TimeOfDay;
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="FrenchRepublicanDateTime"/> class to the specified Republican year, month and day.
    /// </summary>
    /// <param name="year">An integer that represents the year.</param>
    /// <param name="month">An integer that represents the month.</param>
    /// <param name="day">An integer that represents the day.</param>
    private FrenchRepublicanDateTime(int year, int month, int day)
    {
        _calendar.ValidateDay(year, month, day);

        Year = year;
        Month = month;
        Day = day;
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="FrenchRepublicanDateTime"/> class to the specified Republican year, month, day, hour, minute, second and millisecond.
    /// </summary>
    /// <param name="year">An integer that represents the year.</param>
    /// <param name="month">An integer that represents the month.</param>
    /// <param name="day">An integer that represents the day.</param>
    /// <param name="hour">An integer that represents the hour.</param>
    /// <param name="minute">An integer that represents the minute.</param>
    /// <param name="second">An integer that represents the second.</param>
    /// <param name="millisecond">An integer that represents the millisecond.</param>
    internal FrenchRepublicanDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond) : this(year, month, day)
    {
        _calendar.ValidateTime(hour, minute, second, millisecond);

        Hour = hour;
        Minute = minute;
        Second = second;
        Millisecond = millisecond;
        TimeOfDay = new TimeSpan(0, hour, minute, second, millisecond);
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="FrenchRepublicanDateTime"/> class to the specified Republican year, month, day and time span.
    /// </summary>
    /// <param name="year">An integer that represents the year.</param>
    /// <param name="month">An integer that represents the month.</param>
    /// <param name="day">An integer that represents the day.</param>
    /// <param name="timeOfDay">A time span that represents the time of the day.</param>
    internal FrenchRepublicanDateTime(int year, int month, int day, TimeSpan timeOfDay) : this(year, month, day)
    {
        TimeOfDay = timeOfDay;
        Hour = timeOfDay.Hours;
        Minute = timeOfDay.Minutes;
        Second = timeOfDay.Seconds;
        Millisecond = timeOfDay.Milliseconds;
    }

    #endregion


    #region Properties

    /// <summary>
    /// The era.
    /// </summary>
    internal int Era { get; } = 1;


    /// <summary>
    /// The year.
    /// </summary>
    internal int Year { get; private set; }


    /// <summary>
    /// The month.
    /// </summary>
    internal int Month { get; private set; }


    /// <summary>
    /// The day.
    /// </summary>
    internal int Day { get; private set; }


    /// <summary>
    /// The hour.
    /// </summary>
    internal int Hour { get; }


    /// <summary>
    /// The minute.
    /// </summary>
    internal int Minute { get; }


    /// <summary>
    /// The second.
    /// </summary>
    internal int Second { get; }


    /// <summary>
    /// The millisecond.
    /// </summary>
    internal int Millisecond { get; }


    /// <summary>
    /// The time of day for this instance.
    /// </summary>
    internal TimeSpan TimeOfDay { get; } = new();


    /// <summary>
    /// Determines whether this <see cref="FrenchRepublicanDateTime"/> is in the complementary days section.
    /// </summary>
    internal bool IsComplementaryDays => Month == 13;


    /// <summary>
    /// Determines whether this <see cref="FrenchRepublicanDateTime"/> is Jour de la Révolution, the additional complementary day of a leap year.
    /// </summary>
    internal bool IsJourDeLaRevolution => _calendar.IsLeapDay(Year, Month, Day);

    #endregion


    #region Methods

    /// <summary>
    /// Gets the last day of the previous month.
    /// </summary>
    /// <returns></returns>
    internal FrenchRepublicanDateTime EndOfPreviousMonth()
    {
        FrenchRepublicanDateTime repTime = this;
        repTime.Month--;

        if (Month == 0)
        {
            Month = 12;
            Year--;
        }

        repTime.Day = 30;
        return repTime;
    }


    /// <summary>
    /// Gets the first day of the following year.
    /// </summary>
    /// <returns></returns>
    internal FrenchRepublicanDateTime StartOfNextYear()
    {
        FrenchRepublicanDateTime repTime = this;
        repTime.Year++;
        repTime.Month = 1;
        repTime.Day = 1;
        return repTime;
    }

    #endregion

}
