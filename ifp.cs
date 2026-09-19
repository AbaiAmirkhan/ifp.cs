using System;

public enum DeliveryType
{
    Pickup,
    Courier,
    DoorToDoor
}

public enum DeliveryZone
{
    City,
    OutsideCity,
    Remote
}

class Program
{
    static decimal ApplyRule(decimal price, Func<decimal, decimal> rule) => rule(price);

    static decimal CalculateDeliveryPrice(decimal basePrice, int itemAmount, DeliveryType type, DeliveryZone zone, bool isExpress)
    {
        Func<decimal, decimal> itemRule = currentPrice =>
        {
            if (itemAmount >= 8) return currentPrice * 1.20m;
            if (itemAmount >= 4) return currentPrice * 1.10m;
            return currentPrice;
        };

        Func<decimal, decimal> typeRule = currentPrice => type switch
        {
            DeliveryType.Pickup => currentPrice * 0.80m,
            DeliveryType.DoorToDoor => currentPrice * 1.15m,
            _ => currentPrice
        };

        decimal priceAfterItems = ApplyRule(basePrice, itemRule);
        decimal priceAfterType = ApplyRule(priceAfterItems, typeRule);

        decimal priceAfterZone = zone switch
        {
            DeliveryZone.OutsideCity => priceAfterType * 1.25m,
            _ => priceAfterType
        };

        decimal priceAfterExpress = isExpress ? priceAfterZone * 1.30m : priceAfterZone;

        return Math.Round(priceAfterExpress, 2);
    }

    static void Main(string[] args)
    {
        Console.Write("Enter base delivery price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal basePrice) || basePrice < 0)
        {
            Console.WriteLine("Error: Invalid base delivery price.");
            return;
        }

        Console.Write("Enter number of items: ");
        if (!int.TryParse(Console.ReadLine(), out int itemCount) || itemCount <= 0)
        {
            Console.WriteLine("Error: Invalid number of items.");
            return;
        }

        Console.Write("Enter delivery type (Pickup, Courier, DoorToDoor): ");
        if (!Enum.TryParse(Console.ReadLine(), true, out DeliveryType deliveryType))
        {
            Console.WriteLine("Error: Invalid delivery type.");
            return;
        }

        Console.Write("Enter delivery zone (City, OutsideCity, Remote): ");
        if (!Enum.TryParse(Console.ReadLine(), true, out DeliveryZone deliveryZone))
        {
            Console.WriteLine("Error: Invalid delivery zone.");
            return;
        }

        Console.Write("Is express delivery? (true/false): ");
        if (!bool.TryParse(Console.ReadLine(), out bool isExpress))
        {
            Console.WriteLine("Error: Invalid express status.");
            return;
        }

        decimal finalPrice = CalculateDeliveryPrice(basePrice, itemCount, deliveryType, deliveryZone, isExpress);

        Console.WriteLine($"\nFinal Delivery Price: {finalPrice}");
    }
}