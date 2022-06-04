using System.Collections.Generic;
using Xunit;

namespace CourierKata.Tests;

public class CourierTest
{
    private readonly Courier _courier = new Courier();

    #region Theory Tests
    [Theory]
    [InlineData(2, 5, 9, 1, ParcelType.Small)]
    [InlineData(14, 5, 9, 6, ParcelType.Medium)]
    [InlineData(2, 55, 9, 9, ParcelType.Large)]
    [InlineData(2, 5, 144, 20, ParcelType.XL)]
    [InlineData(2, 5, 144, 55, ParcelType.Heavy)]
    public void GetParcelType_ShouldReturnTypeCorrectly(int length, int width, int height, int weight, ParcelType expectedType)
    {
        var parcelSize = new Parcel(length, width, height, weight);
        
        var actualType = _courier.GetParcelType(parcelSize);
        
        Assert.Equal(expectedType, actualType);
    }

    [Theory]
    [InlineData(3.00, ParcelType.Small)]
    [InlineData(8.00, ParcelType.Medium)]
    [InlineData(15.00, ParcelType.Large)]
    [InlineData(25.00, ParcelType.XL)]
    [InlineData(50.00, ParcelType.Heavy)]
    public void CalculateCost_ShouldReturnCostsCorrectly(double expectedPrice, ParcelType parcelType)
    {
        var actualPrice = _courier.CalculateTypeCost(parcelType);
        
        Assert.Equal(expectedPrice, actualPrice);
    }

    [Theory]
    [InlineData(1, 0, ParcelType.Small)]
    [InlineData(2, 2, ParcelType.Small)]
    [InlineData(4, 2, ParcelType.Medium)]
    [InlineData(10, 8, ParcelType.Large)]
    [InlineData(25, 30, ParcelType.XL)]
    [InlineData(55, 5, ParcelType.Heavy)]
    [InlineData(75, 25, ParcelType.Heavy)]
    public void CalculateOverweightCost_ShouldReturnOverweightCostCorrectly(int weight, int expectedOverweightCost, ParcelType parcelType)
    {
        var actualOverweightCost = _courier.CalculateOverweightCost(parcelType, weight);
        
        Assert.Equal(expectedOverweightCost, actualOverweightCost);
    }
    #endregion

    #region Fact Tests
    [Fact]
    public void CalculateOrder_ShouldHaveShippingDetails()
    {
        var orderList = new List<Parcel>
        {
            new Parcel(2, 4, 6, 1),
            new Parcel(10, 15, 4, 2),
            new Parcel(2, 55, 6, 4),
            new Parcel(2, 120, 6, 6)
        };
        
        _courier.TickSpeedyShipping();
        _courier.CalculateOrder(orderList);

        var receipt = _courier.GetReceipt();
        
        Assert.All(receipt, line => Assert.Contains("With Speedy Shipping", line));
    }
    
    #endregion
    
}