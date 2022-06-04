using System;
using Xunit;

namespace CourierKata.Tests;

public class ParcelTest
{
    [Fact]
    public void GetParcelSize_ShouldReturnTupleCorrectly()
    {
        var expectedTuple = (2, 4, 6);
        
        var actualTuple = new Parcel(2, 4, 6, 1).GetParcelSize();

        Assert.Equal(expectedTuple, actualTuple);
    }
    
    [Theory]
    [InlineData(0, 2, 4, 1)]
    [InlineData(4, 0, 4, 3)]
    [InlineData(2, 2, 0, 5)]
    public void ConstructingFalseParcel_ShouldThrowArgumentException(int length, int width, int height, int weight)
    {
        Assert.Throws<ArgumentException>(() => new Parcel(length, width, height, weight));
    }
}