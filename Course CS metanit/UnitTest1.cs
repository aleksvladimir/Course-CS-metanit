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
                         "{{25,26},{27,28},{29,30},{31,32}},"+
                         "{{33,34},{35,36},{37,38},{39,40}},"+
                         "{{41,42},{43,44},{45,46},{47,48}}}";
      Assert.AreEqual(expected, res);
    }                  
  }
}