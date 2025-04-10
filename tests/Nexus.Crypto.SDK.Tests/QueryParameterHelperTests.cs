namespace Nexus.Crypto.SDK.Tests;

using Xunit;

public class QueryParameterHelperTests
{
    private class TestObject
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }

    [Fact]
    public void ToQueryString_NullObject_ReturnsEmptyString()
    {
        // Arrange
        object obj = null;

        // Act
        var result = QueryParameterHelper.ToQueryString(obj);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ToQueryString_EmptyObject_ReturnsEmptyString()
    {
        // Arrange
        var obj = new { };

        // Act
        var result = QueryParameterHelper.ToQueryString(obj);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ToQueryString_ObjectWithProperties_ReturnsQueryString()
    {
        // Arrange
        var obj = new TestObject { Name = "John Doe", Age = 30, City = "New York" };

        // Act
        var result = QueryParameterHelper.ToQueryString(obj);

        // Assert
        Assert.Equal("name=John%20Doe&age=30&city=New%20York", result);
    }

    [Fact]
    public void ToQueryString_ObjectWithSpecialCharacters_ReturnsEncodedQueryString()
    {
        // Arrange
        var obj = new TestObject { Name = "John & Jane", Age = 25, City = "Los Angeles" };

        // Act
        var result = QueryParameterHelper.ToQueryString(obj);

        // Assert
        Assert.Equal("name=John%20%26%20Jane&age=25&city=Los%20Angeles", result);
    }

    [Fact]
    public void ToQueryString_ObjectWithNullProperties_IgnoresNullProperties()
    {
        // Arrange
        var obj = new TestObject { Name = "Alice", Age = 0, City = null };

        // Act
        var result = QueryParameterHelper.ToQueryString(obj);

        // Assert
        Assert.Equal("name=Alice&age=0", result);
    }
}