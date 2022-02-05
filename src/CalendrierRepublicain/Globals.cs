using System.Diagnostics.CodeAnalysis;


namespace Sinistrius.CalendrierRepublicain;


/// <summary>
/// Defines common functions or immutable values.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class Globals
{

    /// <summary>
    /// Gets the earliest date and time supported by the French Republican calendar.
    /// </summary>
    /// <remarks>The official epoch of the Republican calendar is 22 September 1792 or 1 Vendémiaire I. The calendar is not proleptic and doesn't cover earlier dates.</remarks>
    internal static DateTime MinSupportedDateTime => new DateTime(1792, 9, 22);


    /// <summary>
    /// Gets the latest date and time supported by the French Republican calendar.
    /// </summary>
    /// <remarks>The Republican calendar was officially abolished at the end of the year 1805.</remarks>
    internal static DateTime MaxSupportedDateTime => new DateTime(1806, 1, 1).AddTicks(-1);


    /// <summary>
    /// Determines whether the specified year in the Republican calendar was a leap year.
    /// </summary>
    /// <param name="repYear">An integer that represents the year in the Republican calendar.</param>
    /// <returns><see cref="true"/> if the year was a leap year, otherwise <see cref="false"/>.</returns>
    internal static bool IsLeapYear(int repYear)
    {
        return (repYear + 1) % 4 == 0;
    }

}
