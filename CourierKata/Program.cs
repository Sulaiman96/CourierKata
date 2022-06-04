// See https://aka.ms/new-console-template for more information

using CourierKata;

var orderHandler = new Courier();
var orderList = new List<Parcel>
{
    new Parcel(2, 4, 6, 2),
    new Parcel(3, 1, 4, 4),
    new Parcel(10, 15, 4, 4),
    new Parcel(2, 55, 6, 12),
    new Parcel(2, 120, 6, 20),
    new Parcel(2, 120, 6, 60),
    new Parcel(2, 2, 7, 44),
    new Parcel(2, 3, 5, 12)
};

orderHandler.TickSpeedyShipping();
orderHandler.CalculateOrder(orderList);
orderHandler.PrintReceipt();
