using System;
using System.Collections.Generic;
using System.Linq; // For LINQ

// 1. ABSTRACTION & INTERFACE
// This defines what every "Item" in our warehouse must have.
public interface IStorable
{
    string Name { get; }
    decimal Price { get; }
}

// 2. ENCAPSULATION & CLASS
// We protect the data by using properties.
public class Product : IStorable
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public Product(string name, decimal price, int stock)
    {
        Name = name;
        Price = price;
        Stock = stock;
    }
}

class Warehouse
{
    // 3. COLLECTIONS & GENERICS
    // We use a List to store many Products.
    private List<Product> _inventory = new List<Product>();

    public void AddToInventory(Product prod)
    {
        // 4. GUARD CLAUSE (Basic Logic)
        if (prod.Price < 0) throw new Exception("Price cannot be negative!");
        
        _inventory.Add(prod);
        Console.WriteLine($"Added: {prod.Name}");
    }

    public void ShowStock()
    {
        Console.WriteLine("\n--- Current Inventory ---");
        
        // 5. LINQ (Advanced Querying)
        // We filter the list to find items that are low on stock.
        var lowStock = _inventory.Where(p => p.Stock < 5).ToList();

        foreach (var item in _inventory)
        {
            Console.WriteLine($"{item.Name} - ${item.Price} (Qty: {item.Stock})");
        }

        if (lowStock.Any())
        {
            Console.WriteLine(">> ALERT: Some items are low on stock!");
        }
    }
}

class Program
{
    static void Main()
    {
        // 6. OBJECTS & EXCEPTION HANDLING
        Warehouse myWarehouse = new Warehouse();

        try
        {
            // Creating objects and adding to the collection
            myWarehouse.AddToInventory(new Product("Laptop", 1200.00m, 10));
            myWarehouse.AddToInventory(new Product("Mouse", 25.50m, 2)); // Low stock
            
            // This will trigger the Exception logic
            // myWarehouse.AddToInventory(new Product("Broken Item", -10m, 1)); 

            myWarehouse.ShowStock();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"System Error: {ex.Message}");
        }
    }
}