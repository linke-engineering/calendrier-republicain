namespace LinkeEngineering.CalendrierRepublicain.UnitTests;


/// <summary>
/// Tests the <see cref="String"/> class.
/// </summary>
[TestClass]
public class StringTests
{

    /// <summary>
    /// Tests the <see cref="String.Format(IFormatProvider?, string, object?[])"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    /// <param name="format">A string that represents the format specifier.</param>
    /// <param name="expectedString">A string that represents the expected output.</param>
    [TestMethod]
    [DataRow(1792, 9, 22, "D", "Primidi, 1. Vendémiaire I")]
    [DataRow(1792, 9, 22, "d. MMM. an yyyy", "1. Vend. an I")]
    [DataRow(1795, 9, 22, "D", "Jour de la révolution III")]
    [DataRow(1795, 9, 22, "d. MMM. yyyy", "Jour de la révolution III")]
    [DataRow(1799, 11, 9, "D", "Octidi, 18. Brumaire VIII")]
    [DataRow(1805, 12, 31, "D", "Décadi, 10. Nivôse XIV")]
    [DataRow(1805, 12, 31, "d. MMM. yyyy", "10. Niv. XIV")]
    public void Format_GeorgianDate_ReturnsFormattedString(int year, int month, int day, string format, string expectedString)
    {
        // Arrange
        FrenchRepublicanDateTimeFormatter provider = new();
        DateTime time = new(year, month, day);
        format = $"{{0:{format}}}";

        // Act
        string actualString = String.Format(provider, format, time);

        // Assert
        Assert.AreEqual(expectedString, actualString);
    }


    /// <summary>
    /// Tests the <see cref="String.Format(IFormatProvider?, string, object?[])"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Republican calendar.</param>
    /// <param name="month">An integer that represents the month in the Republican calendar.</param>
    /// <param name="day">An integer that represents the day in the Republican calendar.</param>
    /// <param name="format">A string that represents the format specifier.</param>
    /// <param name="expectedString">A string that represents the expected output.</param>
    [TestMethod]
    [DataRow(1, 1, 1, "D", "Primidi, 1. Vendémiaire I")]
    [DataRow(1, 1, 1, "d. MMMM yyyy", "1. Vendémiaire I")]
    [DataRow(1, 1, 1, "d. MMM. yyyy", "1. Vend. I")]
    [DataRow(3, 13, 6, "D", "Jour de la révolution III")]
    [DataRow(3, 13, 6, "d. MMMM yyyy", "Jour de la révolution III")]
    [DataRow(3, 13, 6, "d. MMM. yyyy", "Jour de la révolution III")]
    [DataRow(14, 4, 10, "D", "Décadi, 10. Nivôse XIV")]
    [DataRow(14, 4, 10, "d. MMMM yyyy", "10. Nivôse XIV")]
    [DataRow(14, 4, 10, "d. MMM. yyyy", "10. Niv. XIV")]
    public void Format_FrenchRepublicanDate_ReturnsFormattedString(int year, int month, int day, string format, string expectedString)
    {
        // Arrange
        FrenchRepublicanDateTimeFormatter provider = new();
        DateTime time = new(year, month, day, new FrenchRepublicanCalendar());
        format = $"{{0:{format}}}";

        // Act
        string actualString = String.Format(provider, format, time);

        // Assert
        Assert.AreEqual(expectedString, actualString);
    }

}
