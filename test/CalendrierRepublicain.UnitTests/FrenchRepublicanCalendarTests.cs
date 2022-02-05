using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using GregorianDateTime = System.DateTime;


namespace Sinistrius.CalendrierRepublicain.UnitTests;


/// <summary>
/// Tests the <see cref="FrenchRepublicanCalendar"/> class.
/// </summary>
[TestClass]
[ExcludeFromCodeCoverage]
public class FrenchRepublicanCalendarTests
{

    #region Property Tests

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.AlgorithmType"/> property.
    /// </summary>
    [TestMethod]
    public void AlgorithmType_ReturnsSolarCalendar()
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();

        // Act
        CalendarAlgorithmType type = calendar.AlgorithmType;

        // Assert
        Assert.AreEqual(CalendarAlgorithmType.SolarCalendar, type);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.Eras"/> property.
    /// </summary>
    [TestMethod]
    public void Eras_ReturnsOne()
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();

        // Act
        int[] eras = calendar.Eras;

        // Assert
        CollectionAssert.AreEqual(new[] { 1 }, eras);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.MaxSupportedDateTime"/> property.
    /// </summary>
    [TestMethod]
    public void MaxSupportedDateTime_ReturnsLastDateGregorian()
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();

        // Act
        GregorianDateTime date = calendar.MaxSupportedDateTime;

        // Assert
        Assert.AreEqual(1805, date.Year);
        Assert.AreEqual(12, date.Month);
        Assert.AreEqual(31, date.Day);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.MinSupportedDateTime"/> property.
    /// </summary>
    [TestMethod]
    public void MinSupportedDateTime_ReturnsFirstDateGregorian()
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();

        // Act
        GregorianDateTime date = calendar.MinSupportedDateTime;

        // Assert
        Assert.AreEqual(1792, date.Year);
        Assert.AreEqual(9, date.Month);
        Assert.AreEqual(22, date.Day);
    }

    #endregion


    #region Method Tests

    #region IsLeapYear

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.IsLeapYear(int, int)"/> method with leap years.
    /// </summary>
    /// <param name="year">An integer representing the year to be tested.</param>
    [TestMethod]
    [DataRow(3)]
    [DataRow(7)]
    [DataRow(11)]
    public void IsLeapYear_LeapYear_ReturnsTrue(int year)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();

        // Act
        bool isLeapYear = calendar.IsLeapYear(year);

        // Assert
        Assert.IsTrue(isLeapYear);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.IsLeapYear(int, int)"/> method with non-leap years.
    /// </summary>
    /// <param name="year">An integer representing the year to be tested.</param>
    [TestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(10)]
    public void IsLeapYear_NonLeapYear_ReturnsFalse(int year)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();

        // Act
        bool isLeapYear = calendar.IsLeapYear(year);

        // Assert
        Assert.IsFalse(isLeapYear);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.IsLeapYear(int, int)"/> method with a leap year, but invalid era.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void IsLeapYear_LeapYearInvalidEra_Throws()
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();

        // Act and assert
        _ = calendar.IsLeapYear(3, 0);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.IsLeapYear(int, int)"/> method with a non-leap year, but invalid era.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void IsLeapYear_NonLeapYearInvalidEra_Throws()
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();

        // Act and assert
        _ = calendar.IsLeapYear(4, 0);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.IsLeapYear(int, int)"/> method with an invalid year and valid era.
    /// </summary>
    /// <param name="year">An integer representing the year to be tested.</param>
    [TestMethod]
    [DataRow(-10)]
    [DataRow(0)]
    [DataRow(15)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void IsLeapYear_InvalidYearValidEra_Throws(int year)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();

        // Act and assert
        _ = calendar.IsLeapYear(year, 1);
    }

    #endregion


    #region AddMonths

    /// <summary>
    /// Tests the addition of years to a date.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    /// <param name="monthsToAdd">An integer that represents the months to be added.</param>
    /// <param name="expectedYear">An integer that represents the expected year in the Gregorian calendar after the addition.</param>
    /// <param name="expectedMonth">An integer that represents the expected month in the Gregorian calendar after the addition.</param>
    /// <param name="expectedDay">An integer that represents the expected day in the Gregorian calendar after the addition.</param>
    [TestMethod]
    [DataRow(1792, 9, 22, 1, 1792, 10, 22)]
    [DataRow(1793, 12, 26, -2, 1793, 10, 27)]
    [DataRow(1795, 9, 01, 4, 1796, 1, 5)]
    public void AddMonth_ValidDate_ReturnsModifiedDate(int year, int month, int day, int monthsToAdd, int expectedYear, int expectedMonth, int expectedDay)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();
        GregorianDateTime date = new(year, month, day);

        // Act
        GregorianDateTime actualDate = calendar.AddMonths(date, monthsToAdd);

        // Assert
        Assert.AreEqual(expectedYear, actualDate.Year);
        Assert.AreEqual(expectedMonth, actualDate.Month);
        Assert.AreEqual(expectedDay, actualDate.Day);
    }

    #endregion


    #region AddYears

    /// <summary>
    /// Tests the addition of years to a date.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    /// <param name="yearsToAdd">An integer that represents the years to be added.</param>
    /// <param name="expectedYear">An integer that represents the expected year in the Gregorian calendar after the addition.</param>
    /// <param name="expectedMonth">An integer that represents the expected month in the Gregorian calendar after the addition.</param>
    /// <param name="expectedDay">An integer that represents the expected day in the Gregorian calendar after the addition.</param>
    [TestMethod]
    [DataRow(1792, 9, 22, 1, 1793, 9, 22)]
    [DataRow(1799, 11, 9, -3, 1796, 11, 8)]
    [DataRow(1795, 9, 22, 2, 1797, 9, 22)]
    public void AddYear_ValidDate_ReturnsModifiedDate(int year, int month, int day, int yearsToAdd, int expectedYear, int expectedMonth, int expectedDay)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();
        GregorianDateTime date = new(year, month, day);

        // Act
        GregorianDateTime actualDate = calendar.AddYears(date, yearsToAdd);

        // Assert
        Assert.AreEqual(expectedYear, actualDate.Year);
        Assert.AreEqual(expectedMonth, actualDate.Month);
        Assert.AreEqual(expectedDay, actualDate.Day);
    }

    #endregion


    #region GetDayOfMonth

    /// <summary>
    /// Tests the retrieval of a day of month.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    /// <param name="expectedDay">An integer that represents the expected day of the month in the Republican calendar.</param>
    [TestMethod]
    [DataRow(1792, 9, 22, 1)]
    [DataRow(1799, 11, 9, 18)]
    [DataRow(1800, 9, 22, 5)]
    [DataRow(1805, 12, 31, 10)]
    public void GetDayOfMonth_ValidDate_ReturnsDay(int year, int month, int day, int expectedDay)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();
        GregorianDateTime date = new(year, month, day);

        // Act
        int actualDay = calendar.GetDayOfMonth(date);

        // Assert
        Assert.AreEqual(expectedDay, actualDay);
    }


    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void GetDayOfWeek_ValidDate_ThrowsInvalidOperationException()
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();
        GregorianDateTime date = Globals.MinSupportedDateTime;

        // Act
        _ = calendar.GetDayOfWeek(date);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.GetDayOfYear(GregorianDateTime)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    /// <param name="expectedDay">An integer that represents the expected day of the year in the Republican calendar.</param>
    [TestMethod]
    [DataRow(1792, 9, 22, 1)]
    [DataRow(1799, 11, 9, 48)]
    [DataRow(1805, 12, 31, 100)]
    public void GetDayOfYear_ValidGregorianDate_ReturnsRepublicanDayOfYear(int year, int month, int day, int expectedDay)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();
        GregorianDateTime date = new(year, month, day);

        // Act
        int actualDay = calendar.GetDayOfYear(date);

        // Assert
        Assert.AreEqual(expectedDay, actualDay);
    }

    #endregion

    #endregion

}
