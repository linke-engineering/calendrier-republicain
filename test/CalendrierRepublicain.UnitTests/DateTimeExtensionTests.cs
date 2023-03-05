namespace Sinistrius.CalendrierRepublicain.UnitTests;


/// <summary>
/// Tests the <see cref="DateTimeExtension"/> class.
/// </summary>
[TestClass]
public class DateTimeExtensionTests
{

    /// <summary>
    /// Tests the <see cref="DateTimeExtension.ToFrenchRepublicanTime(DateTime)"/> method.
    /// </summary>
    /// <param name="gregYear">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="gregMonth">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="gregDay">An integer that represents the day in the Gregorian calendar.</param>
    /// <param name="expectedRepYear">An integer that represents the expected year in the Republican calendar.</param>
    /// <param name="expectedRepMonth">An integer that represents the expected month in the Republican calendar.</param>
    /// <param name="expectedRepDay">An integer that represents the expected day in the Republican calendar.</param>
    [TestMethod]
    [DataRow(1792, 9, 22, 1, 1, 1)]
    [DataRow(1799, 11, 9, 8, 2, 18)]
    [DataRow(1805, 12, 31, 14, 4, 10)]
    public void ToFrenchRepublicanDateTime_GregorianDateTime_ReturnsRepublicanDateTime(int gregYear, int gregMonth, int gregDay, int expectedRepYear, int expectedRepMonth, int expectedRepDay)
    {
        // Arrange
        int hour = 13;
        int minute = 14;
        int second = 15;
        int millisecond = 160;
        DateTime gregTime = new(gregYear, gregMonth, gregDay, hour, minute, second, millisecond);

        // Act
        FrenchRepublicanDateTime actualRepTime = gregTime.ToFrenchRepublicanTime();

        // Assert
        Assert.AreEqual(expectedRepYear, actualRepTime.Year);
        Assert.AreEqual(expectedRepMonth, actualRepTime.Month);
        Assert.AreEqual(expectedRepDay, actualRepTime.Day);
        Assert.AreEqual(hour, actualRepTime.Hour);
        Assert.AreEqual(minute, actualRepTime.Minute);    
        Assert.AreEqual(second, actualRepTime.Second);
        Assert.AreEqual(millisecond, actualRepTime.Millisecond);
    }

}
