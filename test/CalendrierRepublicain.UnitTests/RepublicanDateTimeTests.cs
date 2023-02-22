using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;


namespace Sinistrius.CalendrierRepublicain.UnitTests;


/// <summary>
/// Tests the <see cref="RepublicanDateTime"/> class.
/// </summary>
[TestClass]
[ExcludeFromCodeCoverage]
public class RepublicanDateTimeTests
{

    #region DateTime Parameter Constructor

    /// <summary>
    /// Tests the <see cref="RepublicanDateTime(DateTime)"/> constructor with valid input.
    /// </summary>
    /// <param name="gregYear">An integer that represents the year in the Gregorian calendar.</param>
    /// <param name="gregMonth">An integer that represents the month in the Gregorian calendar.</param>
    /// <param name="gregDay">An integer that represents the day in the Gregorian calendar.</param>
    /// <param name="repYear">An integer that represents the expected year in the Republican calendar.</param>
    /// <param name="repMonth">An integer that represents the expected month in the Republican calendar.</param>
    /// <param name="repDay">An integer that represents the expected day in the Republican calendar.</param>
    [TestMethod]
    [DataRow(1792, 9, 22, 1, 1, 1)]
    [DataRow(1799, 11, 9, 8, 2, 18)]
    [DataRow(1805, 12, 31, 14, 4, 10)]
    public void Initialize_ValidGregorianTime_CreatesRepublicanTime(int gregYear, int gregMonth, int gregDay, int repYear, int repMonth, int repDay)
    {
        // Arrange
        Random random = new();
        int hour = random.Next(24);
        int minute = random.Next(60);
        int second = random.Next(60);
        int millisecond = random.Next(1000);
        DateTime gregTime = new(gregYear, gregMonth, gregDay, hour, minute, second, millisecond);
        TimeSpan expectedTimeOfDay = gregTime.TimeOfDay;

        // Act
        RepublicanDateTime repTime = new(gregTime);

        // Assert
        Assert.AreEqual(repYear, repTime.Year);
        Assert.AreEqual(repMonth, repTime.Month);
        Assert.AreEqual(repDay, repTime.Day);
        Assert.AreEqual(expectedTimeOfDay, repTime.TimeOfDay);
    }

    #endregion


    #region Integer Parameter Constructor

    /// <summary>
    /// Tests the <see cref="RepublicanDateTime(int, int, int, int, int, int, int)"/> constructor with valid input.
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
        // Arrange
        Random random = new();
        int hour = random.Next(24);
        int minute = random.Next(60);
        int second = random.Next(60);
        int millisecond = random.Next(1000);
        RepublicanDateTime repTime = new(repYear, repMonth, repDay, hour, minute, second, millisecond);
        TimeSpan expectedTimeOfDay = repTime.TimeOfDay;

        // Assert
        Assert.AreEqual(repYear, repTime.Year);
        Assert.AreEqual(repMonth, repTime.Month);
        Assert.AreEqual(repDay, repTime.Day);
        Assert.AreEqual(expectedTimeOfDay, repTime.TimeOfDay);
    }


    /// <summary>
    /// Tests the <see cref="RepublicanDateTime(int, int, int, int, int, int, int)"/> constructor with an invalid year parameter.
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
        _ = new RepublicanDateTime(repYear, 1, 1, 0, 0, 0, 0);
    }


    /// <summary>
    /// Tests the <see cref="RepublicanDateTime(int, int, int, int, int, int, int)"/> constructor with an invalid month parameter.
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
        _ = new RepublicanDateTime(repYear, repMonth, 1, 0, 0, 0, 0);
    }


    /// <summary>
    /// Tests the <see cref="RepublicanDateTime(int, int, int, int, int, int, int)"/> constructor with an invalid day parameter.
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
        _ = new RepublicanDateTime(repYear, repMonth, repDay, 0, 0, 0, 0);
    }


    /// <summary>
    /// Tests the <see cref="RepublicanDateTime(int, int, int, int, int, int, int)"/> constructor with an invalid hour parameter.
    /// </summary>
    /// <param name="hour">An integer that represents the hour in the Republican calendar.</param>
    [TestMethod]
    [DataRow(-1)]
    [DataRow(24)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Initialize_InvalidHour_ThrowsArgumentOutOfRangeException(int hour)
    {
        // Act
        _ = new RepublicanDateTime(1, 1, 1, hour, 0, 0, 0);
    }


    /// <summary>
    /// Tests the <see cref="RepublicanDateTime(int, int, int, int, int, int, int)"/> constructor with an invalid minute parameter.
    /// </summary>
    /// <param name="minute">An integer that represents the minute in the Republican calendar.</param>
    [TestMethod]
    [DataRow(-1)]
    [DataRow(60)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Initialize_InvalidMinute_ThrowsArgumentOutOfRangeException(int minute)
    {
        // Act
        _ = new RepublicanDateTime(1, 1, 1, 0, minute, 0, 0);
    }


    /// <summary>
    /// Tests the <see cref="RepublicanDateTime(int, int, int, int, int, int, int)"/> constructor with an invalid second parameter.
    /// </summary>
    /// <param name="second">An integer that represents the second in the Republican calendar.</param>
    [TestMethod]
    [DataRow(-1)]
    [DataRow(60)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Initialize_InvalidSecond_ThrowsArgumentOutOfRangeException(int second)
    {
        // Act
        _ = new RepublicanDateTime(1, 1, 1, 0, 0, second, 0);
    }


    /// <summary>
    /// Tests the <see cref="RepublicanDateTime(int, int, int, int, int, int, int)"/> constructor with an invalid millisecond parameter.
    /// </summary>
    /// <param name="millisecond">An integer that represents the second in the Republican calendar.</param>
    [TestMethod]
    [DataRow(-1)]
    [DataRow(1000)]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Initialize_InvalidMillisecond_ThrowsArgumentOutOfRangeException(int millisecond)
    {
        // Act
        _ = new RepublicanDateTime(1, 1, 1, 0, 0, 0, millisecond);
    }

    #endregion

}
