namespace Sinistrius.CalendrierRepublicain.UnitTests;


/// <summary>
/// Tests the <see cref="FrenchRepublicanDateTime"/> class.
/// </summary>
[TestClass]
public class FrenchRepublicanDateTimeTests
{

    #region Local Fields

    private const int Era = 1;
    private const int Hour = 22;
    private const int Minute = 33;
    private const int Second = 44;
    private const int Millisecond = 555;

    #endregion


    #region Initialization

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanDateTime(int, int, int, int, int, int, int)"/> constructor with valid input.
    /// </summary>
    /// <param name="repYear">An integer that represents the year in the Republican calendar.</param>
    /// <param name="repMonth">An integer that represents the month in the Republican calendar.</param>
    /// <param name="repDay">An integer that represents the day in the Republican calendar.</param>
    [TestMethod]
    [DataRow(1, 1, 1)]
    [DataRow(8, 2, 18)]
    [DataRow(14, 4, 10)]
    public void Initialize_ValidRepublicanTime_CreatesRepublicanTime(int repYear, int repMonth, int repDay)
    {
        // Act
        FrenchRepublicanDateTime actualTime = new(repYear, repMonth, repDay, Hour, Minute, Second, Millisecond, Era);

        // Assert
        Assert.AreEqual(repYear, actualTime.Year);
        Assert.AreEqual(repMonth, actualTime.Month);
        Assert.AreEqual(repDay, actualTime.Day);
        Assert.AreEqual(Hour, actualTime.Hour);
        Assert.AreEqual(Minute, actualTime.Minute);
        Assert.AreEqual(Second, actualTime.Second);
        Assert.AreEqual(Millisecond, actualTime.Millisecond);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanDateTime(int, int, int, int, int, int, int, int)"/> constructor with an invalid year parameter.
    /// </summary>
    /// <param name="repYear">An integer that represents the year in the Republican calendar.</param>
    [TestMethod]
    [DataRow(-1)]
    [DataRow(0)]
    [DataRow(15)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Initialize_InvalidRepublicanYear_ThrowsArgumentOutOfRangeException(int repYear)
    {
        // Act
        _ = new FrenchRepublicanDateTime(repYear, 1, 1, Hour, Minute, Second, Millisecond, Era);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanDateTime(int, int, int, int, int, int, int, int)"/> constructor with an invalid month parameter.
    /// </summary>
    /// <param name="repYear">An integer that represents the year in the Republican calendar.</param>
    /// <param name="repMonth">An integer that represents the month in the Republican calendar.</param>
    [TestMethod]
    [DataRow(1, -1)]
    [DataRow(1, 0)]
    [DataRow(1, 14)]
    [DataRow(14, 5)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Initialize_InvalidRepublicanMonth_ThrowsArgumentOutOfRangeException(int repYear, int repMonth)
    {
        // Act
        _ = new FrenchRepublicanDateTime(repYear, repMonth, 1, Hour, Minute, Second, Millisecond, Era);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanDateTime(int, int, int, int, int, int, int, int)"/> constructor with an invalid day parameter.
    /// </summary>
    /// <param name="repYear">An integer that represents the year in the Republican calendar.</param>
    /// <param name="repMonth">An integer that represents the month in the Republican calendar.</param>
    /// <param name="repDay">An integer that represents the day in the Republican calendar.</param>
    [TestMethod]
    [DataRow(1, 1, -1)]
    [DataRow(1, 1, 0)]
    [DataRow(1, 1, 31)]
    [DataRow(1, 13, 6)]
    [DataRow(14, 4, 11)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Initialize_InvalidRepublicanDay_ThrowsArgumentOutOfRangeException(int repYear, int repMonth, int repDay)
    {
        // Act
        _ = new FrenchRepublicanDateTime(repYear, repMonth, repDay, Hour, Minute, Second, Millisecond, Era);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanDateTime(int, int, int, int, int, int, int, int)"/> constructor with an invalid hour parameter.
    /// </summary>
    /// <param name="hour">An integer that represents the hour in the Republican calendar.</param>
    [TestMethod]
    [DataRow(-1)]
    [DataRow(24)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Initialize_InvalidHour_ThrowsArgumentOutOfRangeException(int hour)
    {
        // Act
        _ = new FrenchRepublicanDateTime(1, 1, 1, hour, Minute, Second, Millisecond, Era);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanDateTime(int, int, int, int, int, int, int, int)"/> constructor with an invalid minute parameter.
    /// </summary>
    /// <param name="minute">An integer that represents the minute in the Republican calendar.</param>
    [TestMethod]
    [DataRow(-1)]
    [DataRow(60)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Initialize_InvalidMinute_ThrowsArgumentOutOfRangeException(int minute)
    {
        // Act
        _ = new FrenchRepublicanDateTime(1, 1, 1, Hour, minute, Second, Millisecond, Era);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanDateTime(int, int, int, int, int, int, int, int)"/> constructor with an invalid second parameter.
    /// </summary>
    /// <param name="second">An integer that represents the second in the Republican calendar.</param>
    [TestMethod]
    [DataRow(-1)]
    [DataRow(60)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Initialize_InvalidSecond_ThrowsArgumentOutOfRangeException(int second)
    {
        // Act
        _ = new FrenchRepublicanDateTime(1, 1, 1, Hour, Minute, second, Millisecond, Era);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanDateTime(int, int, int, int, int, int, int, int)"/> constructor with an invalid millisecond parameter.
    /// </summary>
    /// <param name="millisecond">An integer that represents the millisecond in the Republican calendar.</param>
    [TestMethod]
    [DataRow(-1)]
    [DataRow(1000)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Initialize_InvalidMillisecond_ThrowsArgumentOutOfRangeException(int millisecond)
    {
        // Act
        _ = new FrenchRepublicanDateTime(1, 1, 1, Hour, Minute, Second, millisecond, Era);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanDateTime(int, int, int, int, int, int, int, int)"/> constructor with an invalid era parameter.
    /// </summary>
    /// <param name="era">An integer that represents the era in the Republican calendar.</param>
    [TestMethod]
    [DataRow(0)]
    [DataRow(2)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Initialize_InvalidEra_ThrowsArgumentOutOfRangeException(int era)
    {
        // Act
        _ = new FrenchRepublicanDateTime(1, 1, 1, Hour, Minute, Second, Millisecond, era);
    }

    #endregion


    #region AddMonths

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanDateTime.AddMonths(int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Republican calendar.</param>
    /// <param name="month">An integer that represents the month in the Republican calendar.</param>
    /// <param name="day">An integer that represents the day in the Republican calendar.</param>
    /// <param name="months">An integer that represents the months to be added.</param>
    /// <param name="expectedYear">An integer that represents the expected year in the Republican calendar after the addition.</param>
    /// <param name="expectedMonth">An integer that represents the expected month in the Republican calendar after the addition.</param>
    /// <param name="expectedDay">An integer that represents the expected day in the Republican calendar after the addition.</param>
    [TestMethod]
    [DataRow(1, 1, 1, 0, 1, 1, 1)]
    [DataRow(1, 1, 1, 23, 2, 12, 1)]
    [DataRow(1, 1, 1, 24, 3, 1, 1)]
    [DataRow(1, 1, 1, 25, 3, 2, 1)]
    [DataRow(2, 12, 1, -23, 1, 1, 1)]
    [DataRow(3, 1, 1, -24, 1, 1, 1)]
    [DataRow(3, 2, 1, -25, 1, 1, 1)]
    public void AddMonths_ValidDate_ReturnsModifiedDate(int year, int month, int day, int months, int expectedYear, int expectedMonth, int expectedDay)
    {
        // Arrange
        FrenchRepublicanDateTime time = new(year, month, day, Era);

        // Act
        FrenchRepublicanDateTime actualTime = time.AddMonths(months);

        // Assert
        Assert.AreEqual(expectedYear, actualTime.Year);
        Assert.AreEqual(expectedMonth, actualTime.Month);
        Assert.AreEqual(expectedDay, actualTime.Day);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.AddMonths(DateTime, int)"/> method.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void AddMonths_DateOutOfRange_ThrowsInvalidOperationException()
    {
        // Arrange
        FrenchRepublicanDateTime time = new(1, 13, 1, Era);

        // Act
        _ = time.AddMonths(1);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.AddMonths(DateTime, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Republican calendar.</param>
    /// <param name="month">An integer that represents the month in the Republican calendar.</param>
    /// <param name="day">An integer that represents the day in the Republican calendar.</param>
    /// <param name="months">An integer that represents the months to be added.</param>
    [TestMethod]
    [DataRow(1, 1, 1, -1)]
    [DataRow(1, 12, 1, -12)]
    [DataRow(14, 3, 11, 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AddMonths_AdditionExceedsValidity_ThrowsArgumentOutOfRangeException(int year, int month, int day, int months)
    {
        // Arrange
        FrenchRepublicanDateTime time = new(year, month, day, Era);
        // Act
        _ = time.AddMonths(months);
    }

    #endregion


    #region AddWeeks

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.AddWeeks(DateTime, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Republican calendar.</param>
    /// <param name="month">An integer that represents the month in the Republican calendar.</param>
    /// <param name="day">An integer that represents the day in the Republican calendar.</param>
    /// <param name="weeks">An integer that represents the weeks to be added.</param>
    /// <param name="expectedYear">An integer that represents the expected year in the Republican calendar after the addition.</param>
    /// <param name="expectedMonth">An integer that represents the expected month in the Republican calendar after the addition.</param>
    /// <param name="expectedDay">An integer that represents the expected day in the Republican calendar after the addition.</param>
    [TestMethod]
    [DataRow(1, 1, 1, 0, 1, 1, 1)]
    [DataRow(1, 1, 1, 2, 1, 1, 21)]
    [DataRow(1, 1, 1, 6, 1, 3, 1)]
    [DataRow(1, 1, 1, 35, 1, 12, 21)]
    [DataRow(1, 1, 1, 71, 2, 12, 21)]
    [DataRow(2, 12, 21, -71, 1, 1, 1)]
    [DataRow(1, 12, 21, -35, 1, 1, 1)]
    [DataRow(1, 3, 1, -6, 1, 1, 1)]

    public void AddWeeks_ValidDate_ReturnsModifiedDate(int year, int month, int day, int weeks, int expectedYear, int expectedMonth, int expectedDay)
    {
        // Arrange
        FrenchRepublicanDateTime time = new(year, month, day, Era);

        // Act
        FrenchRepublicanDateTime actualTime = time.AddWeeks(weeks);

        // Assert
        Assert.AreEqual(expectedYear, actualTime.Year);
        Assert.AreEqual(expectedMonth, actualTime.Month);
        Assert.AreEqual(expectedDay, actualTime.Day);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.AddWeeks(DateTime, int)"/> method.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void AddWeeks_DateOutOfRange_ThrowsInvalidOperationException()
    {
        // Arrange
        FrenchRepublicanDateTime time = new(1, 13, 1, Era);

        // Act
        _ = time.AddWeeks(1);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.AddWeeks(DateTime, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Republican calendar.</param>
    /// <param name="month">An integer that represents the month in the Republican calendar.</param>
    /// <param name="day">An integer that represents the day in the Republican calendar.</param>
    /// <param name="weeks">An integer that represents the weeks to be added.</param>
    [TestMethod]
    [DataRow(1, 1, 1, -1)]
    [DataRow(1, 12, 1, -36)]
    [DataRow(14, 4, 1, 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AddWeeks_AdditionExceedsValidity_ThrowsArgumentOutOfRangeException(int year, int month, int day, int weeks)
    {
        // Arrange
        FrenchRepublicanDateTime time = new(year, month, day, Era);

        // Act
        _ = time.AddWeeks(weeks);
    }

    #endregion


    #region AddYears

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.AddYears(DateTime, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Republican calendar.</param>
    /// <param name="month">An integer that represents the month in the Republican calendar.</param>
    /// <param name="day">An integer that represents the day in the Republican calendar.</param>
    /// <param name="years">An integer that represents the years to be added.</param>
    /// <param name="expectedYear">An integer that represents the expected year in the Republican calendar after the addition.</param>
    [TestMethod]
    [DataRow(1, 1, 1, 1, 2)]
    [DataRow(1, 2, 1, 13, 14)]
    [DataRow(14, 2, 1, -13, 1)]
    public void AddYear_ValidDate_ReturnsModifiedDate(int year, int month, int day, int years, int expectedYear)
    {
        // Arrange
        FrenchRepublicanDateTime time = new(year, month, day, Era);

        // Act
        FrenchRepublicanDateTime actualTime = time.AddYears(years);

        // Assert
        Assert.AreEqual(expectedYear, actualTime.Year);
        Assert.AreEqual(month, actualTime.Month);
        Assert.AreEqual(day, actualTime.Day);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.AddYears(DateTime, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Republican calendar.</param>
    /// <param name="month">An integer that represents the month in the Republican calendar.</param>
    /// <param name="day">An integer that represents the day in the Republican calendar.</param>
    /// <param name="years">An integer that represents the years to be added.</param>
    [TestMethod]
    [DataRow(1, 1, 1, -1)]
    [DataRow(13, 4, 11, 1)]
    [DataRow(14, 4, 10, -14)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AddYears_AdditionExceedsValidity_ThrowsArgumentOutOfRangeException(int year, int month, int day, int years)
    {
        // Arrange
        FrenchRepublicanDateTime time = new(year, month, day, Era);

        // Act
        _ = time.AddYears(years);
    }

    #endregion


    #region ToGregorianDateTime

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanDateTime.ToGregorianDateTime()"/> method.
    /// </summary>
    /// <param name="repYear">An integer that represents the year in the Republican calendar.</param>
    /// <param name="repMonth">An integer that represents the month in the Republican calendar.</param>
    /// <param name="repDay">An integer that represents the day in the Republican calendar.</param>
    /// <param name="expectedGregYear">An integer that represents the expected year in the Republican calendar.</param>
    /// <param name="expectedGregMonth">An integer that represents the expected month in the Republican calendar.</param>
    /// <param name="expectedGregDay">An integer that represents the expected day in the Republican calendar.</param>
    [TestMethod]
    [DataRow(1, 1, 1, 1792, 9, 22)]
    [DataRow(8, 2, 18, 1799, 11, 9)]
    [DataRow(14, 4, 10, 1805, 12, 31)]
    public void ToGregorianDateTime_RepublicanDateTime_ReturnsGregorianDateTime(int repYear, int repMonth, int repDay, int expectedGregYear, int expectedGregMonth, int expectedGregDay)
    {
        // Arrange
        FrenchRepublicanDateTime repTime = new(repYear, repMonth, repDay, Hour, Minute, Second, Millisecond, Era);

        // Act
        DateTime actualGregTime = repTime.ToGregorianDateTime();

        // Assert
        Assert.AreEqual(expectedGregYear, actualGregTime.Year);
        Assert.AreEqual(expectedGregMonth, actualGregTime.Month);
        Assert.AreEqual(expectedGregDay, actualGregTime.Day);
        Assert.AreEqual(Hour, actualGregTime.Hour);
        Assert.AreEqual(Minute, actualGregTime.Minute);
        Assert.AreEqual(Second, actualGregTime.Second);
        Assert.AreEqual(Millisecond, actualGregTime.Millisecond);
    }

    #endregion

}
