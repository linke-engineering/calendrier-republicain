namespace Sinistrius.CalendrierRepublicain.UnitTests;


/// <summary>
/// Tests the <see cref="JulianDayNumber"/> class.
/// </summary>
[TestClass]
public class JulianDayNumberTests
{

    #region Constructor

    /// <summary>
    /// Tests the <see cref="JulianDayNumber(DateTime)"/> constructor.
    /// </summary>
    /// <param name="year">An integer that represents theyear in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    /// <param name="hour">An integer that represents the hour.</param>
    /// <param name="expectedJdn">The expected Julian day number.</param>
    [TestMethod]
    [DataRow(1792, 9, 22, 0, 2375839)]
    [DataRow(1799, 11, 9, 12, 2378443)]
    [DataRow(1805, 12, 31, 13, 2380686)]
    public void Constructor_ValidGregorianTime_Constructs(int year, int month, int day, int hour, int expectedJdn)
    {
        // Arrange
        DateTime time = new(year, month, day, hour, 0, 0);

        // Act
        JulianDayNumber jdn = new(time);
        int actualJdn = jdn.Value;

        // Assert
        Assert.AreEqual(expectedJdn, actualJdn);
    }


    /// <summary>
    /// Tests the <see cref="JulianDayNumber(DateTime)"/> constructor.
    /// </summary>
    /// <param name="year">An integer that represents theyear in the Republican calendar.</param>
    /// <param name="month">An integer that represents the month in the Republican calendar.</param>
    /// <param name="day">An integer that represents the day in the Republican calendar.</param>
    /// <param name="expectedJdn">The expected Julian day number.</param>
    [TestMethod]
    [DataRow(1, 1, 1, 2375839)]
    [DataRow(8, 2, 18, 2378443)]
    [DataRow(14, 4, 10, 2380686)]
    public void Constructor_ValidRepublicanTime_Constructs(int year, int month, int day, int expectedJdn)
    {
        // Act
        JulianDayNumber jdn = new(year, month, day);
        int actualJdn = jdn.Value;

        // Assert
        Assert.AreEqual(expectedJdn, actualJdn);
    }

    #endregion


    #region ToGregorian

    /// <summary>
    /// Tests the <see cref="JulianDayNumber.ToGregorianTime()"/> method.
    /// </summary>
    /// <param name="year">An integer that represents theyear in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    [TestMethod]
    [DataRow(1792, 9, 22)]
    [DataRow(1799, 11, 9)]
    [DataRow(1805, 12, 31)]
    public void ToGregorian_GregorianTime_ReturnsSame(int year, int month, int day)
    {
        // Arrange
        DateTime time = new(year, month, day);
        JulianDayNumber jdn = new(time);

        // Act
        DateTime actualTime = jdn.ToGregorianTime();
        int actualYear = actualTime.Year;
        int actualMonth = actualTime.Month;
        int actualDay = actualTime.Day;

        // Assert
        Assert.AreEqual(year, actualYear);
        Assert.AreEqual(month, actualMonth);
        Assert.AreEqual(day, actualDay);
    }

    #endregion

}
