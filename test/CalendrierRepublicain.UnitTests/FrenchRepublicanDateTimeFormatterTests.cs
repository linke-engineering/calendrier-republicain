using System.Globalization;


namespace LinkeEngineering.CalendrierRepublicain.UnitTests;


/// <summary>
/// Contains unit tests for the <see cref="FrenchRepublicanDateTimeFormatter"/> class, verifying correct formatting 
/// and culture-specific behavior for French Republican calendar date and time values.
/// </summary>
[TestClass]
public class FrenchRepublicanDateTimeFormatterTests
{

    #region Fields

    /// <summary>
    /// The format provider to test.
    /// </summary>
    private readonly FrenchRepublicanDateTimeFormatter _provider = new();

    #endregion


    /// <summary>
    /// Tests that the <see cref="FrenchRepublicanDateTimeFormatter.GetFormat(Type)"/> method returns a valid 
    /// formatter.
    /// </summary>
    [TestMethod]
    public void GetFormat_ValidFormatter_ReturnsFrenchRepublicanDateTimeFormatter()
    {
        // Act
        var result = _provider.GetFormat(typeof(ICustomFormatter));

        // Assert
        Assert.IsInstanceOfType<FrenchRepublicanDateTimeFormatter>(result);
    }


    /// <summary>
    /// Tests that the <see cref="FrenchRepublicanDateTimeFormatter.GetFormat(Type)"/> method returns null for an 
    /// invalid formatter.
    /// </summary>
    [TestMethod]
    public void GetFormat_InvalidFormatter_ReturnsNull()
    {
        // Act
        var result = _provider.GetFormat(typeof(string));

        // Assert
        Assert.IsNull(result);
    }


    /// <summary>
    /// Verifies that formatting a time value using various format strings and cultures produces the expected output
    /// pattern.
    /// </summary>
    /// <remarks>This test iterates through a set of test data, applying each format string and culture to
    /// ensure the formatting provider returns the correct result. It helps validate that the custom formatting logic
    /// works as intended across different locales and format patterns.</remarks>
    [TestMethod]
    public void Format_SupportedFormatString_ReturnsExpectedPattern()
    {
        var originalCulture = CultureInfo.CurrentCulture;

        try
        {
            foreach (var (FormatString, time, culture, expected) in TestData.Data)
            {
                // Arrange
                var format = $"{{0:{FormatString}}}";
                CultureInfo.CurrentCulture = new CultureInfo(culture);

                // Act
                var actual = String.Format(_provider, format, time);

                // Assert
                Assert.AreEqual(expected, actual, $"Failed for FormatString: {FormatString}, Culture: {culture}");
            }
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
        }
    }


    /// <summary>
    /// Verifies that formatting a French Republican calendar date with an unsupported format pattern throws a
    /// FormatException.
    /// </summary>
    /// <remarks>This test ensures that the formatter correctly rejects unsupported format patterns by
    /// throwing a FormatException. It is parameterized to cover multiple unsupported patterns.</remarks>
    /// <param name="formatString">The format string to test. Represents a standard or custom date and time format pattern that is not supported by
    /// the French Republican calendar formatter.</param>
    [TestMethod]
    [DataRow("o")]
    [DataRow("O")]
    [DataRow("r")]
    [DataRow("R")]
    [DataRow("s")]
    [DataRow("u")]
    [DataRow("U")]
    public void Format_UnsupportedStandardFormatString_ThrowsFormatException(string formatString)
    {
        // Arrange
        var time = new DateTime(1, 1, 1, new FrenchRepublicanCalendar());
        var format = $"{{0:{formatString}}}";

        // Act & Assert
        Assert.Throws<FormatException>(() => String.Format(_provider, format, time));
    }

}
