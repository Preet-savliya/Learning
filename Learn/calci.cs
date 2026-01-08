using System;
using System.Collections.Generic;

// 1. abstraction: Defines the blueprint for any operation
public abstract class Operation
{
    public abstract string Symbol { get; }
    public abstract double Execute(double a, double b);
}


public class Add : Operation
{
    public override string Symbol => "+";
    public override double Execute(double a, double b) => a + b;
}

public class Divide : Operation
{
    public override string Symbol => "/";
    public override double Execute(double a, double b)
    {
        // 3. EXCEPTION HANDLING: Guarding against logic errors
        if (b == 0) throw new DivideByZeroException("Cannot divide by zero!");
        return a / b;
    }
}

public class Multiply : Operation
{
    public override string Symbol => "*";

    public override double Execute(double a, double b)
    {
        return a * b;
    }
}

public class Minus : Operation
{
    public override string Symbol => "-";

    public override double Execute(double a, double b)
    {
        return a - b;
    }
}

class Program
{
    static void Main()
    {
        // 4. COLLECTIONS & GENERICS: Storing our operations in a Dictionary
        // Key is the string (e.g., "+"), Value is the Class logic.
        Dictionary<string, Operation> calculatorMenu = new Dictionary<string, Operation>
{
    { "+", new Add() },
    { "/", new Divide() },
    { "*", new Multiply() },
    { "-", new Minus() }
};


        try
        {
            Console.WriteLine("Enter Number 1:");
            double n1 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter Operation (+ , /, - or *):");
            string op = Console.ReadLine();

            Console.WriteLine("Enter Number 2:");
            double n2 = Convert.ToDouble(Console.ReadLine());

            // 5. LOGIC: Finding the right operation from the collection
            if (calculatorMenu.ContainsKey(op))
            {
                var result = calculatorMenu[op].Execute(n1, n2);
                Console.WriteLine($"Result: {result}");
            }
            else
            {
                Console.WriteLine("Operation not supported.");
            }
        }
        catch (Exception ex)
        {
            // 6. LOGGING (Concept): Catching any user input errors
            Console.WriteLine($"[LOG]: An error occurred: {ex.Message}");
        }
    }
}