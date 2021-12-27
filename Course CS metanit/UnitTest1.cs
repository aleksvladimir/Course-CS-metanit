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
      // ������ - ���������/rank 1, ������� - ���������/rank 2, ������ ������� - ���������/rank 3
      int[,,] mas = {
// �������:   0          1           2           3
        { { 1, 2 },   { 3, 4 },   { 5, 6 },   { 7, 8 }   }, // ������ 0
        { { 9, 10 },  { 11, 12 }, { 13, 14 }, { 15, 16 } }, // ������ 1
        { { 17, 18 }, { 19, 20 }, { 21, 22 }, { 23, 24 } }, // ������ 2
        { { 25, 26 }, { 27, 28 }, { 29, 30 }, { 31, 32 } }, // ������ 3
        { { 33, 34 }, { 35, 36 }, { 37, 38 }, { 39, 40 } }, // ������ 4
        { { 41, 42 }, { 43, 44 }, { 45, 46 }, { 47, 48 } }  // ������ 5
      };

      int rows = mas.GetUpperBound(0) + 1;        // ���������� ����� (5+1=6)
      int numbersCountPerRow = mas.Length / rows; // ���������� ����� � 1 ������ (48/6=8)
      int columns = mas.GetUpperBound(1) + 1;     // ���������� ������� (3+1=4)
      int cells = mas.GetUpperBound(2) + 1;       // ���������� ����� � ������� (1+1=2)

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
              res += ','; // ������ �� ���������
          }
          res += '}';
          if (j + 1 != columns)
            res += ','; // ������ �� ���������
        }
        res += '}';
        if (i + 1 != rows)
          res += ','; // ������ �� ���������
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

  }
}