using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using GregorianDateTime = System.DateTime;


namespace Sinistrius.CalendrierRepublicain.UnitTests;


/// <summary>
/// Tests the <see cref="RepublicanDateTimeExtension"/> class.
/// </summary>
[TestClass]
[ExcludeFromCodeCoverage]
public class RepublicanDateTimeExtensionTests
{

    /// <summary>
    /// Tests the <see cref="RepublicanDateTimeExtension.ToGregorian(RepublicanDateTime)"/> method.
    /// </summary>
    /// <param name="repYear">An integer that represents the year in the Republican calendar.</param>
    /// <param name="repMonth">An integer that represents the month in the Republican calendar.</param>
    /// <param name="repDay">An integer that represents the day in the Republican calendar.</param>
    /// <param name="gregYear">An integer that represents the expected year in the Gregorian calendar.</param>
    /// <param name="gregMonth">An integer that represents the expected month in the Gregorian calendar.</param>
    /// <param name="gregDay">An integer that represents the expected day in the Gregorian calendar.</param>
    [TestMethod]
    [DataRow(1, 1, 1, 1792, 9, 22)]
    [DataRow(8, 2, 18, 1799, 11, 9)]
    [DataRow(14, 4, 10, 1805, 12, 31)]
    public void ToGregorian_RepublicanDateTime_ReturnsGregorianDateTime(int repYear, int repMonth, int repDay, int gregYear, int gregMonth, int gregDay)
    {
        // Arrange
        Random random = new();
        int hour = random.Next(24);
        int minute = random.Next(60);
        int second = random.Next(60);
        int millisecond = random.Next(1000);
        RepublicanDateTime repDateTime = new(repYear, repMonth, repDay, hour, minute, second, millisecond);

        // Act
        GregorianDateTime gregDateTime = repDateTime.ToGregorian();

        // Assert
        Assert.AreEqual(gregYear, gregDateTime.Year);
        Assert.AreEqual(gregMonth, gregDateTime.Month);
        Assert.AreEqual(gregDay, gregDateTime.Day);
        Assert.AreEqual(hour, gregDateTime.Hour);
        Assert.AreEqual(minute, gregDateTime.Minute);
        Assert.AreEqual(second, gregDateTime.Second);
        Assert.AreEqual(millisecond, gregDateTime.Millisecond);
    }

}
