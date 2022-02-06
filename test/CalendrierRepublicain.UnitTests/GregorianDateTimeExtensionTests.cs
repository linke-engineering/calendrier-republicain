using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sinistrius.CalendrierRepublicain.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;
using GregorianDateTime = System.DateTime;


namespace Sinistrius.CalendrierRepublicain.UnitTests;


/// <summary>
/// Tests the <see cref="GregorianDateTimeExtension"/> class.
/// </summary>
[TestClass]
[ExcludeFromCodeCoverage]
public class GregorianDateTimeExtensionTests
{

    /// <summary>
    /// Tests the <see cref="GregorianDateTimeExtension.ToRepublican(GregorianDateTime)"/> method.
    /// </summary>
    /// <param name="gregYear">An integer that represents the expected year in the Gregorian calendar.</param>
    /// <param name="gregMonth">An integer that represents the expected month in the Gregorian calendar.</param>
    /// <param name="gregDay">An integer that represents the expected day in the Gregorian calendar.</param>
    /// <param name="repYear">An integer that represents the year in the Republican calendar.</param>
    /// <param name="repMonth">An integer that represents the month in the Republican calendar.</param>
    /// <param name="repDay">An integer that represents the day in the Republican calendar.</param>
    [TestMethod]
    [DataRow(1792, 9, 22, 1, 1, 1)]
    [DataRow(1799, 11, 9, 8, 2, 18)]
    [DataRow(1805, 12, 31, 14, 4, 10)]
    public void ToRepublican_GregorianDateTime_ReturnsRepublicanDateTime(int gregYear, int gregMonth, int gregDay, int repYear, int repMonth, int repDay)
    {
        // Arrange
        Random random = new();
        int hour = random.Next(24);
        int minute = random.Next(60);
        int second = random.Next(60);
        int millisecond = random.Next(1000);
        GregorianDateTime gregDateTime = new(gregYear, gregMonth, gregDay, hour, minute, second, millisecond);

        // Act
        RepublicanDateTime repDateTime = gregDateTime.ToRepublican();

        // Assert
        Assert.AreEqual(repYear, repDateTime.Year);
        Assert.AreEqual(repMonth, repDateTime.Month);
        Assert.AreEqual(repDay, repDateTime.Day);
        Assert.AreEqual(hour, repDateTime.Hour);
        Assert.AreEqual(minute, repDateTime.Minute);
        Assert.AreEqual(second, repDateTime.Second);
        Assert.AreEqual(millisecond, repDateTime.Millisecond);
    }

}
