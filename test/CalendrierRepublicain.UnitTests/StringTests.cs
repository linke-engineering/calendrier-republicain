using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;


namespace Sinistrius.CalendrierRepublicain.UnitTests;


/// <summary>
/// Tests the <see cref="String"/> class.
/// </summary>
[TestClass]
[ExcludeFromCodeCoverage]
public class StringTests
{

    /// <summary>
    /// Tests the <see cref="String.Format(IFormatProvider?, string, object?[])"/> method.
    /// </summary>
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
        FrenchRepublicanDateTime time = new(year, month, day, 1);
        format = $"{{0:{format}}}";

        // Act
        string actualString = String.Format(provider, format, time);

        // Assert
        Assert.AreEqual(expectedString, actualString);
    }


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
