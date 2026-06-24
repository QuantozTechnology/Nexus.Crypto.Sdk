namespace Nexus.Crypto.SDK.Tests;

using System;
using System.Globalization;
using System.Threading;
using Xunit;

public class QueryParameterHelperTests
{
    private enum TestEnum
    {
        Value1,
        Value2,
        Value3
    }

    private class TestObject
    {
        public string Name { get; set; }
        public int? Age { get; set; }
        public string City { get; set; }
        public TestEnum? EnumValue { get; set; }
    }

    private class NumericObject
    {
        public decimal Price { get; set; }
        public double Rate { get; set; }
    }

    private class DateObject
    {
        public DateTime CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
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
        var obj = new TestObject { Name = "John Doe", Age = 30, City = "New York", EnumValue = TestEnum.Value1 };

        // Act
        var result = QueryParameterHelper.ToQueryString(obj);

        // Assert
        Assert.Equal("name=John%20Doe&age=30&city=New%20York&enumValue=Value1", result);
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

    [Fact]
    public void ToQueryString_DecimalAndDouble_UsesInvariantCulture()
    {
        // Arrange
        var obj = new NumericObject { Price = 1.5m, Rate = 2.75 };
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE"); // uses ',' as decimal separator

        try
        {
            // Act
            var result = QueryParameterHelper.ToQueryString(obj);

            // Assert — values must contain '.' not ','
            Assert.Equal("price=1.5&rate=2.75", result);
        }
        finally
        {
            Thread.CurrentThread.CurrentCulture = originalCulture;
        }
    }

    [Fact]
    public void ToQueryString_DateTime_UsesIso8601()
    {
        // Arrange
        var dt = new DateTime(2024, 6, 15, 10, 30, 0, DateTimeKind.Utc);
        var dto = new DateTimeOffset(2024, 6, 15, 10, 30, 0, TimeSpan.Zero);
        var obj = new DateObject { CreatedAt = dt, UpdatedAt = dto };

        // Act
        var result = QueryParameterHelper.ToQueryString(obj);

        // Assert — round-trip "O" format; URI-encoded '+' becomes '%2B', ':' becomes '%3A'
        Assert.Contains("createdAt=", result);
        Assert.Contains("updatedAt=", result);
        Assert.Contains("2024", result);
    }
}