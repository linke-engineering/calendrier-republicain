using System.Diagnostics.CodeAnalysis;


namespace LinkeEngineering.CalendrierRepublicain;


/// <summary>
/// Provides some date/time format infos, based on <see cref="System.Globalization.DateTimeFormatInfo"/>.
/// </summary>
/// <remarks>
/// Save this file in "Unicode (UTF-8 with signature)" encoding to avoid issues with accented characters in the CI workflow.
/// </remarks>

[ExcludeFromCodeCoverage]
internal class FrenchRepublicanDateTimeFormatInfo
{

    /// <summary>
    /// The names of the days of a décade (week of ten days).
    /// </summary>
    internal string[] DayNames =
    [
        "Primidi",   // décade day 1
        "Duodi",     // décade day 2
        "Tridi",     // décade day 3
        "Quartidi",  // décade day 4
        "Quintidi",  // décade day 5
        "Sextiti",   // décade day 6
        "Septidi",   // décade day 7
        "Octidi",    // décade day 8
        "Nonidi",    // décade day 9
        "Décadi"     // décade day 10
    ];


    /// <summary>
    /// The names of the months of a year.
    /// </summary>
    internal string[] MonthNames =
    [
        "Vendémiaire",           // month 1
        "Brumaire",              // month 2
        "Frimaire",              // month 3
        "Nivôse",                // month 4
        "Pluviôse",              // month 5
        "Ventôse",               // month 6
        "Germinal",              // month 7
        "Floréal",               // month 8
        "Prairial",              // month 9
        "Messidor",              // month 10
        "Thermidor",             // month 11
        "Fructidor",             // month 12
        "Jours complémentaires"  // complementary days
    ];


    /// <summary>
    /// The names of the complementary days at the end of a year.
    /// </summary>
    internal string[] ComplementaryDayNames =
    [
        "Jour de la vertu",      // complementary day 1
        "Jour du génie",         // complementary day 2
        "Jour du travail",       // complementary day 3
        "Jour de l’opinion",     // complementary day 4
        "Jour des récompenses",  // complementary day 5
        "Jour de la révolution"  // complementary day 6 (only in leap years)
    ];

}
