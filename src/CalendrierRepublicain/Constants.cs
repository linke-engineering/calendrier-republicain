using System.Diagnostics.CodeAnalysis;


namespace Sinistrius.CalendrierRepublicain;


/// <summary>
/// Defines common functions or immutable values.
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
    /// The last year of the Republican calendar.
    /// </summary>
    internal static int LastRepublicanYear = 14;


    /// <summary>
    /// The last month in the last year of the Republican calendar.
    /// </summary>
    internal static int LastRepublicanMonth = 4;


    /// <summary>
    /// The last year of a 100-year range that can be represented by a 2-digit year.
    /// </summary>
    internal static int TwoDigitYearMax = 99;


    internal static string[] DayOfWeekNames = { "Primidi", "Duodi", "Tridi", "Quartidi", "Quintidi", "Sextiti", "Septidi", "Octidi", "Nonidi", "Décadi" };


    internal static string[] MonthNames = { "Vendémiaire", "Brumaire", "Frimaire", "Nivôse", "Pluviôse", "Ventôse", "Germinal", "Floréal", "Prairial", "Messidor", "Thermidor", "Fructidor" };


    internal static string[] AbbreviatedMonthNames = { "Vend", "Brum", "Frim", "Niv", "Pluv", "Vent", "Germ", "Flor", "Prair", "Mess", "Therm", "Fruct" };


    internal static string[] ComplementaryDayNames = { "Jour de la vertu", "Jour du génie", "Jour du travail", "Jour de l’opinion", "Jour des récompenses", "Jour de la révolution" };

}
