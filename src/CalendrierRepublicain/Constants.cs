using System.Diagnostics.CodeAnalysis;


namespace LinkeEngineering.CalendrierRepublicain;


/// <summary>
/// Defines common immutable calendar values.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class Constants
{

    /// <summary>
    /// Gets the earliest date and time supported by the Republican calendar.
    /// </summary>
    /// <remarks>The official epoch of the Republican calendar is 22 September 1792 or 1 Vendémiaire I. The calendar is not proleptic and doesn't cover earlier dates.</remarks>
    internal static DateTime MinSupportedDateTime => new(1792, 9, 22);


    /// <summary>
    /// Gets the latest date and time supported by the Republican calendar.
    /// </summary>
    /// <remarks>The Republican calendar was officially abolished at the end of the year 1805.</remarks>
    internal static DateTime MaxSupportedDateTime => new DateTime(1806, 1, 1).AddTicks(-1);


    /// <summary>
    /// The number of full months in a year of the Republican calendar.
    /// </summary>
    internal const int MonthsInYear = 12;


    /// <summary>
    /// The number of weeks in a month of the Republican calendar.
    /// </summary>
    internal const int WeeksInMonth = 3;


    /// <summary>
    /// The number of days in a week of the Republican calendar.
    /// </summary>
    internal const int DaysInWeek = 10;


    /// <summary>
    /// The number of complementary days in a common year of the Republican calendar.
    /// </summary>
    internal const int ComplementaryDays = 5;


    /// <summary>
    /// The last year of the Republican calendar (XIV).
    /// </summary>
    internal const int LastYear = 14;


    /// <summary>
    /// The last month in the last year of the Republican calendar (Nivôse XIV).
    /// </summary>
    internal const int LastMonth = 4;


    /// <summary>
    /// The last day in the last month of the last year of the Republican calendar (10 Nivôse XIV).
    /// </summary>
    internal const int DaysInLastMonth = 10;


    /// <summary>
    /// The number of the complementary month of a year in the Republican calendar.
    /// </summary>
    internal const int ComplementaryMonth = MonthsInYear + 1;


    /// <summary>
    /// The number of weeks in a year of the Republican calendar.
    /// </summary>
    internal const int WeeksInYear = MonthsInYear * WeeksInMonth;


    /// <summary>
    /// The number of days in a month of the Republican calendar.
    /// </summary>
    internal const int DaysInMonth = WeeksInMonth * DaysInWeek;


    /// <summary>
    /// The number of complementary days in a leap year of the Republican calendar.
    /// </summary>
    internal const int LeapComplementaryDays = ComplementaryDays + 1;

}
