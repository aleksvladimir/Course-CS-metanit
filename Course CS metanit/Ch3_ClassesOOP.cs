using NUnit.Framework;
using System;
using Base;
// global using Base; // using in .net 6.0

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

      tom.Deconstruct(out string name, out CompanyClass company); //Deconstruct
      Assert.AreEqual("Tom", name);
      Assert.AreEqual("Microsoft", company.title);

      (string localName, CompanyClass localCompany) = tom; //Deconstruct
      Assert.AreEqual("Tom", localName);
      Assert.AreEqual("Microsoft", localCompany.title);

      (_, CompanyClass c) = tom; //Deconstruct (need only company)
      Assert.AreEqual("Microsoft", c.title);
    }
    #region[TestObjectInitializers]
    class PersonClass
    {
      public string name;
      public CompanyClass company;
      //public PersonClass(string name, Company company) // cannot have default constructor if user create ownered constructor with params and not define constructor PersonClass()!!!
      //{
      //}
      public PersonClass()
      {
        name = "Undefined";
        company = new CompanyClass();
      }
      public void Print() => Console.WriteLine($"Имя: {name}  Компания: {company.title}");

      public void Deconstruct(out string personName, out CompanyClass personCompany)
      {
        personName = name;
        personCompany = company;
      }
    }
    class CompanyClass
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
    #region[TestCreateStruct]
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
    #endregion

    [Test]
    public void TestValTypeVsRefType()
    {
      // Reference types inside value types:

      State state1 = new State();
      State state2 = new State();

      state2.country = new Country();
      state2.x = 5;
      state2.country.x = 5;
      state1 = state2;
      state2.x = 8;         // change value in variable with value type
      state2.country.x = 8; // change value in variable with reference type

      Assert.AreEqual(8, state1.country.x); //8
      Assert.AreEqual(8, state2.country.x); //8

      Assert.AreEqual(5, state1.x); //5!
      Assert.AreEqual(8, state2.x); //8!
    }
    #region[TestValTypeVsRefType]
    struct State
    {
      public int x;
      public int y;
      public Country country; // reference type
    }
    class Country
    {
      public int x;
      public int y;
    }
    #endregion

    /*
      Сравнение передачи объектов классов как параметры методов (для языков С++ и С#)

      //---------------------------------------------------------------------------------
      
      //C++ (Передача копии указателя)        | C# (Передача копии ссылки)
                                              
      Person *p1 = new Person();              | Person p1 = new Person();
      ChangePerson(p1);                       
                                              
      void ChangePerson(Person *p2)           | void ChangePerson(Person p2) 
      {
        //p2 указывает на p1
        p2 = new Person();
        //p2 указывает на новую память в куче | //p2 указывает на новую память в куче если ссылочный тип, иначе в стеке
        p2->x = 2;                            | p2.x = 2;
        // p2 изменилось, p1 осталось неизменным
      }

      //---------------------------------------------------------------------------------
      // Передача ссылок

      //C++ (Передача ссылки указателя)       | C# (Передача ссылки)

      Person *p1 = new Person();              | Person p1 = new Person();
      ChangePerson(p1);                       | ChangePerson(ref p1);

      void ChangePerson(Person &*p1)          | void ChangePerson(ref Person p1)
      {
        p1 = new Person();
        //p1 указывает на новую память в куче | //p1 указывает на новую память в куче если ссылочный тип, иначе в стеке
        p1->x = 2;                            | p1.x = 2;
        // p1 изменилось
        // утечка памяти	                    | // сборщик мусора сам освободит неиспользуемую память в куче если это ссылочный тип
      }

     */

    [Test]
    public void TestGlobalNamespace()
    {
      Company microsoftBaseCompany = new Company("Microsoft");  // class Company has namespace Base
      Person tom = new Person("Tom", microsoftBaseCompany);     // class Person doesn`t have namespace
      tom.Print(out string s);
      Assert.AreEqual("Имя: Tom. Компания: Microsoft.", s);
    }
  }
}
