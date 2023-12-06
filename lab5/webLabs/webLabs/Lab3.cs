namespace webLabs
{
    public static class Lab3
    {
        public static void task3(string inputFilePath, string outputFileName)
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

