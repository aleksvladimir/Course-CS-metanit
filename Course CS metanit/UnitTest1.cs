using NUnit.Framework;
using System;

namespace Tests
{
  public class TestsArrays
  {
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestThreeDimensionalArray()
    {
      // строка - измерение/rank 1, колонка - измерение/rank 2, €чейка колонки - измерение/rank 3
      int[,,] mas = {
// колонка:   0          1           2           3
        { { 1, 2 },   { 3, 4 },   { 5, 6 },   { 7, 8 }   }, // строка 0
        { { 9, 10 },  { 11, 12 }, { 13, 14 }, { 15, 16 } }, // строка 1
        { { 17, 18 }, { 19, 20 }, { 21, 22 }, { 23, 24 } }, // строка 2
        { { 25, 26 }, { 27, 28 }, { 29, 30 }, { 31, 32 } }, // строка 3
        { { 33, 34 }, { 35, 36 }, { 37, 38 }, { 39, 40 } }, // строка 4
        { { 41, 42 }, { 43, 44 }, { 45, 46 }, { 47, 48 } }  // строка 5
      };

      int rows = mas.GetUpperBound(0) + 1;        // количество строк (5+1=6)
      int numbersCountPerRow = mas.Length / rows; // количество чисел в 1 строке (48/6=8)
      int columns = mas.GetUpperBound(1) + 1;     // количество колонок (3+1=4)
      int cells = mas.GetUpperBound(2) + 1;       // количество €чеек в колонке (1+1=2)

      Assert.AreEqual(6, rows);
      Assert.AreEqual(8, numbersCountPerRow);
      Assert.AreEqual(4, columns);
      Assert.AreEqual(2, cells);

      string res = "";
      res += '{';
      for (int i = 0; i < rows; i++)
      {
        res += '{';
        for (int j = 0; j < columns; j++)
        {
          res += '{';
          for (int c = 0; c < cells; c++)
          {
            res += String.Format("{0}", mas[i, j, c]);
            if (c + 1 != cells)
              res += ','; // €чейка не последн€€
          }
          res += '}';
          if (j + 1 != columns)
            res += ','; // €чейка не последн€€
        }
        res += '}';
        if (i + 1 != rows)
          res += ','; // €чейка не последн€€
      }
      res += '}';
      string expected = "{{{1,2},{3,4},{5,6},{7,8}}," +
                         "{{9,10},{11,12},{13,14},{15,16}}," +
                         "{{17,18},{19,20},{21,22},{23,24}}," +
                         "{{25,26},{27,28},{29,30},{31,32}}," +
                         "{{33,34},{35,36},{37,38},{39,40}}," +
                         "{{41,42},{43,44},{45,46},{47,48}}}";
      Assert.AreEqual(expected, res);
    }

    [Test]
    public void TestPositiveNumbersFromArray()
    {
      int[] numbers = { -4, -3, -2, -1, 0, 1, 2, 3, 4 };
      int res = 0;
      foreach(var num in numbers)
      {
        if (num > 0)
          res++;
      }
      Assert.AreEqual(4, res);
    }

    [Test]
    public void TestInversionArray()
    {
      // ¬иды делегатов:
      // Func<T,T> - возвращает значение через return
      // Action<T> - модифицирует объект
      Action<int[]> revers = (int[] arr) =>
      {
        int tmp;
        for (int b = 0, m = arr.Length/2; b < m; b++)
        {
          tmp = arr[b];
          arr[b] = arr[arr.Length - 1 - b];
          arr[arr.Length - 1 - b] = tmp;
        }
      };

      int[] numbers = { };
      int[] expected = { };
      revers(numbers);
      Assert.AreEqual(expected, numbers);

      numbers = new int[] { 1 };
      expected = new int[] { 1 };
      revers(numbers);
      Assert.AreEqual(expected, numbers);

      numbers = new int[] { 1, 2 };
      expected = new int[] { 2, 1 };
      revers(numbers);
      Assert.AreEqual(expected, numbers);

      numbers = new int[] { 1, 2, 3 };
      expected = new int[] { 3, 2, 1 };
      revers(numbers);
      Assert.AreEqual(expected, numbers);

      numbers = new int[] { -4, -3, -2, -1, 0, 1, 2, 3, 4 };
      expected = new int[] { 4, 3, 2, 1, 0, -1, -2, -3, -4 };
      revers(numbers);
      Assert.AreEqual(expected, numbers);
    }

    #region[TestRefOutIn Impl]
    public void Increment(int n)
    {
      //копи€
      n++;
    }
    public void Increment(ref int n)
    {
      //ref - &
      n++;
    }
    public void Increment(in byte n)
    {
      //in - const&
      //n++; - error!
    }
    public void Increment(in int n, out int incr1, out int incr2)
    {
      incr1 = n + 1;  // как return
      incr2 = n + 2;  // как return
    }
    #endregion
    [Test]
    public void TestRefOutIn()
    {
      int number = 1;
      Increment(number);
      Assert.AreEqual(1, number);

      Increment(ref number);
      Assert.AreEqual(2, number);

      byte n = 1;
      Increment(in n);
      Assert.AreEqual(1, n);

      number = 1;
      Increment(in number, out int incr1, out int incr2);
      Assert.AreEqual(2, incr1);
      Assert.AreEqual(3, incr2);
      Increment(in incr2, out int incr3, out int incr4);
      Assert.AreEqual(4, incr3);
      Assert.AreEqual(5, incr4);
    }

    #region[TestKeywordParams Impl]
    public int Sum(int initVal, params int[] numbers)
    {
      int res = initVal;
      foreach (var item in numbers)
      {
        res += item;
      }
      return res;
    }
    #endregion
    [Test]
    public void TestKeywordParams()
    {
      Assert.AreEqual(0, Sum(0));
      Assert.AreEqual(5, Sum(5, new int[] { }));
      Assert.AreEqual(6, Sum(0, new int[] { 1, 2, 3 }));
    }
  }
}