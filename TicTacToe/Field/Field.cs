namespace TicTacToe.Field
{
    using static System.Console;
    using Contracts;
    using Parser;

    public class Field : IField
    {
        private string[,] field;
        private int rows;
        private int cols;
        private int x = 1;
        private int y = 1;

        public Field()
        {
            this.field = FieldParser.GetField("../../../Field/Parser/FieldFile.txt");
            this.rows = field.GetLength(0);
            this.cols = field.GetLength(1);
            DrawEmptyField();
        }

        private void DrawEmptyField()
        {
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    SetCursorPosition(j, i);
                    Write(this.field[i, j]);
                }
            }

            SetCursorPosition(this.x, this.y);
        }

        private bool IsWalkable(int newX, int newY) =>
            newX >= 1 && newX < this.rows - 1 &&
            newY >= 1 && newY < this.cols - 1;

        private void Add(string symbol) => this.field[this.x, this.y] = symbol;
        
        private bool IsWinning(string symbol) =>
            (this.field[1, 1] == symbol && this.field[1, 3] == symbol && this.field[1, 5] == symbol) ||
            (this.field[3, 1] == symbol && this.field[3, 3] == symbol && this.field[3, 5] == symbol) ||
            (this.field[5, 1] == symbol && this.field[5, 3] == symbol && this.field[5, 5] == symbol) ||
            (this.field[1, 1] == symbol && this.field[3, 1] == symbol && this.field[5, 1] == symbol) ||
            (this.field[1, 3] == symbol && this.field[3, 3] == symbol && this.field[5, 3] == symbol) ||
            (this.field[1, 5] == symbol && this.field[3, 5] == symbol && this.field[5, 5] == symbol) ||
            (this.field[1, 1] == symbol && this.field[3, 3] == symbol && this.field[5, 5] == symbol) ||
            (this.field[1, 5] == symbol && this.field[3, 3] == symbol && this.field[5, 1] == symbol);

        public bool DrawSymbol(string symbol)
        {
            string currSymbol = this.field[this.x, this.y];
            if (currSymbol == "X" || currSymbol == "O") return false;
            
            Write(symbol);
            Add(symbol);
            SetCursorPosition(this.y, this.x);

            return this.IsWinning(symbol);
        }

        public void Update(int x, int y)
        {
            int newX = x + this.x;
            int newY = y + this.y;

            if (IsWalkable(newX, newY))
            {
                this.x = newX;
                this.y = newY;
            }

            SetCursorPosition(newY, newX);
        }

        public void SetOnRandomCell()
        {
            Random random = new Random();
            List<(int x, int y)> cells = new List<(int x, int y)>();
            for (int i = 1; i < this.rows - 1; i += 2)
            {
                for (int j = 1; j < this.cols - 1; j += 2)
                {
                    if (this.field[i, j] == " ")
                    {
                        cells.Add((i, j));
                    }
                }
            }

            int index = random.Next(0, cells.Count);

            (this.x, this.y) = cells[index];
            SetCursorPosition(this.y, this.x);
        }
    }
}
