using System.Diagnostics.CodeAnalysis;


namespace Sinistrius.CalendrierRepublicain;


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
    /// The last year of the Republican calendar.
    /// </summary>
    internal static int LastRepublicanYear = 14;


    /// <summary>
    /// The last month in the last year of the Republican calendar.
    /// </summary>
    internal static int LastRepublicanMonth = 4;

}
