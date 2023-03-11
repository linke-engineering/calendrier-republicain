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

    #endregion


    #region Properties

    /// <summary>
    /// The Julian day number value.
    /// </summary>
    internal int Value { get; }

    #endregion

}
