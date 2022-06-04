﻿// See https://aka.ms/new-console-template for more information

using CourierKata;

Courier orderHandler = new Courier();
List<Parcel> orderList = new List<Parcel>
{
    new Parcel(2, 4, 6),
    new Parcel(10, 15, 4),
    new Parcel(2, 55, 6),
    new Parcel(2, 120, 6)
};

orderHandler.CalculateOrder(orderList);
orderHandler.PrintReceipt();