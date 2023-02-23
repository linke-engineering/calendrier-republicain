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


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.TwoDigitYearMax"/> property.
    /// </summary>
    [TestMethod]
    public void TwoDigitYearMax_ReturnsNinetyNine()
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();

        // Act
        int year = calendar.TwoDigitYearMax;

        // Act and assert
        Assert.AreEqual(99, year);
    }

    #endregion


    #region Region Method Tests

    #region AddDays

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.AddDays(GregorianDateTime, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    /// <param name="daysToAdd">An integer that represents the days to be added.</param>
    /// <param name="expectedYear">An integer that represents the expected year in the Gregorian calendar after the addition.</param>
    /// <param name="expectedMonth">An integer that represents the expected month in the Gregorian calendar after the addition.</param>
    /// <param name="expectedDay">An integer that represents the expected day in the Gregorian calendar after the addition.</param>
    [TestMethod]
    [DataRow(1792, 9, 22, 1, 1792, 9, 23)]
    [DataRow(1793, 12, 26, -26, 1793, 11, 30)]
    [DataRow(1795, 9, 16, 10, 1795, 9, 26)]
    public void AddDays_ValidDate_ReturnsModifiedDate(int year, int month, int day, int daysToAdd, int expectedYear, int expectedMonth, int expectedDay)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();
        GregorianDateTime date = new(year, month, day);

        // Act
        GregorianDateTime actualDate = calendar.AddDays(date, daysToAdd);

        // Assert
        Assert.AreEqual(expectedYear, actualDate.Year);
        Assert.AreEqual(expectedMonth, actualDate.Month);
        Assert.AreEqual(expectedDay, actualDate.Day);
        Assert.AreEqual(date.Hour, actualDate.Hour);
        Assert.AreEqual(date.Minute, actualDate.Minute);
        Assert.AreEqual(date.Second, actualDate.Second);
        Assert.AreEqual(date.Millisecond, actualDate.Millisecond);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.AddDays(GregorianDateTime, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    /// <param name="daysToAdd">An integer that represents the days to be added.</param>
    [TestMethod]
    [DataRow(1792, 9, 22, -1)]
    [DataRow(1805, 1, 1, 365)]
    [ExpectedException(typeof(ArgumentException))]
    public void AddDays_AdditionExceedsValidity_ThrowsArgumentException(int year, int month, int day, int daysToAdd)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();
        GregorianDateTime date = new(year, month, day);

        // Act
        _ = calendar.AddDays(date, daysToAdd);
    }

    #endregion


    #region AddMonths

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.AddMonths(GregorianDateTime, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    /// <param name="daysToAdd">An integer that represents the months to be added.</param>
    /// <param name="expectedYear">An integer that represents the expected year in the Gregorian calendar after the addition.</param>
    /// <param name="expectedMonth">An integer that represents the expected month in the Gregorian calendar after the addition.</param>
    /// <param name="expectedDay">An integer that represents the expected day in the Gregorian calendar after the addition.</param>
    [TestMethod]
    [DataRow(1792, 9, 22, 1, 1792, 10, 22)]
    [DataRow(1793, 12, 26, -2, 1793, 10, 27)]
    [DataRow(1795, 9, 20, 20, 1797, 5, 20)]
    public void AddMonths_ValidDate_ReturnsModifiedDate(int year, int month, int day, int daysToAdd, int expectedYear, int expectedMonth, int expectedDay)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();
        GregorianDateTime date = new(year, month, day);

        // Act
        GregorianDateTime actualDate = calendar.AddMonths(date, daysToAdd);

        // Assert
        Assert.AreEqual(expectedYear, actualDate.Year);
        Assert.AreEqual(expectedMonth, actualDate.Month);
        Assert.AreEqual(expectedDay, actualDate.Day);
        Assert.AreEqual(date.Hour, actualDate.Hour);
        Assert.AreEqual(date.Minute, actualDate.Minute);
        Assert.AreEqual(date.Second, actualDate.Second);
        Assert.AreEqual(date.Millisecond, actualDate.Millisecond);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.AddMonths(GregorianDateTime, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    [TestMethod]
    [DataRow(1792, 9, 21)]
    [DataRow(1806, 1, 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AddMonths_DateOutOfRange_ThrowsArgumentOutOfRangeException(int year, int month, int day)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();
        GregorianDateTime date = new(year, month, day);

        // Act
        _ = calendar.AddMonths(date, 1);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.AddMonths(GregorianDateTime, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    /// <param name="monthsToAdd">An integer that represents the months to be added.</param>
    [TestMethod]
    [DataRow(1792, 9, 22, -1)]
    [DataRow(1793, 9, 22, -12)]
    [DataRow(1805, 1, 1, 12)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AddMonths_AdditionExceedsValidity_ThrowsArgumentOutOfRangeException(int year, int month, int day, int monthsToAdd)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();
        GregorianDateTime date = new(year, month, day);

        // Act
        _ = calendar.AddMonths(date, monthsToAdd);
    }

    #endregion


    #region AddWeeks

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.AddWeeks(GregorianDateTime, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    /// <param name="weeksToAdd">An integer that represents the weeks to be added.</param>
    /// <param name="expectedYear">An integer that represents the expected year in the Gregorian calendar after the addition.</param>
    /// <param name="expectedMonth">An integer that represents the expected month in the Gregorian calendar after the addition.</param>
    /// <param name="expectedDay">An integer that represents the expected day in the Gregorian calendar after the addition.</param>
    [TestMethod]
    [DataRow(1792, 9, 22, 1, 1792, 10, 2)]
    [DataRow(1793, 12, 26, -2, 1793, 12, 6)]
    [DataRow(1795, 9, 20, 20, 1796, 4, 10)]
    public void AddWeeks_ValidDate_ReturnsModifiedDate(int year, int month, int day, int weeksToAdd, int expectedYear, int expectedMonth, int expectedDay)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();
        GregorianDateTime date = new(year, month, day);

        // Act
        GregorianDateTime actualDate = calendar.AddWeeks(date, weeksToAdd);

        // Assert
        Assert.AreEqual(expectedYear, actualDate.Year);
        Assert.AreEqual(expectedMonth, actualDate.Month);
        Assert.AreEqual(expectedDay, actualDate.Day);
        Assert.AreEqual(date.Hour, actualDate.Hour);
        Assert.AreEqual(date.Minute, actualDate.Minute);
        Assert.AreEqual(date.Second, actualDate.Second);
        Assert.AreEqual(date.Millisecond, actualDate.Millisecond);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.AddWeeks(GregorianDateTime, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    [TestMethod]
    [DataRow(1792, 9, 21)]
    [DataRow(1806, 1, 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AddWeeks_DateOutOfRange_ThrowsArgumentOutOfRangeException(int year, int month, int day)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();
        GregorianDateTime date = new(year, month, day);

        // Act
        _ = calendar.AddWeeks(date, 1);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.AddWeeks(GregorianDateTime, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    /// <param name="weeksToAdd">An integer that represents the weeks to be added.</param>
    [TestMethod]
    [DataRow(1792, 9, 22, -1)]
    [DataRow(1792, 10, 2, -2)]
    [DataRow(1805, 12, 22, 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AddWeeks_AdditionExceedsValidity_ThrowsArgumentOutOfRangeException(int year, int month, int day, int weeksToAdd)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();
        GregorianDateTime date = new(year, month, day);

        // Act
        _ = calendar.AddWeeks(date, weeksToAdd);
    }

    #endregion


    #region AddYears

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.AddYears(GregorianDateTime, int)"/> method.
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
        Assert.AreEqual(date.Hour, actualDate.Hour);
        Assert.AreEqual(date.Minute, actualDate.Minute);
        Assert.AreEqual(date.Second, actualDate.Second);
        Assert.AreEqual(date.Millisecond, actualDate.Millisecond);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.AddYears(GregorianDateTime, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    [TestMethod]
    [DataRow(1792, 9, 21)]
    [DataRow(1806, 1, 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AddYears_DateOutOfRange_ThrowsArgumentOutOfRangeException(int year, int month, int day)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();
        GregorianDateTime date = new(year, month, day);

        // Act
        _ = calendar.AddYears(date, 1);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.AddYears(GregorianDateTime, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    /// <param name="yearsToAdd">An integer that represents the years to be added.</param>
    [TestMethod]
    [DataRow(1792, 9, 22, -1)]
    [DataRow(1793, 9, 21, -1)]
    [DataRow(1805, 1, 1, 1)]
    [DataRow(1805, 12, 31, 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AddYears_AdditionExceedsValidity_ThrowsArgumentOutOfRangeException(int year, int month, int day, int yearsToAdd)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();
        GregorianDateTime date = new(year, month, day);

        // Act
        _ = calendar.AddYears(date, yearsToAdd);
    }

    #endregion


    #region GetDayOfMonth

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.GetDayOfMonth(GregorianDateTime)"/> method.
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


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.GetDayOfMonth(GregorianDateTime)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    [TestMethod]
    [DataRow(1792, 9, 21)]
    [DataRow(1806, 1, 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void GetDayOfMonth_DateOutOfRange_ThrowsArgumentOutOfRangeException(int year, int month, int day)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();
        GregorianDateTime date = new(year, month, day);

        // Act
        _ = calendar.GetDayOfMonth(date);
    }

    #endregion


    #region GetDayOfWeek

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.GetDayOfWeek(GregorianDateTime)"/> method.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void GetDayOfWeek_ValidDate_ThrowsInvalidOperationException()
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();
        GregorianDateTime date = Constants.MinSupportedDateTime;

        // Act
        _ = calendar.GetDayOfWeek(date);
    }

    #endregion


    #region GetDayOfYear

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.GetDayOfYear(GregorianDateTime)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    /// <param name="expectedDays">An integer that represents the expected day of the year in the Republican calendar.</param>
    [TestMethod]
    [DataRow(1792, 9, 22, 1)]
    [DataRow(1799, 11, 9, 48)]
    [DataRow(1805, 12, 31, 100)]
    public void GetDayOfYear_ValidDate_ReturnsDay(int year, int month, int day, int expectedDays)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();
        GregorianDateTime date = new(year, month, day);

        // Act
        int actualDay = calendar.GetDayOfYear(date);

        // Assert
        Assert.AreEqual(expectedDays, actualDay);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.GetDayOfYear(GregorianDateTime)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    [TestMethod]
    [DataRow(1792, 9, 21)]
    [DataRow(1806, 1, 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void GetDayOfYear_DateOutOfRange_ThrowsArgumentOutOfRangeException(int year, int month, int day)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();
        GregorianDateTime date = new(year, month, day);

        // Act
        _ = calendar.GetDayOfYear(date);
    }

    #endregion


    #region GetDaysInMonth

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.GetDaysInMonth(int, int, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Republican calendar.</param>
    /// <param name="month">An integer that represents the month in the Republican calendar.</param>
    /// <param name="expectedDays">An integer that represents the expected days in the specified month.</param>
    [TestMethod]
    [DataRow(1, 1, 30)]
    [DataRow(8, 3, 30)]
    [DataRow(14, 4, 10)]
    public void GetDaysInMonth_ValidRepublicanMonth_ReturnsDayCount(int year, int month, int expectedDays)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();

        // Act
        int actualDays = calendar.GetDaysInMonth(year, month, 1);

        // Assert
        Assert.AreEqual(expectedDays, actualDays);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.GetDaysInMonth(int, int, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Republican calendar.</param>
    /// <param name="month">An integer that represents the month in the Republican calendar.</param>
    /// <param name="era">An integer that represents the era in the Republican calendar.</param>
    [TestMethod]
    [DataRow(1, 1, 0)]
    [DataRow(3, 14, 1)]
    [DataRow(14, 5, 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void GetDaysInMonth_DateOutOfRange_ThrowsArgumentOutOfRangeException(int year, int month, int era)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();

        // Act
        _ = calendar.GetDaysInMonth(year, month, era);
    }

    #endregion


    #region GetDaysInYear

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.GetDaysInYear(int, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Republican calendar.</param>
    /// <param name="expectedDays">An integer that represents the expected number of days in the specified year in the Republican calendar.</param>
    [TestMethod]
    [DataRow(2, 365)]
    [DataRow(7, 366)]
    [DataRow(14, 100)]
    public void GetDaysInYear_ValidRepublicanMonth_ReturnsDayCount(int year, int expectedDays)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();

        // Act
        int actualDays = calendar.GetDaysInYear(year, 1);

        // Assert
        Assert.AreEqual(expectedDays, actualDays);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.GetDaysInYear(int, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Republican calendar.</param>
    /// <param name="era">An integer that represents the era in the Republican calendar.</param>
    [TestMethod]
    [DataRow(0, 1)]
    [DataRow(1, 0)]
    [DataRow(15, 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void GetDaysInYear_YearOutOfRange_ThrowsArgumentOutOfRangeException(int year, int era)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();

        // Act
        _ = calendar.GetDaysInYear(year, era);
    }

    #endregion


    #region IsLeapYear

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.IsLeapYear(int, int)"/> method with leap years.
    /// </summary>
    /// <param name="year">An integer representing the year in the Republican calendar.</param>
    [TestMethod]
    [DataRow(3)]
    [DataRow(7)]
    [DataRow(11)]
    public void IsLeapYear_LeapYear_ReturnsTrue(int year)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();

        // Act
        bool isLeapYear = calendar.IsLeapYear(year, 1);

        // Assert
        Assert.IsTrue(isLeapYear);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.IsLeapYear(int, int)"/> method with non-leap years.
    /// </summary>
    /// <param name="year">An integer representing the year in the Republican calendar.</param>
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
    /// <param name="year">An integer representing the year in the Republican calendar.</param>
    /// <param name="year">An integer representing the era in the Republican calendar.</param>
    [TestMethod]
    [DataRow(0, 1)]
    [DataRow(1, 0)]
    [DataRow(15, 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void IsLeapYear_InvalidYear_Throws(int year, int era)
    {
        // Arrange
        FrenchRepublicanCalendar calendar = new();

        // Act and assert
        _ = calendar.IsLeapYear(year, era);
    }

    #endregion

    #endregion

}
