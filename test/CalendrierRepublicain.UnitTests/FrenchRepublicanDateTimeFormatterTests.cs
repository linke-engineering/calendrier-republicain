namespace LinkeEngineering.CalendrierRepublicain.UnitTests;


[TestClass]
public class FrenchRepublicanDateTimeFormatterTests
{

    #region Fields

    /// <summary>
    /// The formatter to test.
    /// </summary>
    private readonly FrenchRepublicanDateTimeFormatter _formatter = new();

    #endregion


    #region Methods

    /// <summary>
    /// Tests that the <see cref="FrenchRepublicanDateTimeFormatter.GetFormat(Type)"/> method returns a valid formatter.
    /// </summary>
    [TestMethod]
    public void GetFormat_ValidFormatter_ReturnsFrenchRepublicanDateTimeFormatter()
    {
        // Act
        object? result = _formatter.GetFormat(typeof(ICustomFormatter));

        // Assert
        Assert.IsInstanceOfType(result, typeof(FrenchRepublicanDateTimeFormatter));
    }


    /// <summary>
    /// Tests that the <see cref="FrenchRepublicanDateTimeFormatter.GetFormat(Type)"/> method returns null for an invalid formatter.
    /// </summary>
    [TestMethod]
    public void GetFormat_InvalidFormatter_ReturnsNull()
    {
        // Act
        object? result = _formatter.GetFormat(typeof(string));

        // Assert
        Assert.IsNull(result);
    }

    #endregion

}
