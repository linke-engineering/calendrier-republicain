using System.Globalization;

namespace LinkeEngineering.CalendrierRepublicain.UnitTests;


/// <summary>
/// Tests the <see cref="String"/> class.
/// </summary>
/// <remarks>
/// Save this file in "Unicode (UTF-8 with signature)" encoding to avoid issues with accented characters in the CI 
/// workflow.
/// </remarks>
[TestClass]
public class StringTests
{

    /// <summary>
    /// Tests that formatting a French Republican date using an empty format string returns an empty string.
    /// </summary>
    [TestMethod]
    public void Format_NullOrEmptyFormatString_ReturnsEmptyString()
    {
        // Arrange
        FrenchRepublicanDateTimeFormatter provider = new();
        DateTime time = new(1, 1, 1, new FrenchRepublicanCalendar());
        string format = String.Empty;

        // Act
        string actualString = String.Format(provider, format, time);

        // Assert
        Assert.AreEqual(String.Empty, actualString);
    }


    [TestMethod]
    [DataRow(1, 1, 1, "D", "fr-FR", "Primidi 1 Vendémiaire I")]
    [DataRow(1, 1, 1, "D", "de-DE", "Primidi, 1. Vendémiaire I")]
    [DataRow(1, 1, 1, "D", "en-US", "Primidi, Vendémiaire 1, I")]
    [DataRow(3, 13, 6, "D", "fr-FR", "Jour de la révolution III")]
    [DataRow(3, 13, 6, "D", "de-DE", "Jour de la révolution III")]
    [DataRow(3, 13, 6, "D", "en-US", "Jour de la révolution, III")]
    [DataRow(14, 4, 10, "D", "fr-FR", "Décadi 10 Nivôse XIV")]
    [DataRow(14, 4, 10, "D", "de-DE", "Décadi, 10. Nivôse XIV")]
    [DataRow(14, 4, 10, "D", "en-US", "Décadi, Nivôse 10, XIV")]
    public void Format_LongDateStandardFormatString_ReturnsLongDate(int year, int month, int day, string format, string culture, string expectedString)
    {
        // Arrange
        FrenchRepublicanDateTimeFormatter provider = new();
        DateTime time = new(year, month, day, new FrenchRepublicanCalendar());
        format = $"{{0:{format}}}";
        CultureInfo.CurrentCulture = new CultureInfo(culture);

        // Act
        Console.WriteLine(CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator);
        string actualString = String.Format(provider, format, time);

        // Assert
        Assert.AreEqual(expectedString, actualString);
    }


    [TestMethod]
    [DataRow(1, 1, 1, "d", "fr-FR", "01/01/01")]
    [DataRow(1, 1, 1, "d", "de-DE", "01.01.01")]
    [DataRow(1, 1, 1, "d", "en-US", "1/1/01")]
    [DataRow(3, 13, 6, "d", "fr-FR", "06/13/03")]
    [DataRow(3, 13, 6, "d", "de-DE", "06.13.03")]
    [DataRow(3, 13, 6, "d", "en-US", "13/6/03")]
    [DataRow(14, 4, 10, "d", "fr-FR", "10/04/14")]
    [DataRow(14, 4, 10, "d", "de-DE", "10.04.14")]
    [DataRow(14, 4, 10, "d", "en-US", "4/10/14")]
    public void Format_ShortDateStandardFormatString_ReturnsShortDate(int year, int month, int day, string format, string culture, string expectedString)
    {
        // Arrange
        FrenchRepublicanDateTimeFormatter provider = new();
        DateTime time = new(year, month, day, new FrenchRepublicanCalendar());
        format = $"{{0:{format}}}";
        CultureInfo.CurrentCulture = new CultureInfo(culture);

        // Act
        string actualString = String.Format(provider, format, time);

        // Assert
        Assert.AreEqual(expectedString, actualString);
    }


    /// <summary>
    /// Tests that formatting a French Republican year using a custom year formatter returns the expected Roman numeral
    /// representation.
    /// </summary>
    /// <param name="year">The French Republican year to format.</param>
    /// <param name="format">The format string specifying the year format pattern.</param>
    /// <param name="expectedString">The expected Roman numeral string result.</param>
    [TestMethod]
    [DataRow(1, "yyyyy", "I")]
    [DataRow(1, "yyyy", "I")]
    [DataRow(1, "yyy", "I")]
    [DataRow(2, "yyyyy", "II")]
    [DataRow(2, "yyyy", "II")]
    [DataRow(2, "yyy", "II")]
    [DataRow(14, "yyyyy", "XIV")]
    [DataRow(14, "yyyy", "XIV")]
    [DataRow(14, "yyy", "XIV")]
    public void Format_LongYearFormatter_ReturnsRomanNumeral(int year, string format, string expectedString)
    {
        // Arrange
        FrenchRepublicanDateTimeFormatter provider = new();
        DateTime time = new(year, 1, 1, new FrenchRepublicanCalendar());
        format = $"{{0:{format}}}";

        // Act
        string actualString = String.Format(provider, format, time);

        // Assert
        Assert.AreEqual(expectedString, actualString);
    }


    /// <summary>
    /// Tests that formatting a French Republican year using a custom short year formatter returns the expected 
    /// numeric representation.
    /// </summary>
    /// <param name="year">The French Republican year to format.</param>
    /// <param name="format">The format string specifying the year format pattern.</param>
    /// <param name="expectedString">The expected Roman numeral string result.</param>
    [TestMethod]
    [DataRow(1, "yyyyy", "I")]
    [DataRow(1, "yyyy", "I")]
    [DataRow(1, "yyy", "I")]
    [DataRow(1, "yy", "01")]
    [DataRow(1, "%y", "1")]
    [DataRow(2, "yyyyy", "II")]
    [DataRow(2, "yyyy", "II")]
    [DataRow(2, "yyy", "II")]
    [DataRow(2, "yy", "02")]
    [DataRow(2, "%y", "2")]
    [DataRow(14, "yyyyy", "XIV")]
    [DataRow(14, "yyyy", "XIV")]
    [DataRow(14, "yyy", "XIV")]
    [DataRow(14, "yy", "14")]
    [DataRow(14, "%y", "14")]
    public void Format_ShortYearFormatter_ReturnsYearNumber(int year, string format, string expectedString)
    {
        // Arrange
        FrenchRepublicanDateTimeFormatter provider = new();
        DateTime time = new(year, 1, 1, new FrenchRepublicanCalendar());
        format = $"{{0:{format}}}";

        // Act
        string actualString = String.Format(provider, format, time);

        // Assert
        Assert.AreEqual(expectedString, actualString);
    }


    /// <summary>
    /// Tests that formatting a French Republican calendar month using a supported month name format string returns the
    /// correct month name.
    /// </summary>
    /// <param name="month">The month number in the French Republican calendar to format.</param>
    /// <param name="expectedString">The expected month name string result for the given month and format.</param>
    [TestMethod]
    [DataRow(1, "Vendémiaire")]
    [DataRow(2, "Brumaire")]
    [DataRow(12, "Fructidor")]
    public void Format_MonthNameFormatter_ReturnsMonthName(int month, string expectedString)
    {
        // Arrange
        FrenchRepublicanDateTimeFormatter provider = new();
        DateTime time = new(1, month, 1, new FrenchRepublicanCalendar());
        string format = "{0:MMMM}";

        // Act
        string actualString = String.Format(provider, format, time);

        // Assert
        Assert.AreEqual(expectedString, actualString);
    }


    /// <summary>
    /// Tests that formatting a French Republican calendar month using supported month number format strings returns the
    /// expected month number representation.
    /// </summary>
    /// <param name="month">The month value to format, corresponding to the French Republican calendar month.</param>
    /// <param name="format">The format string specifying the month number format to use.</param>
    /// <param name="expectedString">The expected string result after formatting the month using the specified format.</param>
    [TestMethod]
    [DataRow(1, "MM", "01")]
    [DataRow(1, "%M", "1")]
    [DataRow(2, "MM", "02")]
    [DataRow(2, "%M", "2")]
    [DataRow(12, "MM", "12")]
    [DataRow(12, "%M", "12")]
    public void Format_SupportedMonthNumberFormatter_ReturnsMonthNumber(int month, string format, string expectedString)
    {
        // Arrange
        FrenchRepublicanDateTimeFormatter provider = new();
        DateTime time = new(1, month, 1, new FrenchRepublicanCalendar());
        format = $"{{0:{format}}}";

        // Act
        string actualString = String.Format(provider, format, time);

        // Assert
        Assert.AreEqual(expectedString, actualString);
    }


    /// <summary>
    /// Tests that formatting a French Republican date using a supported day name formatter returns the correct day name
    /// string.
    /// </summary>
    /// <param name="day">The day of the French Republican month to format.</param>
    /// <param name="expectedString">The expected day name string corresponding to the specified day.</param>
    [TestMethod]
    [DataRow(1, "Primidi")]
    [DataRow(2, "Duodi")]
    [DataRow(29, "Nonidi")]
    [DataRow(30, "Décadi")]
    public void Format_SupportedDayNameFormatter_ReturnsDayName(int day, string expectedString)
    {
        // Arrange
        FrenchRepublicanDateTimeFormatter provider = new();
        DateTime time = new(1, 1, day, new FrenchRepublicanCalendar());
        string format = "{0:dddd}";

        // Act
        string actualString = String.Format(provider, format, time);

        // Assert
        Assert.AreEqual(expectedString, actualString);
    }


    /// <summary>
    /// Verifies that formatting a French Republican calendar day using a supported day number format string returns the
    /// expected day number representation.
    /// </summary>
    /// <param name="day">The day of the month in the French Republican calendar to format. Must be a valid day number for the calendar.</param>
    /// <param name="format">The format string specifying how the day number should be represented. Supported values include standard numeric
    /// format strings such as "d" or "dd".</param>
    /// <param name="expectedString">The expected string result after formatting the day number with the specified format.</param>
    [TestMethod]
    [DataRow(1, "dd", "01")]
    [DataRow(1, "%d", "1")]
    [DataRow(5, "dd", "05")]
    [DataRow(5, "%d", "5")]
    [DataRow(30, "dd", "30")]
    [DataRow(30, "%d", "30")]
    public void Format_SupportedDayNumberFormatter_ReturnsDayNumber(int day, string format, string expectedString)
    {
        // Arrange
        FrenchRepublicanDateTimeFormatter provider = new();
        DateTime time = new(1, 1, day, new FrenchRepublicanCalendar());
        format = $"{{0:{format}}}";

        // Act
        string actualString = String.Format(provider, format, time);

        // Assert
        Assert.AreEqual(expectedString, actualString);
    }


    /// <summary>
    /// Tests that the FrenchRepublicanDateTimeFormatter correctly formats the hour component of a DateTime using
    /// supported hour format strings.
    /// </summary>
    /// <param name="hour">The hour value to be formatted. Must be in the range 0 to 23.</param>
    /// <param name="format">The hour format string to use.</param>
    /// <param name="expectedString">The expected string result after formatting the hour.</param>
    [TestMethod]
    [DataRow(0, "HH", "00")]
    [DataRow(0, "%H", "0")]
    [DataRow(0, "hh", "12")]
    [DataRow(0, "%h", "12")]
    [DataRow(1, "HH", "01")]
    [DataRow(1, "%H", "1")]
    [DataRow(1, "hh", "01")]
    [DataRow(1, "%h", "1")]
    [DataRow(12, "HH", "12")]
    [DataRow(12, "%H", "12")]
    [DataRow(12, "hh", "12")]
    [DataRow(12, "%h", "12")]
    [DataRow(13, "HH", "13")]
    [DataRow(13, "%H", "13")]
    [DataRow(13, "hh", "01")]
    [DataRow(13, "%h", "1")]
    [DataRow(23, "HH", "23")]
    [DataRow(23, "%H", "23")]
    [DataRow(23, "hh", "11")]
    [DataRow(23, "%h", "11")]
    public void Format_SupportedHourFormat_ReturnsHour(int hour, string format, string expectedString)
    {
        // Arrange
        FrenchRepublicanDateTimeFormatter provider = new();
        DateTime time = new(1, 1, 1, hour, 0, 0, new FrenchRepublicanCalendar());
        format = $"{{0:{format}}}";

        // Act
        string actualString = String.Format(provider, format, time);

        // Assert
        Assert.AreEqual(expectedString, actualString);
    }


    /// <summary>
    /// Verifies that the FrenchRepublicanDateTimeFormatter correctly formats the minute component of a date using
    /// supported format strings.
    /// </summary>
    /// <param name="minute">The minute value to format. Must be in the range 0 to 59.</param>
    /// <param name="format">The format string specifying how the minute should be represented.</param>
    /// <param name="expectedString">The expected string result after formatting the minute value.</param>
    [TestMethod]
    [DataRow(0, "mm", "00")]
    [DataRow(0, "%m", "0")]
    [DataRow(1, "mm", "01")]
    [DataRow(1, "%m", "1")]
    [DataRow(59, "mm", "59")]
    [DataRow(59, "%m", "59")]
    public void Format_SupportedMinuteFormat_ReturnsMinute(int minute, string format, string expectedString)
    {
        // Arrange
        FrenchRepublicanDateTimeFormatter provider = new();
        DateTime time = new(1, 1, 1, 0, minute, 0, new FrenchRepublicanCalendar());
        format = $"{{0:{format}}}";

        // Act
        string actualString = String.Format(provider, format, time);

        // Assert
        Assert.AreEqual(expectedString, actualString);
    }


    /// <summary>
    /// Tests that the FrenchRepublicanDateTimeFormatter correctly formats the seconds component of a DateTime using
    /// supported format strings.
    /// </summary>
    /// <param name="second">The seconds value to be formatted. Must be in the range 0 to 59.</param>
    /// <param name="format">The format string specifying how the seconds should be represented.</param>
    /// <param name="expectedString">The expected string result after formatting the seconds value.</param>
    [TestMethod]
    [DataRow(0, "ss", "00")]
    [DataRow(0, "%s", "0")]
    [DataRow(1, "ss", "01")]
    [DataRow(1, "%s", "1")]
    [DataRow(59, "ss", "59")]
    [DataRow(59, "%s", "59")]
    public void Format_SupportedSecondFormat_ReturnsSecond(int second, string format, string expectedString)
    {
        // Arrange
        FrenchRepublicanDateTimeFormatter provider = new();
        DateTime time = new(1, 1, 1, 0, 0, second, new FrenchRepublicanCalendar());
        format = $"{{0:{format}}}";

        // Act
        string actualString = String.Format(provider, format, time);

        // Assert
        Assert.AreEqual(expectedString, actualString);
    }


    [TestMethod]
    [DataRow("MMM")]
    [DataRow("ddd")]
    [DataRow("gg")]
    [DataRow("%g")]
    [DataRow("fffffff")]
    [DataRow("ffffff")]
    [DataRow("fffff")]
    [DataRow("ffff")]
    [DataRow("fff")]
    [DataRow("ff")]
    [DataRow("%f")]
    [DataRow("FFFFFFF")]
    [DataRow("FFFFFF")]
    [DataRow("FFFFF")]
    [DataRow("FFFF")]
    [DataRow("FFF")]
    [DataRow("FF")]
    [DataRow("%F")]
    [DataRow("%K")]
    [DataRow("zzz")]
    [DataRow("zz")]
    [DataRow("%z")]
    public void Format_UnsupportedFormatStrings_ReturnsEmptyString(string format)
    {
        // Arrange
        FrenchRepublicanDateTimeFormatter provider = new();
        DateTime time = new(1, 1, 1, new FrenchRepublicanCalendar());
        format = $"{{0:{format}}}";

        // Act
        string actualString = String.Format(provider, format, time);

        // Assert
        Assert.AreEqual(String.Empty, actualString);
    }


    /// <summary>
    /// Verifies that formatting a French Republican date and time using a custom format string and culture returns the
    /// expected formatted date string.
    /// </summary>
    /// <param name="year">The year component of the French Republican date to format.</param>
    /// <param name="month">The month component of the French Republican date to format.</param>
    /// <param name="day">The day component of the French Republican date to format.</param>
    /// <param name="hour">The hour component of the French Republican time to format.</param>
    /// <param name="minute">The minute component of the French Republican time to format.</param>
    /// <param name="second">The second component of the French Republican time to format.</param>
    /// <param name="format">The custom format string to use when formatting the date and time.</param>
    /// <param name="culture">The culture name to use for formatting, such as "fr-FR".</param>
    /// <param name="expectedString">The expected formatted date string result.</param>
    [TestMethod]
    [DataRow(1, 1, 1, 0, 0, 0, "yyyy-MM-dd HH:mm:ss", "fr-FR", "I-01-01 00:00:00")]
    public void Format_CustomFormatString_ReturnsFormattedDateString(int year, int month, int day, int hour, int minute, int second, string format, string culture, string expectedString)
    {
        // Arrange
        FrenchRepublicanDateTimeFormatter provider = new();
        DateTime time = new(year, month, day, hour, minute, second, new FrenchRepublicanCalendar());
        format = $"{{0:{format}}}";
        CultureInfo.CurrentCulture = new CultureInfo(culture);

        // Act
        string actualString = String.Format(provider, format, time);

        // Assert
        Assert.AreEqual(expectedString, actualString);
    }

}
