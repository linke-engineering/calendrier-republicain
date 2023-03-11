namespace Sinistrius.CalendrierRepublicain.UnitTests;


/// <summary>
/// Tests the <see cref="FrenchRepublicanDateTimeFormatter"/> class.
/// </summary>
[TestClass]
public class FrenchRepublicanDateTimeFormatterTests
{

    /// <summary>
    /// Tests the <see cref="FrenchRepublicanDateTimeFormatter.GetFormat(Type?)"/> method.
    /// </summary>
    [TestMethod]
    [Ignore("Not sure what to expect here")]
    [DataRow(typeof(FrenchRepublicanDateTimeFormatter))]
    public void GetFormat_ICustomFormatter_ReturnsProvider(Type type)
    {
        // Arrange
        FrenchRepublicanDateTimeFormatter provider = new();

        // Act
        object? actualFormat = provider.GetFormat(type);

        // Assert
        Assert.IsInstanceOfType(actualFormat, typeof(FrenchRepublicanDateTimeFormatter));
    }

}
