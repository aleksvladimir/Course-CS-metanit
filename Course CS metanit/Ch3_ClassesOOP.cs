using NUnit.Framework;
using System;

namespace Tests
{
  public class Tests_Chapter3_ClassesOOP
  {
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestObjectInitializers()
    {
      PersonClass tom = new PersonClass() { name = "Tom", company = { title = "Microsoft" } };
      Assert.AreEqual("Tom", tom.name);
      Assert.AreEqual("Microsoft", tom.company.title);

      tom.Deconstruct(out string name, out Company company); //Deconstruct
      Assert.AreEqual("Tom", name);
      Assert.AreEqual("Microsoft", company.title);

      (string localName, Company localCompany) = tom; //Deconstruct
      Assert.AreEqual("Tom", localName);
      Assert.AreEqual("Microsoft", localCompany.title);

      (_, Company c) = tom; //Deconstruct (need only company)
      Assert.AreEqual("Microsoft", c.title);
    }
    #region[TestObjectInitializers]
    class PersonClass
    {
      public string name;
      public Company company;
      //public PersonClass(string name, Company company) // cannot have default constructor if user create ownered constructor with params and not define constructor PersonClass()!!!
      //{
      //}
      public PersonClass()
      {
        name = "Undefined";
        company = new Company();
      }
      public void Print() => Console.WriteLine($"Имя: {name}  Компания: {company.title}");

      public void Deconstruct(out string personName, out Company personCompany)
      {
        personName = name;
        personCompany = company;
      }
    }
    class Company
    {
      public string title = "Unknown";
    }
    #endregion

    [Test]
    public void TestCreateStruct()
    {
      PersonStruct person1 = new PersonStruct(); // can have default constructor even if user create ownered constructor
      PersonStruct person2 = new PersonStruct("",0);

      PersonStruct person3;         // can 
      person3.name = "";            // call default constructor
      Console.Write(person3.name);  // but if default constructor is not called there will be an error CS0170: Use of possibly unassigned field 'name'!!!

      PersonClass person4;          // can create null reference on object class, but...
      //person4.name = "";          // error CS0165: Use of unassigned local variable 'person4'!!!

      // initializators
      PersonStruct person5 = new PersonStruct { age = 2, name = "" };
      PersonStruct person6 = new PersonStruct() { age = 2, name = "" };
      PersonStruct person7 = new PersonStruct("", 0) { age = 2, name = "" };

      // with start with C#9.0
      //PersonStruct person8 = person5 with { age = 3 };
    }
    struct PersonStruct
    {
      public string name; //  = "" - Cannot have field initializers in struct!!! Starts with C#10.
      public int age;

      //public PersonStruct() // Cannot contain explicit parameterless constructors!!! Starts with C#10.
      //{
      //  this.name = "";
      //  this.age = 0;
      //}

      public PersonStruct(string name, int age)
      {
        this.name = name;
        this.age = age;
      }
    }
  }
}
