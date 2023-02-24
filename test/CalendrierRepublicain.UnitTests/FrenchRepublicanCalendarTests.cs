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

    #region Local Fields

    /// <summary>
    /// A Republican calendar object.
    /// </summary>
    /// <remarks>
    /// This object will be initialized per each test method.
    /// </remarks>
    private readonly FrenchRepublicanCalendar _calendar = new();

    #endregion


    #region Property Tests

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.AlgorithmType"/> property.
    /// </summary>
    [TestMethod]
    public void AlgorithmType_ReturnsSolarCalendar()
    {
        // Act
        CalendarAlgorithmType type = _calendar.AlgorithmType;

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
        // Act
        GregorianDateTime date = _calendar.MaxSupportedDateTime;

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
        // Act
        GregorianDateTime date = _calendar.MinSupportedDateTime;

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
        // Act
        int year = _calendar.TwoDigitYearMax;

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
        GregorianDateTime date = new(year, month, day);

        // Act
        GregorianDateTime actualDate = _calendar.AddDays(date, daysToAdd);

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
        GregorianDateTime date = new(year, month, day);

        // Act
        _ = _calendar.AddDays(date, daysToAdd);
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
        GregorianDateTime date = new(year, month, day);

        // Act
        GregorianDateTime actualDate = _calendar.AddMonths(date, daysToAdd);

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
        GregorianDateTime date = new(year, month, day);

        // Act
        _ = _calendar.AddMonths(date, 1);
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
        GregorianDateTime date = new(year, month, day);

        // Act
        _ = _calendar.AddMonths(date, monthsToAdd);
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
        GregorianDateTime date = new(year, month, day);

        // Act
        GregorianDateTime actualDate = _calendar.AddWeeks(date, weeksToAdd);

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
        GregorianDateTime date = new(year, month, day);

        // Act
        _ = _calendar.AddWeeks(date, 1);
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
        GregorianDateTime date = new(year, month, day);

        // Act
        _ = _calendar.AddWeeks(date, weeksToAdd);
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
        GregorianDateTime date = new(year, month, day);

        // Act
        GregorianDateTime actualDate = _calendar.AddYears(date, yearsToAdd);

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
        GregorianDateTime date = new(year, month, day);

        // Act
        _ = _calendar.AddYears(date, 1);
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
        GregorianDateTime date = new(year, month, day);

        // Act
        _ = _calendar.AddYears(date, yearsToAdd);
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
        GregorianDateTime date = new(year, month, day);

        // Act
        int actualDay = _calendar.GetDayOfMonth(date);

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
        GregorianDateTime date = new(year, month, day);

        // Act
        _ = _calendar.GetDayOfMonth(date);
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
        GregorianDateTime date = Constants.MinSupportedDateTime;

        // Act
        _ = _calendar.GetDayOfWeek(date);
    }

    #endregion


    #region GetDayOfYear

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
    public void GetDayOfYear_ValidDate_ReturnsDay(int year, int month, int day, int expectedDay)
    {
        // Arrange
        GregorianDateTime date = new(year, month, day);

        // Act
        int actualDay = _calendar.GetDayOfYear(date);

        // Assert
        Assert.AreEqual(expectedDay, actualDay);
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
        GregorianDateTime date = new(year, month, day);

        // Act
        _ = _calendar.GetDayOfYear(date);
    }

    #endregion


    #region GetDaysInMonth

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.GetDaysInMonth(int, int, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Republican calendar.</param>
    /// <param name="month">An integer that represents the month in the Republican calendar.</param>
    /// <param name="expectedDays">An integer that represents the expected number of days in the specified month.</param>
    [TestMethod]
    [DataRow(1, 1, 30)]
    [DataRow(8, 3, 30)]
    [DataRow(14, 4, 10)]
    public void GetDaysInMonth_ValidRepublicanMonth_ReturnsDayCount(int year, int month, int expectedDays)
    {
        // Act
        int actualDays = _calendar.GetDaysInMonth(year, month, 1);

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
        // Act
        _ = _calendar.GetDaysInMonth(year, month, era);
    }

    #endregion


    #region GetDaysInYear

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.GetDaysInYear(int, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Republican calendar.</param>
    /// <param name="expectedDays">An integer that represents the expected number of days in the specified year.</param>
    [TestMethod]
    [DataRow(2, 365)]
    [DataRow(7, 366)]
    [DataRow(14, 100)]
    public void GetDaysInYear_ValidRepublicanMonth_ReturnsDayCount(int year, int expectedDays)
    {
        // Act
        int actualDays = _calendar.GetDaysInYear(year, 1);

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
        // Act
        _ = _calendar.GetDaysInYear(year, era);
    }

    #endregion


    #region GetEra

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.GetEra(GregorianDateTime)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    [TestMethod]
    [DataRow(1792, 9, 22)]
    [DataRow(1799, 11, 9)]
    [DataRow(1800, 9, 22)]
    [DataRow(1805, 12, 3)]
    public void GetEra_ValidDate_ReturnsOne(int year, int month, int day)
    {
        // Arrange
        GregorianDateTime date = new(year, month, day);

        // Act
        int actualEra = _calendar.GetEra(date);

        // Assert
        Assert.AreEqual(1, actualEra);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.GetEra(GregorianDateTime)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    [TestMethod]
    [DataRow(1792, 9, 21)]
    [DataRow(1806, 1, 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void GetEra_DateOutOfRange_ThrowsArgumentOutOfRangeException(int year, int month, int day)
    {
        // Arrange
        GregorianDateTime date = new(year, month, day);

        // Act
        _ = _calendar.GetEra(date);
    }

    #endregion


    #region GetMonth

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.GetMonth(GregorianDateTime)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="era">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    /// <param name="expectedMonth">An integer that represents the expected month in the Republican calendar.</param>
    [TestMethod]
    [DataRow(1792, 9, 22, 1)]
    [DataRow(1799, 11, 9, 2)]
    [DataRow(1800, 9, 22, 13)]
    [DataRow(1805, 12, 31, 4)]
    public void GetMonth_ValidDate_ReturnsMonth(int year, int era, int day, int expectedMonth)
    {
        // Arrange
        GregorianDateTime date = new(year, era, day);

        // Act
        int actualMonth = _calendar.GetMonth(date);

        // Assert
        Assert.AreEqual(expectedMonth, actualMonth);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.GetMonth(GregorianDateTime)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    [TestMethod]
    [DataRow(1792, 9, 21)]
    [DataRow(1806, 1, 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void GetMonth_DateOutOfRange_ThrowsArgumentOutOfRangeException(int year, int month, int day)
    {
        // Arrange
        GregorianDateTime date = new(year, month, day);

        // Act
        _ = _calendar.GetMonth(date);
    }

    #endregion


    #region GetMonthsInYear

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.GetMonthsInYear(int, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Republican calendar.</param>
    /// <param name="expectedMonths">An integer that represents the expected number of months in the specified year.</param>
    [TestMethod]
    [DataRow(1, 13)]
    [DataRow(3, 13)]
    [DataRow(14, 4)]
    public void GetMonthsInYear_ValidDate_ReturnsMonths(int year, int expectedMonths)
    {
        // Act
        int actualMonths = _calendar.GetMonthsInYear(year, 1);

        // Assert
        Assert.AreEqual(expectedMonths, actualMonths);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.GetMonthsInYear(int, int)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Republican calendar.</param>
    /// <param name="era">An integer that represents the era in the Republican calendar.</param>
    [TestMethod]
    [DataRow(0, 1)]
    [DataRow(1, 0)]
    [DataRow(15, 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void GetMonthsInYear_YearOutOfRange_ThrowsArgumentOutOfRangeException(int year, int era)
    {
        // Act
        _ = _calendar.GetMonthsInYear(year, era);
    }

    #endregion


    #region GetWeekOfYear

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.GetWeekOfYear(GregorianDateTime, CalendarWeekRule, DayOfWeek)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    /// <param name="expectedWeek">An integer that represents the expected week in the Republican calendar.</param>
    [TestMethod]
    [DataRow(1792, 9, 22, 1)]
    [DataRow(1799, 11, 9, 5)]
    [DataRow(1800, 9, 22, 37)]
    [DataRow(1805, 12, 31, 10)]
    public void GetWeekOfYear(int year, int month, int day, int expectedWeek)
    {
        // Arrange
        GregorianDateTime date = new(year, month, day);

        // Act
        int actualWeek = _calendar.GetWeekOfYear(date, 0, 0);

        // Assert
        Assert.AreEqual(expectedWeek, actualWeek);
    }

    #endregion


    #region GetYear

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.GetYear(GregorianDateTime)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    /// <param name="expectedYear">An integer that represents the expected year in the Republican calendar.</param>
    [TestMethod]
    [DataRow(1792, 9, 22, 1)]
    [DataRow(1799, 11, 9, 8)]
    [DataRow(1800, 9, 22, 8)]
    [DataRow(1805, 12, 31, 14)]
    public void GetYear_ValidDate_ReturnsMonth(int year, int month, int day, int expectedYear)
    {
        // Arrange
        GregorianDateTime date = new(year, month, day);

        // Act
        int actualYear = _calendar.GetYear(date);

        // Assert
        Assert.AreEqual(expectedYear, actualYear);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.GetYear(GregorianDateTime)"/> method.
    /// </summary>
    /// <param name="year">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="month">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="day">An integer that represents the day in the Gregorian calendar.</param>
    [TestMethod]
    [DataRow(1792, 9, 21)]
    [DataRow(1806, 1, 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void GetYear_DateOutOfRange_ThrowsArgumentOutOfRangeException(int year, int month, int day)
    {
        // Arrange
        GregorianDateTime date = new(year, month, day);

        // Act
        _ = _calendar.GetYear(date);
    }

    #endregion


    #region IsLeapDay

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.IsLeapDay(int, int, int, int)"/> method.
    /// </summary>
    /// <param name="year">An integer representing the year in the Republican calendar.</param>
    [TestMethod]
    [DataRow(3)]
    [DataRow(7)]
    [DataRow(11)]
    public void IsLeapYear_LeapDay_ReturnsTrue(int year)
    {
        // Act
        bool isLeapDay = _calendar.IsLeapDay(year, 13, 6, 1);

        // Assert
        Assert.IsTrue(isLeapDay);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.IsLeapDay(int, int, int, int)"/> method.
    /// </summary>
    /// <param name="year">An integer representing the year in the Republican calendar.</param>
    /// <param name="month">An integer representing the month in the Republican calendar.</param>
    /// <param name="day">An integer representing the day in the Republican calendar.</param>
    [TestMethod]
    [DataRow(1, 1, 1)]
    [DataRow(2, 13, 5)]
    [DataRow(3, 13, 5)]
    public void IsLeapDay_NonLeapDay_ReturnsFalse(int year, int month, int day)
    {
        // Act
        bool isLeapDay = _calendar.IsLeapDay(year, month, day);

        // Assert
        Assert.IsFalse(isLeapDay);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.IsLeapYear(int, int)"/> method.
    /// </summary>
    /// <param name="year">An integer representing the year in the Republican calendar.</param>
    /// <param name="month">An integer representing the month in the Republican calendar.</param>
    /// <param name="day">An integer representing the day in the Republican calendar.</param>
    /// <param name="era">An integer representing the era in the Republican calendar.</param>
    [TestMethod]
    [DataRow(1, 1, 1, 0)]
    [DataRow(1, 1, 0, 1)]
    [DataRow(15, 1, 1, 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void IsLeapDay_InvalidDate_ThrowsArgumentOutOfRangeException(int year, int month, int day, int era)
    {
        // Act and assert
        _ = _calendar.IsLeapDay(year, month, day, era);
    }

    #endregion


    #region IsLeapMonth

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.IsLeapMonth(int, int, int)"/> method.
    /// </summary>
    /// <param name="year">An integer representing the year in the Republican calendar.</param>
    [TestMethod]
    [DataRow(3)]
    [DataRow(7)]
    [DataRow(11)]
    public void IsLeapMonth_LeapMonth_ReturnsTrue(int year)
    {
        // Act
        bool isLeapMonth = _calendar.IsLeapMonth(year, 13, 1);

        // Assert
        Assert.IsTrue(isLeapMonth);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.IsLeapMonth(int, int, int)"/> method.
    /// </summary>
    /// <param name="year">An integer representing the year in the Republican calendar.</param>
    /// <param name="month">An integer representing the month in the Republican calendar.</param>
    [TestMethod]
    [DataRow(1, 1)]
    [DataRow(2, 13)]
    [DataRow(3, 12)]
    public void IsLeapMonth_NonLeapMonth_ReturnsFalse(int year, int month)
    {
        // Act
        bool isLeapMonth = _calendar.IsLeapMonth(year, month, 1);

        // Assert
        Assert.IsFalse(isLeapMonth);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.IsLeapMonth(int, int, int)"/> method.
    /// </summary>
    /// <param name="year">An integer representing the year in the Republican calendar.</param>
    /// <param name="month">An integer representing the month in the Republican calendar.</param>
    /// <param name="era">An integer representing the era in the Republican calendar.</param>
    [TestMethod]
    [DataRow(1, 1, 0)]
    [DataRow(1, 0, 1)]
    [DataRow(15, 1, 1)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void IsLeapMonth_InvalidDate_ThrowsArgumentOutOfRangeException(int year, int month, int era)
    {
        // Act and assert
        _ = _calendar.IsLeapMonth(year, month, era);
    }

    #endregion


    #region IsLeapYear

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.IsLeapYear(int, int)"/> method.
    /// </summary>
    /// <param name="year">An integer representing the year in the Republican calendar.</param>
    [TestMethod]
    [DataRow(3)]
    [DataRow(7)]
    [DataRow(11)]
    public void IsLeapYear_LeapYear_ReturnsTrue(int year)
    {
        // Act
        bool isLeapYear = _calendar.IsLeapYear(year, 1);

        // Assert
        Assert.IsTrue(isLeapYear);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.IsLeapYear(int, int)"/> method.
    /// </summary>
    /// <param name="year">An integer representing the year in the Republican calendar.</param>
    [TestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(10)]
    public void IsLeapYear_NonLeapYear_ReturnsFalse(int year)
    {
        // Act
        bool isLeapYear = _calendar.IsLeapYear(year);

        // Assert
        Assert.IsFalse(isLeapYear);
    }


    /// <summary>
    /// Tests the <see cref="FrenchRepublicanCalendar.IsLeapYear(int, int)"/> method.
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
        // Act and assert
        _ = _calendar.IsLeapYear(year, era);
    }

    #endregion

    #endregion

}
