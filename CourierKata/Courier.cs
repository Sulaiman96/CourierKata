namespace CourierKata;

public class Courier
{
    private readonly List<string> _outputList;
    private bool _speedyShipping;
    private const int SmallParcelMania = 4;
    private const int MediumParcelMania = 3;
    private const int MixedParcelMania = 5;
    private const string Currency = "$";
    
    public Courier()
    {
        _outputList = new List<string>();
    }

    public void CalculateOrder(IEnumerable<Parcel> orderList)
    {
        double runningOrderTotal = 0;
        double runningDiscountTotal = 0;
        var orderListWithDiscountsApplied = CalculateDiscounts(orderList);
        foreach (var (parcel, applyDiscount) in orderListWithDiscountsApplied)
        {
            double parcelTypeCost = 0;
            double parcelOverweightCost = 0;
            if (applyDiscount)
            {
                runningDiscountTotal += CalculateTypeCost(parcel.Type);
                runningDiscountTotal += CalculateOverweightCost(parcel.Type, parcel.Weight);
            }
            else
            {
                parcelTypeCost = CalculateTypeCost(parcel.Type, applyDiscount);
                parcelOverweightCost = CalculateOverweightCost(parcel.Type, parcel.Weight, applyDiscount);
                runningOrderTotal += parcelTypeCost + parcelOverweightCost;
            }

            _outputList.Add(_speedyShipping
                ? $"{parcel.Type} Parcel: {Currency}{parcelTypeCost}. Weight Cost: {Currency}{parcelOverweightCost}  Total Cost: {Currency}{runningOrderTotal}. Total Saved: {Currency}{runningDiscountTotal}. Total Cost With Speedy Shipping: {Currency}{runningOrderTotal * 2}"
                : $"{parcel.Type} Parcel: {Currency}{parcelTypeCost}. Weight Cost: {Currency}{parcelOverweightCost}  Total Cost: {Currency}{runningOrderTotal}. Total Saved: {Currency}{runningDiscountTotal}. ");
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

    public double CalculateTypeCost(ParcelType parcelType, bool applyDiscount = false)
    {
        if (applyDiscount)
            return 0;
        
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
    
    public double CalculateOverweightCost(ParcelType parcelType, int parcelWeight, bool applyDiscount = false)
    {
        if (applyDiscount)
            return 0;
        
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
    
    /* -- Summary --
    Prioritising the most expensive option and making my way down to cheapest.
    
    The TryAdd protects against any duplication So there is a layer
    of protection.
    */
    public IDictionary<Parcel, bool> CalculateDiscounts(IEnumerable<Parcel> orderList)
    {
        var mixedParcelDiscountCounter = 0;
        var mediumParcelDiscountCounter = 0;
        var smallParcelDiscountCounter = 0;

        var parcelsWithDiscountTags = new Dictionary<Parcel, bool>();
        
        foreach (var parcel in orderList)
        {
            parcel.Type = GetParcelType(parcel);
            switch (parcel.Type)
            {
                case ParcelType.Medium:
                    mediumParcelDiscountCounter++;
                    break;
                case ParcelType.Small:
                    smallParcelDiscountCounter++;
                    break;
            }
            mixedParcelDiscountCounter++;

            if (mixedParcelDiscountCounter != 0 && mixedParcelDiscountCounter % MixedParcelMania == 0)
            {
                parcelsWithDiscountTags.TryAdd(parcel, true);
            }
            else if (mediumParcelDiscountCounter != 0 && mediumParcelDiscountCounter % MediumParcelMania == 0)
            {
                parcelsWithDiscountTags.TryAdd(parcel, true);
            }
            else if (smallParcelDiscountCounter != 0 && smallParcelDiscountCounter % SmallParcelMania == 0)
            {
                parcelsWithDiscountTags.TryAdd(parcel, true);
            }
            
            parcelsWithDiscountTags.TryAdd(parcel, false);
        }

        return parcelsWithDiscountTags;
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