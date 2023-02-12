namespace TicTacToe.Field
{
    using static System.Console;
    using Contracts;

    public class Field : IField
    {
        private string[,] field;
        private int rows;
        private int cols;

        public Field(string[,] field)
        {
            this.field = field;
            rows = field.GetLength(0);
            cols = field.GetLength(1);
        }

        public int Rows
        {
            get 
            {
                return rows;
            }
        }

        public int Cols
        {
            get 
            { 
                return cols;
            }
        }

        public void DrawEmptyField()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    SetCursorPosition(j, i);
                    Write(this.field[i, j]);
                }
            }
        }

        public bool IsWalkable(int newX, int newY)
        {
            return newX >= 0 && newX < Rows
                && newY >= 0 && newY < Cols;
        }

        public void Add(int x, int y, string symbol)
        {
            this.field[x, y] = symbol;
        }

        public bool IsWinning(string symbol)
        {
            for ( int i = 1; i < Rows - 1; i += 2)
            {
                if (this.field[i, 1] == symbol && this.field[i, 3] == symbol && this.field[i, 5] == symbol)
                {
                    return true;
                }
            }

            for (int i = 1; i < Cols - 1; i += 2)
            {
                if (this.field[1, i] == symbol && this.field[3, i] == symbol && this.field[5, i] == symbol)
                {
                    return true;
                }
            }

            if (this.field[1, 1] == symbol && this.field[3, 3] == symbol && this.field[5, 5] == symbol)
            {
                return true;
            }

            if (this.field[1, 5] == symbol && this.field[3, 3] == symbol && this.field[5, 1] == symbol)
            {
                return true;
            }

            return false;
        }

        public string GetElementAt(int x, int y)
        {
            return this.field[x, y];
        }

        public void Clear()
        {
            for (int i = 1; i < Rows - 1; i += 2)
            {
                for (int j = 1; j < Cols - 1; j += 2)
                {
                    this.field[i, j] = " ";
                }
            }
        }
    }
}
