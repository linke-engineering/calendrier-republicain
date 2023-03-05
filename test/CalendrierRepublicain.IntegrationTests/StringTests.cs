namespace Sinistrius.CalendrierRepublicain.IntegrationTests;


/// <summary>
/// Tests the <see cref="String"/> class.
/// </summary>
[TestClass]
public class StringTests
{

    /// <summary>
    /// Tests the <see cref="String.Format(IFormatProvider?, string, object?[])"/> method.
    /// </summary>
    [TestMethod]
    [DataRow(1792, 9, 22, "Primidi, 1. Vendémiaire I")]
    [DataRow(1795, 9, 22, "Jour de la révolution III")]
    [DataRow(1805, 12, 31, "Décadi, 10. Nivôse XIV")]
    public void Format_GeorgianDate_LongDatePattern_ReturnsFormattedString(int year, int month, int day, string expectedString)
    {
        // Arrange
        FrenchRepublicanDateTimeFormatter provider = new();
        DateTime time = new(year, month, day);

        // Act
        string actualString = String.Format(provider, "{0:D}", time);

        // Assert
        Assert.AreEqual(expectedString, actualString);
    }

}
