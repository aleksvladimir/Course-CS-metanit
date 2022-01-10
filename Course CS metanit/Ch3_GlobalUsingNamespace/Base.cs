namespace Base
{
  class Company
  {
    string title;
    public Company(string title) => this.title = title;
    public void Print(ref string s) => s = s + $"Компания: {title}.";
  }
}
