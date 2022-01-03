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
      Person tom = new Person() { name = "Tom", company = { title = "Microsoft" } };
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
    class Person
    {
      public string name;
      public Company company;
      public Person()
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

  }
}
