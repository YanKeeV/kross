namespace ClassLibrary1
{
    public class Class1
    {
        public void task1(string inputFilePath, string outputFileName)
        {
            int N;
            int[][] table;

            // Зчитуємо вхідні дані з файлу INPUT.TXT
            using (StreamReader reader = new StreamReader(inputFilePath))
            {
                N = int.Parse(reader.ReadLine());
                table = new int[N][];
                for (int i = 0; i < N; i++)
                {
                    string[] line = reader.ReadLine().Split();
                    int Ai = int.Parse(line[0]);
                    int Bi = int.Parse(line[1]);
                    table[i] = new int[] { Ai, Bi };
                }
            }

            // Обчислюємо вартість гри
            int result = CalculateGameValue(N, table);

            // Записуємо результат у файл OUTPUT.TXT
            using (StreamWriter writer = new StreamWriter(outputFileName))
            {
                writer.WriteLine(result);
            }
        }

        static int CalculateGameValue(int N, int[][] table)
        {
            int[] player1Scores = new int[N];
            int[] player2Scores = new int[N];

            // Обчислюємо суми очок для кожного стовпця таблиці
            for (int i = 0; i < N; i++)
            {
                player1Scores[i] = table[i][0];
                player2Scores[i] = table[i][1];
            }

            // Застосовуємо динамічне програмування для знаходження оптимальних сум очок
            for (int i = 1; i < N; i++)
            {
                player1Scores[i] += Math.Max(player1Scores[i - 1], player2Scores[i - 1]);
                player2Scores[i] += Math.Max(player1Scores[i - 1], player2Scores[i - 1]);
            }

            // Знаходимо вартість гри
            int gameValue = player1Scores[N - 1] - player2Scores[N - 1];

            return gameValue;
        }

        static long CalculateAij(long[][] matrix, int i, int j, long r)
        {
            long sumAbove = 0;
            int newj = 0;
            int x = i;

            for (int row = 1; row <= i; row++)
            {
                //Console.WriteLine(" | row = " + row + " | ");
                //Console.WriteLine(" | i = " + i + " | ");
                int a = (1 + row) - (x + 2);
                for (int col = a; col <= Math.Abs(a); col++)
                {
                    newj = j + col;
                    // Console.Write(" | newj = " + newj + " | ");
                    if (newj >= 0 && newj <= matrix.Length)
                    {
                        sumAbove += matrix[row - 1][newj];
                    }

                }
            }

            return sumAbove % r;
        }

        public void task2(string inputFilePath, string outputFileName)
        {
            using (StreamReader reader = new StreamReader(inputFilePath))
            using (StreamWriter writer = new StreamWriter(outputFileName))
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

        public void task3(string inputFilePath, string outputFileName)
        {
            // Читаємо вхідні дані з файлу INPUT.TXT
            Console.WriteLine("Start");
            string[] inputLines = File.ReadAllLines(inputFilePath);
            int N = int.Parse(inputLines[0]);
            char[][] grid = new char[N][];

            // Заповнюємо сітку
            for (int i = 0; i < N; i++)
            {
                grid[i] = inputLines[i + 1].ToCharArray();
            }

            // Знаходимо початкову позицію (позицію кульки @)
            int startX = -1, startY = -1;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (grid[i][j] == '@')
                    {
                        startX = i;
                        Console.WriteLine(startX);
                        startY = j;
                        Console.WriteLine(startY);
                        break;
                    }
                }
                if (startX != -1)
                    break;
            }

            // Знаходимо кінцеву позицію (позицію кульки X)
            int endX = -1, endY = -1;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (grid[i][j] == 'X')
                    {
                        endX = i;
                        Console.WriteLine(endX);
                        endY = j;
                        Console.WriteLine(endY);
                        break;
                    }
                }
                if (endX != -1)
                    break;
            }

            // Викликаємо функцію для знаходження шляху
            if (FindPath(grid, startX, startY, endX, endY))
            {
                // Виводимо результат в файл OUTPUT.TXT
                File.WriteAllText(outputFileName, "Y\n");
                for (int i = 0; i < N; i++)
                {
                    File.AppendAllText(outputFileName, new string(grid[i]) + "\n");
                }
            }
            else
            {
                File.WriteAllText(outputFileName, "N\n");
            }
        }

        // Функція для знаходження шляху
        static bool FindPath(char[][] grid, int x, int y, int endX, int endY)
        {


            // Масив для зміни напрямку руху (вгору, вниз, вліво, вправо)
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            Console.WriteLine("Start path x " + x);
            Console.WriteLine("Start path y " + y);

            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    Console.WriteLine("up");
                }
                if (i == 1)
                {
                    Console.WriteLine("down");
                }
                if (i == 2)
                {
                    Console.WriteLine("left");
                }
                if (i == 3)
                {
                    Console.WriteLine("right");
                }

                int newX = x + dx[i];
                int newY = y + dy[i];

                Console.WriteLine("Curr X - " + newX);
                Console.WriteLine("Curr Y - " + newY);

                // Перевірка, чи можливо рухнутися на кінцеву позицію
                if (newX == endX && newY == endY)
                {
                    Console.WriteLine("X found");
                    return true;
                }

                // Перевірка, чи нова позиція в межах сітки і є вільною
                if (IsValid(grid, newX, newY))
                {
                    // Змінюємо позицію кульки на плюс
                    grid[newX][newY] = '+';

                    for (int j = 0; j < 5; j++)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            Console.Write(grid[j][k]);
                        }
                        Console.WriteLine("\n");
                    }

                    // Рекурсивно викликаємо функцію для нової позиції
                    if (FindPath(grid, newX, newY, endX, endY))
                    {
                        return true;
                    }

                    // Якщо не знайшли шлях, повертаємо позицію назад
                    grid[newX][newY] = '.';
                }
            }

            return false;
        }

        // Перевірка, чи позиція в межах сітки і є вільною
        static bool IsValid(char[][] grid, int x, int y)
        {
            int N = grid.Length;
            return x >= 0 && x < N && y >= 0 && y < N && grid[x][y] == '.';
        }
    }
}