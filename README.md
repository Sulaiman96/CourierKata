# CourierKata
You work for a courier company and have been tasked with creating a code library to calculate the cost of sending an order of parcels. 

- You may approach this in the language you feel most comfortable using. 
- The API for the library should be programmatic. There is no need to implement a CLI, HTTP, or any other transport layer. 
- Try not to peek ahead at future steps and commit your work as you go. 
- Input can be in any form you choose. 
- Output should be a collection of items with their individual cost and type, as well as total cost. 
- In all circumstances the cheapest option for sending each parcel should be selected. 
- You are expected to use test driven development (TDD). 

##  Implementation Steps
1) The initial implementation just needs to take into account a parcel's size. For each size type there is a fixed delivery cost 
- Small parcel: all dimensions < 10cm. Cost $3 
- Medium parcel: all dimensions < 50cm. Cost $8 
- Large parcel: all dimensions < 100cm. Cost $15 
- XL parcel: any dimension >= 100cm. Cost $25 

2) Thanks to logistics improvements we can deliver parcels faster. This means we can charge more money. Speedy shipping can be selected by the user to take advantage of our improvements. 
- This doubles the cost of the entire order
- Speedy shipping should be listed as a separate item in the output, with its associated cost 
- Speedy shipping should not impact the price of individual parcels, i.e. their individual cost should remain the same as it was before 

3) There have been complaints from delivery drivers that people are taking advantage of our dimension only shipping costs. A new weight limit has been added for each parcel type, over which a charge per kg of weight applies +$2/kg over weight limit for parcel size: 
- Small parcel: 1kg 
- Medium parcel: 3kg 
- Large parcel: 6kg 
- XL parcel: 10kg 

4) Some of the extra weight charges for certain goods were excessive. A new parcel type has been added to try and address overweight parcels 
- Heavy parcel (limit 50kg), $50. +$1/kg over

5) In order to award those who send multiple parcels, special discounts have been introduced. 
- Small parcel mania! Every 4th small parcel in an order is free! 
- Medium parcel mania! Every 3rd medium parcel in an order is free! 
- Mixed parcel mania! Every 5th parcel in an order is free! 
- Each parcel can only be used in a discount once 
- Within each discount, the cheapest parcel is the free one 
- The combination of discounts which saves the most money should be selected every time 

Example: 
6x medium parcel. 3 x $8, 3x $10. 1st discount should include all 3 $8 parcels and save $8. 2nd discount should include all 3 $10 parcels and save $10. 
- Just like speedy shipping, discounts should be listed as a separate item in the output, with associated saving, e.g. "-2" 
- Discounts should not impact the price of individual parcels, i.e. their individual cost should remain the same as it was before 
- Speedy shipping applies after discounts are taken into account

## Platform & Language
Using .Net 6 Console Application | C# 10.0

## Testing Framework
Using xUnit

## Solution
Courier is the class that handles Calculating an order and Printing out the Receipt.

## TL;DR
How to use the Courier Library
```C#
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
```
This will result into:
```bash
Small Parcel: $3. Weight Cost: $2  Total Cost: $5. Total Saved: $0. Total Cost With Speedy Shipping: $10
Small Parcel: $3. Weight Cost: $6  Total Cost: $14. Total Saved: $0. Total Cost With Speedy Shipping: $28
Medium Parcel: $8. Weight Cost: $2  Total Cost: $24. Total Saved: $0. Total Cost With Speedy Shipping: $48
Large Parcel: $15. Weight Cost: $12  Total Cost: $51. Total Saved: $0. Total Cost With Speedy Shipping: $102
XL Parcel: $0. Weight Cost: $0  Total Cost: $51. Total Saved: $45. Total Cost With Speedy Shipping: $102
Heavy Parcel: $50. Weight Cost: $10  Total Cost: $111. Total Saved: $45. Total Cost With Speedy Shipping: $222
Small Parcel: $3. Weight Cost: $86  Total Cost: $200. Total Saved: $45. Total Cost With Speedy Shipping: $400
Small Parcel: $0. Weight Cost: $0  Total Cost: $200. Total Saved: $70. Total Cost With Speedy Shipping: $400
```

The option to tick speedy shipping is completely optional. This will remove the total cost with speedy shipping.

This is the output of the same code above, but without the ```orderHandler.TickSpeedyShipping();```

```bash
Small Parcel: $3. Weight Cost: $2  Total Cost: $5. Total Saved: $0.
Small Parcel: $3. Weight Cost: $6  Total Cost: $14. Total Saved: $0.
Medium Parcel: $8. Weight Cost: $2  Total Cost: $24. Total Saved: $0.
Large Parcel: $15. Weight Cost: $12  Total Cost: $51. Total Saved: $0.
XL Parcel: $0. Weight Cost: $0  Total Cost: $51. Total Saved: $45.
Heavy Parcel: $50. Weight Cost: $10  Total Cost: $111. Total Saved: $45.
Small Parcel: $3. Weight Cost: $86  Total Cost: $200. Total Saved: $45.
Small Parcel: $0. Weight Cost: $0  Total Cost: $200. Total Saved: $70.
```

### Parcel
When generating Parcel, it is crucial to know that it requires four parameters. The length, width, height and weight of the parcel. 
```C#
public Parcel(int length, int width, int height, int weight)
    {
        if (length == 0 || width == 0 || height == 0 || weight == 0)
            throw new ArgumentException("Please check your values, Zero is not allowed!");

        _size._length = length;
        _size._width = width;
        _size._height = height;

        Weight = weight;
    }
```
### Tests
You will find all of the tests in CourierKata.Tests

There are two files, one that tests Courier called "CourierTests" and another which tests Parcel called "ParcelTests".

## Lessons Learnt
This was a great project to get my thinking hat on. It forced me to think about a good design which is not only testable, but allows room for extension.

A lot of time was wasted overthinking design decisions to try and get something perfect, when in reality this problem has a lot of ways it could be solved.

Tests help me write code that is testable, having tests in mind whilst I code allows me to keep functions small.

# Future Improvement
If I had time, there are several things I would like to do. If I do find time, I will probably come back and implement these changes and try to make it as close to production ready code as I can. 

1) The first thing I would do is write a correct behaviour for solving discounts for parcels. I believe there is a flaw in my current implementation, it does not take into account the cheapest parcel to give the discount to. I'm assuming I would need to sort the list in order of ParcelType and then cost. 
- In the case where we have 5 parcel types (Small, Small, Small, Medium, Small). The discount will only apply to 5th item once. Even though, it should apply twice if the list was sorted, meaning that the 4th small parcel will be free alongside the 5th Medium parcel. 

2) The next thing I would do is make it close to production ready code, although I have done that as much as I could by using IEnumerable whenever a function takes a list or returns one, allowing for users to send anything that implements that interface. There are still some magic numbers hanging around, for instance Calculating Type or Weight costs has a lot of magic numbers that could be moved to variables that the company could set. I could also make use of dependency injection by creating a class DiscountHandler which could be passed in as a service when creating an instance of Courier to make use of for handling discounts. 

3) The parcel class should be self contained. At the moment courier is doing a little too much, the parcel when instantiated could calculate its own price as this never changes after being set. It would also remove the unnecessary calls of getting the type of the parcel or its cost, when the parcel object itself should have these values, there is no need for the Courier to do this!

4) The output, I believe the print method could be formatted a lot better to provide a prettier and cleaner receipt. The method CalculateOrder, if read by someone, one might expect a return. So in theory, I should be returning something rather than just keeping a log of it. 

5) Lastly, I believe the CalculateOrder method is doing too much. Although each piece of it is broken into its own testable methods that are doing 1 unit of task. I still think that function handles too much and I would love to think about how I could redesign it in a way where the method does less work without the user having to do more when using my library. 

P.S. The TickSpeedyCheck method now that I think about it, is awful, the user using my library would have no idea to call it again to uncheck it. It should be passed into the constructor when creating a courier, with the option of having a method that could uncheck/check it. 

## Time Taken
I believe github timestamps are a little misguided, I took several breaks as it was the jubilee bank holiday and even when I was coding, most of the time was actually spent on either design decisions or unit tests. But, I really enjoyed it and feel like I have become a slightly better coder because of it. 

Total spent on task 1-3 -> 1-2 hours, this was mainly because although I initially had a simple function that passed the test, when I wanted to pass an list of parcels to be calculated, I realised I would need a Parcel class, then when it got to calculation, I realised I needed ParcelType and all the other methods to achieve it. 

Total time spent on task 4 -> maybe 5-10 min, thanks to the good design (imo) this was quite quick. 

Total time spent on task 5 -> 1-2 hours, This one was slightly confusing and I had to refactor my calculate method as I didn't want it to be too crowded. I still believe the implementation is not correct as I would need to sort it, but I don't want to spend too much time on this project either for now as I am quite busy. I will come back to it!
