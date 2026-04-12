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
    /// The names of the days of a decade.
    /// </summary>
    internal string[] DayNames = { "Primidi", "Duodi", "Tridi", "Quartidi", "Quintidi", "Sextiti", "Septidi", "Octidi", "Nonidi", "Décadi" };


    /// <summary>
    /// The names of the months of a year.
    /// </summary>
    internal string[] MonthNames = { "Vendémiaire", "Brumaire", "Frimaire", "Nivôse", "Pluviôse", "Ventôse", "Germinal", "Floréal", "Prairial", "Messidor", "Thermidor", "Fructidor" };


    /// <summary>
    /// The names of the complementary days at the end of a year.
    /// </summary>
    internal string[] ComplementaryDayNames = { "Jour de la vertu", "Jour du génie", "Jour du travail", "Jour de l’opinion", "Jour des récompenses", "Jour de la révolution" };

}
