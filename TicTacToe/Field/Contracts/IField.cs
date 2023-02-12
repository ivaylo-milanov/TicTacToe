namespace TicTacToe.Field.Contracts
{
    public interface IField
    {
        int Rows { get; }
        int Cols { get; }
        void DrawEmptyField();
        bool IsWalkable(int newX, int newY);
        void Add(int x, int y, string symbol);
        bool IsWinning(string v);
        string GetElementAt(int x, int y);
        void Clear();
    }
}
