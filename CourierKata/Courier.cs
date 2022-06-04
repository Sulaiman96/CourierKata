namespace CourierKata;

public class Courier
{
    private readonly List<string> _outputList;
    private bool _speedyShipping;
    public Courier()
    {
        _outputList = new List<string>();
    }

    public void CalculateOrder(IEnumerable<Parcel> orderList)
    {
        double runningOrderTotal = 0;
        foreach (var parcel in orderList)
        {
            parcel.Type = GetParcelType(parcel);
            var parcelTypeCost = CalculateTypeCost(parcel.Type);
            var parcelOverweightCost = CalculateOverweightCost(parcel.Type, parcel.Weight);
            runningOrderTotal += parcelTypeCost + parcelOverweightCost;

            _outputList.Add(_speedyShipping
                ? $"{parcel.Type} Parcel: ${parcelTypeCost}. Weight Cost: ${parcelOverweightCost}  Total Cost: ${runningOrderTotal}. With Speedy Shipping: ${runningOrderTotal * 2}"
                : $"{parcel.Type} Parcel: ${parcelTypeCost}. Weight Cost: ${parcelOverweightCost}  Total Cost: ${runningOrderTotal}");
        }
    }

    public void PrintReceipt()
    {
        foreach (var order in _outputList)
        {
            Console.WriteLine(order);
        }
    }

    public IEnumerable<string> GetReceipt()
    {
        return _outputList;
    }

    public double CalculateTypeCost(ParcelType parcelType)
    {
        switch (parcelType)
        {
            case ParcelType.Small:
                return 3.00;
            
            case ParcelType.Medium:
                return 8.00;
            
            case ParcelType.Large:
                return 15.00;

            case ParcelType.XL:
                return 25.00;
            
            case ParcelType.Heavy:
            default:
                return 50.00;
        }
    }
    
    public double CalculateOverweightCost(ParcelType parcelType, int parcelWeight)
    {
        switch (parcelType)
        {
            case ParcelType.Small:
                return parcelWeight <= 1 ? 0 : (parcelWeight - 1) * 2; //I will assume we don't deal with weights in decimals.
            
            case ParcelType.Medium:
                return parcelWeight <= 3 ? 0 : (parcelWeight - 3) * 2;
            
            case ParcelType.Large:
                return parcelWeight <= 6 ? 0 : (parcelWeight - 6) * 2;

            case ParcelType.XL:
                return parcelWeight <= 10 ? 0 : (parcelWeight - 10) * 2;
            
            case ParcelType.Heavy:
            default:
                return parcelWeight <= 50 ? 0 : (parcelWeight - 50);
        }
    }

    public ParcelType GetParcelType(Parcel parcel)
    {
        if (parcel.Weight >= 50)
            return ParcelType.Heavy;
        
        var (length, width, height) = parcel.GetParcelSize();

        if (length < 10 && width < 10 && height < 10)
            return ParcelType.Small;
        
        if (length < 50 && width < 50 && height < 50)
            return ParcelType.Medium;

        if (length < 100 && width < 100 && height < 100) 
            return ParcelType.Large;
        
        return ParcelType.XL;
    }
    
    public void TickSpeedyShipping()
    {
        _speedyShipping = !_speedyShipping;
    }
}