namespace LinkeEngineering.CalendrierRepublicain.UnitTests;


/// <summary>
/// Tests the <see cref="DateOnly"/> class.
/// </summary>
[TestClass]
public class DateOnlyTests
{

    /// <summary>
    /// Tests the <see cref="DateOnly(int, int, int, System.Globalization.Calendar)"/> constructor.
    /// </summary>
    /// <param name="repYear">An integer that represents the year in the Republican calendar.</param>
    /// <param name="repMonth">An integer that represents the month in the Republican calendar.</param>
    /// <param name="repDay">An integer that represents the day in the Republican calendar.</param>
    [TestMethod]
    [DataRow(1, 1, 1)]
    [DataRow(1, 13, 5)]
    [DataRow(2, 1, 1)]
    [DataRow(3, 13, 6)]
    [DataRow(8, 2, 18)]
    [DataRow(14, 4, 10)]
    public void Constructor_ValidRepublicanDate_CreatesDate(int repYear, int repMonth, int repDay)
    {
        // Arrange
        FrenchRepublicanCalendar repCalendar = new();

        // Act
        DateOnly dateOnly = new(repYear, repMonth, repDay, repCalendar);
        DateTime dateTime = dateOnly.ToDateTime(TimeOnly.MinValue);

        int actualEra = repCalendar.GetEra(dateTime);
        int actualYear = repCalendar.GetYear(dateTime);
        int actualMonth = repCalendar.GetMonth(dateTime);
        int actualDay = repCalendar.GetDayOfMonth(dateTime);

        // Assert
        Assert.AreEqual(1, actualEra);
        Assert.AreEqual(repYear, actualYear);
        Assert.AreEqual(repMonth, actualMonth);
        Assert.AreEqual(repDay, actualDay);
    }

}
