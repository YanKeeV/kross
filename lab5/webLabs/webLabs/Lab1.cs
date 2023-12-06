namespace webLabs
{
    public class Lab1
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
    }
}