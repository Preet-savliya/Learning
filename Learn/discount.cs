using System;
using System.Collections.Generic;

namespace DiscountSystem
{
    // 1. ABSTRACTION: The interface defines 'what' a discount does.
    public interface IDiscountStrategy
    {
        decimal ApplyDiscount(decimal amount);
    }

    // 2. ENCAPSULATION & POLYMORPHISM: Each class has its own specific math logic.
    
    // Logic for No Discount
    public class NoDiscount : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal amount) => amount;
    }

    // Logic for Percentage (e.g., 10% off)
    public class PercentageDiscount : IDiscountStrategy
    {
        private readonly decimal _rate;
        public PercentageDiscount(decimal rate) => _rate = rate;

        public decimal ApplyDiscount(decimal amount) 
        {
            return amount - (amount * _rate);
        }
    }

    // Logic for Fixed Amount (e.g., $5 off)
    public class FixedAmountDiscount : IDiscountStrategy
    {
        private readonly decimal _discountValue;
        public FixedAmountDiscount(decimal value) => _discountValue = value;

        public decimal ApplyDiscount(decimal amount)
        {
            // Logic: Ensure the price never goes below zero
            decimal result = amount - _discountValue;
            return result < 0 ? 0 : result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 3. COLLECTIONS: Storing our strategies in a Dictionary
            var discountMenu = new Dictionary<string, IDiscountStrategy>
            {
                { "1", new NoDiscount() },
                { "2", new PercentageDiscount(0.10m) }, // 10% off
                { "3", new FixedAmountDiscount(5.00m) }  // $5 off
            };

            try
            {
                Console.WriteLine("--- ERP Discount Engine ---");
                Console.Write("Enter Product Price: ");
                
                // Using decimal for money
                if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    throw new Exception("Invalid price entered. Please use numbers.");
                }

                Console.WriteLine("\nSelect Discount Type:");
                Console.WriteLine("1: No Discount");
                Console.WriteLine("2: 10% Off");
                Console.WriteLine("3: $5 Fixed Discount");
                Console.Write("Choice: ");
                string choice = Console.ReadLine();

                // 4. EXECUTION: Using the strategy
                if (discountMenu.ContainsKey(choice))
                {
                    IDiscountStrategy strategy = discountMenu[choice];
                    decimal finalPrice = strategy.ApplyDiscount(price);

                    Console.WriteLine("---------------------------");
                    Console.WriteLine($"Original Price: {price:C}"); // :C formats as Currency
                    Console.WriteLine($"Final Price:    {finalPrice:C}");
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                }
            }
            catch (Exception ex)
            {
                // 5. EXCEPTION HANDLING
                Console.WriteLine($"[ERROR]: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}