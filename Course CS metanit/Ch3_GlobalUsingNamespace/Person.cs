using Base;

class Person
{
  string name;
  Company company;
  public Person(string name, Company company)
  {
    this.name = name;
    this.company = company;
  }
  public void Print(out string s)
  {
    s = $"Имя: {name}.";
    s += " ";
    company.Print(ref s);
  }
  }
