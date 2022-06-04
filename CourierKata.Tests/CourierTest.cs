using System.Collections.Generic;
using Xunit;

namespace CourierKata.Tests;

public class CourierTest
{
    private readonly Courier _courier = new Courier();
    
    [Theory]
    [InlineData(2, 5, 9, ParcelType.Small)]
    [InlineData(14, 5, 9, ParcelType.Medium)]
    [InlineData(2, 55, 9, ParcelType.Large)]
    [InlineData(2, 5, 144, ParcelType.XL)]
    public void GetParcelType_ShouldReturnTypeCorrectly(int length, int width, int height, ParcelType expectedType)
    {
        (int, int, int) parcelSize = (length, width, height);
        
        var actualType = Courier.GetParcelType(parcelSize);
        
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

    [Fact]
    public void CalculateOrder_ShouldHaveShippingDetails()
    {
        var orderList = new List<Parcel>
        {
            new Parcel(2, 4, 6),
            new Parcel(10, 15, 4),
            new Parcel(2, 55, 6),
            new Parcel(2, 120, 6)
        };
        
        _courier.TickSpeedyShipping();
        _courier.CalculateOrder(orderList);

        var receipt = _courier.GetReceipt();
        
        Assert.All(receipt, line => Assert.Contains("With Speedy Shipping", line));
    }
}