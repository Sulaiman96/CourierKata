namespace CourierKata;

public class Parcel
{
    public ParcelType Type { get; set; }
    private readonly (int _length, int _width, int _height) _size;

    public Parcel(int length, int width, int height)
    {
        if (length == 0 || width == 0 || height == 0)
            throw new ArgumentException("Please check your values, Zero is not allowed!");
        
        _size._length = length;
        _size._width = width;
        _size._height = height;
    }

    public (int, int, int) GetParcelSize()
    {
        return _size;
    }
}