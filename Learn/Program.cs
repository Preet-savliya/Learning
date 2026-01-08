using System;

// 1. ABSTRACTION: A "Person" is a general idea. 
// We mark it 'abstract' because you can't be "just a person" in this system; 
// you must be either a Student or a Teacher.
abstract class Person
{
    // ENCAPSULATION: Properties protect the data
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    // POLYMORPHISM: Every person "Introduces" themselves, 
    // but they do it differently.
    public abstract void Introduce();
}

// 2. INHERITANCE: Student 'is a' Person
class Student : Person
{
    public string Grade { get; set; }

    // 'base' calls the Person constructor
    public Student(string name, int age, string grade) : base(name, age)
    {
        Grade = grade;
    }

    public override void Introduce()
    {
        Console.WriteLine($"I am {Name}, a student in grade {Grade}.");
    }
}

// 2. INHERITANCE: Teacher 'is a' Person
class Teacher : Person
{
    public string Subject { get; set; }

    public Teacher(string name, int age, string subject) : base(name, age)
    {
        Subject = subject;
    }

    public override void Introduce()
    {
        Console.WriteLine($"I am Prof. {Name}, and I teach {Subject}.");
    }
}

class Program
{
    static void Main()
    {
        // 3. OBJECTS: Creating specific instances
        Person person1 = new Student("Surat", 22, "A+");
        Person person2 = new Teacher("Mr. Smith", 45, "C# Programming");

        // 4. POLYMORPHISM in action: 
        // We treat them both as 'Person', but they use their own Introduce() logic.
        person1.Introduce();
        person2.Introduce();
    }
}