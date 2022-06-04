namespace CourierKata;

public class Courier
{
    private readonly List<string> _outputList;

    public Courier()
    {
        _outputList = new List<string>();
    }

    public void CalculateOrder(List<Parcel> orderList)
    {
        double runningOrderTotal = 0;
        foreach (var parcel in orderList)
        {
            parcel.Type = GetParcelType(parcel.GetParcelSize());
            var parcelCost = CalculateCost(parcel.Type);
            runningOrderTotal += parcelCost;
            
            _outputList.Add($"{parcel.Type}: ${parcelCost}. Total Cost: ${runningOrderTotal}");
        }
    }

    public void PrintReceipt()
    {
        foreach (var order in _outputList)
        {
            Console.WriteLine(order);
        }
    }

    public double CalculateCost(ParcelType parcelType)
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
            default:
                return 25.00;
        }
    }

    public static ParcelType GetParcelType((int, int, int) parcelSize)
    {
        var (length, width, height) = parcelSize;

        if (length < 10 && width < 10 && height < 10)
            return ParcelType.Small;
        
        if (length < 50 && width < 50 && height < 50)
            return ParcelType.Medium;

        if (length < 100 && width < 100 && height < 100) 
            return ParcelType.Large;
        
        return ParcelType.XL;
    }
}