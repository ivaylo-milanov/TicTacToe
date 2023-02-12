namespace Parser
{
    public class FieldParser
    {
        public static string[,] GetField(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int rows = lines.Length;
            int cols = lines[0].Length;
            string[,] field = new string[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                string line = lines[i];
                for (int h = 0; h < cols; h++)
                {
                    field[i, h] = line[h].ToString();
                }
            }

            return field;
        }
    }
}
