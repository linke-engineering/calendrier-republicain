namespace Sinistrius.CalendrierRepublicain;


/// <summary>
/// A Julian day number object, used for conversion between calendars.
/// </summary>
internal class JulianDayNumber
{

    #region Constructors

    /// <summary>
    /// Initializes a <see cref="JulianDayNumber"/>.
    /// </summary>
    /// <param name="time">The time.</param>
    internal JulianDayNumber(DateTime time)
    {
        Value = (int)(time.Date.ToOADate() + 2415018.5);
    }


    /// <summary>
    /// Initializes a <see cref="JulianDayNumber"/>.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Republican calendar.</param>
    /// <param name="month">An integer that represents the month in the Republican calendar.</param>
    /// <param name="day">An integer that represents the day in the Republican calendar.</param>
    internal JulianDayNumber(int year, int month, int day) : this(new DateTime(year, month, day, new FrenchRepublicanCalendar()))
    { }

    #endregion


    #region Properties

    /// <summary>
    /// The Julian day number value.
    /// </summary>
    internal int Value { get; }

    #endregion


    #region Methods

    /// <summary>
    /// Converts the Julian day number into a Gregorian time.
    /// </summary>
    /// <returns>The Gregorian time.</returns>
    internal DateTime ToGregorianTime()
    {
        return DateTime.FromOADate(Value - 2415018.5);
    }

    #endregion

}
