using System.Diagnostics.CodeAnalysis;


namespace Sinistrius.CalendrierRepublicain;


/// <summary>
/// Provides some date/time format infos, based on <see cref="System.Globalization.DateTimeFormatInfo"/>.
/// </summary>
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
    /// The abbreviated names of the months of a year.
    /// </summary>
    internal string[] AbbreviatedMonthNames = { "Vend", "Brum", "Frim", "Niv", "Pluv", "Vent", "Germ", "Flor", "Prair", "Mess", "Therm", "Fruct" };


    /// <summary>
    /// The names of the complementary days at the end of a year.
    /// </summary>
    internal string[] ComplementaryDayNames = { "Jour de la vertu", "Jour du génie", "Jour du travail", "Jour de l’opinion", "Jour des récompenses", "Jour de la révolution" };

}
