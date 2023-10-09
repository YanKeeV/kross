using System;
using System.IO;
using System.Linq;

class Program
{
    // Функція для обчислення елемента aij
    static long CalculateAij(long[][] matrix, int i, int j, long r)
    {
        long sumAbove = 0;
        int newj = 0;
        int x = i;

        for(int row =  1; row <= i; row++)
        {
            //Console.WriteLine(" | row = " + row + " | ");
            //Console.WriteLine(" | i = " + i + " | ");
            int a = (1 + row) - (x + 2);
            for(int col = a; col <= Math.Abs(a); col++)
            {
                newj = j + col;
               // Console.Write(" | newj = " + newj + " | ");
                if(newj >= 0 && newj <= matrix.Length)
                {
                    sumAbove += matrix[row - 1][newj];
                }

            }
        }

        return sumAbove % r;
    }

    static void Main()
    {
        using (StreamReader reader = new StreamReader("C:\\Users\\Nazar\\source\\repos\\lab2\\INPUT.TXT"))
        using (StreamWriter writer = new StreamWriter("C:\\Users\\Nazar\\source\\repos\\lab2\\OUTPUT.TXT"))
        {
            string[] input = reader.ReadLine().Split();
            int n = int.Parse(input[0]);
            Console.WriteLine("n - " + n);
            int m = int.Parse(input[1]);
            Console.WriteLine("m - " + m);
            long r = long.Parse(input[2]);
            Console.WriteLine("r - " + r);

            long[][] table = new long[n][];
            table[0] = reader.ReadLine().Split().Select(long.Parse).ToArray();
            for (int col = 0; col < m; col++)
            {
                Console.Write(" " + table[0][col]);
            }

            Console.WriteLine();

            for (int i = 1; i < n; i++)
            {
                table[i] = new long[m];
                Console.Write("iteration - " + i + "\n");
                for (int j = 0; j < m; j++)
                {
                    table[i][j] = CalculateAij(table, i, j, r);
                    Console.Write(" " + table[i][j]);
                }
                Console.Write("\n");
            }

            foreach (long num in table[n - 1])
            {
                writer.Write(num + " ");
            }
        }
    }
}

