﻿// See https://aka.ms/new-console-template for more information

using CourierKata;

var orderHandler = new Courier();
var orderList = new List<Parcel>
{
    new Parcel(2, 4, 6, 2),
    new Parcel(10, 15, 4, 4),
    new Parcel(2, 55, 6, 12),
    new Parcel(2, 120, 6, 20),
    new Parcel(2, 120, 6, 60),
    new Parcel(2, 55, 6, 75)
};

orderHandler.TickSpeedyShipping();
orderHandler.CalculateOrder(orderList);
orderHandler.PrintReceipt();

