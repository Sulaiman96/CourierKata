using Xunit;

namespace CourierKata.Tests;

public class CourierTest
{
    readonly Courier _courier = new Courier();
    
    [Theory]
    [InlineData(2, 5, 9, ParcelType.Small)]
    [InlineData(14, 5, 9, ParcelType.Medium)]
    [InlineData(2, 55, 9, ParcelType.Large)]
    [InlineData(2, 5, 144, ParcelType.XL)]
    public void GetParcelType_ShouldReturnTypeCorrectly(int length, int width, int height, ParcelType expectedType)
    {
        (int, int, int) parcelSize = (length, width, height);
        
        ParcelType actualType = Courier.GetParcelType(parcelSize);
        
        Assert.Equal(expectedType, actualType);
    }

    [Theory]
    [InlineData(3.00, ParcelType.Small)]
    [InlineData(8.00, ParcelType.Medium)]
    [InlineData(15.00, ParcelType.Large)]
    [InlineData(25.00, ParcelType.XL)]
    public void CalculateCost_ShouldReturnCostsCorrectly(double expectedPrice, ParcelType parcelType)
    {
        var actualPrice = _courier.CalculateCost(parcelType);
        
        Assert.Equal(expectedPrice, actualPrice);
    }
}