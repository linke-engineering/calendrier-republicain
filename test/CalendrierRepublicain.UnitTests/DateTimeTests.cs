namespace Sinistrius.CalendrierRepublicain.UnitTests;


/// <summary>
/// Tests the <see cref="DateTime"/> class.
/// </summary>
[TestClass]
public class DateTimeTests
{

    /// <summary>
    /// Tests the <see cref="DateTime(int, int, int, System.Globalization.Calendar)"/> constructor.
    /// </summary>
    /// <param name="repYear">An integer that represents the year in the Republican calendar.</param>
    /// <param name="repMonth">An integer that represents the month in the Republican calendar.</param>
    /// <param name="repDay">An integer that represents the day in the Republican calendar.</param>
    [TestMethod]
    [DataRow(1, 1, 1)]
    [DataRow(3, 13, 5)]
    [DataRow(8, 2, 18)]
    [DataRow(14, 4, 10)]
    public void Constructor_ValidRepublicanDate_CreatesDate(int repYear, int repMonth, int repDay)
    {
        // Arrange
        FrenchRepublicanCalendar repCalendar = new();

        // Act
        DateTime repDate = new(repYear, repMonth, repDay, repCalendar);
        int actualEra = repCalendar.GetEra(repDate);
        int actualYear = repCalendar.GetYear(repDate);
        int actualMonth = repCalendar.GetMonth(repDate);
        int actualDay = repCalendar.GetDayOfMonth(repDate);

        // Assert
        Assert.AreEqual(1, actualEra);
        Assert.AreEqual(repYear, actualYear);
        Assert.AreEqual(repMonth, actualMonth);
        Assert.AreEqual(repDay, actualDay);
    }

}
