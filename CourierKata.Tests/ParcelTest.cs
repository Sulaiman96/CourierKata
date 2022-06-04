using System;
using Xunit;

namespace CourierKata.Tests;

public class ParcelTest
{
    [Fact]
    public void GetParcelSize_ShouldReturnTupleCorrectly()
    {
        (int, int, int) expectedTuple = (2, 4, 6);

        var actualTuple = new Parcel(2, 4, 6).GetParcelSize();
        
        Assert.Equal(expectedTuple, actualTuple);
    }
    
    [Theory]
    [InlineData(0, 2, 4)]
    [InlineData(4, 0, 4)]
    [InlineData(2, 2, 0)]
    public void ConstructingFalseParcel_ShouldThrowArgumentException(int length, int width, int height)
    {
        Assert.Throws<ArgumentException>(() => new Parcel(length, width, height));
    }
}