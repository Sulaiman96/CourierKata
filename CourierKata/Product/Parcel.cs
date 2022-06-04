namespace CourierKata;

public class Parcel
{
    public ParcelType Type { get; set; }
    public int Weight { get; }
    
    private readonly (int _length, int _width, int _height) _size;

    public Parcel(int length, int width, int height, int weight)
    {
        if (length == 0 || width == 0 || height == 0 || weight == 0)
            throw new ArgumentException("Please check your values, Zero is not allowed!");
        
        _size._length = length;
        _size._width = width;
        _size._height = height;

        Weight = weight;
    }

    public (int, int, int) GetParcelSize()
    {
        return _size;
    }
}