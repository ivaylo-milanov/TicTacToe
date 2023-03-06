namespace TicTacToe.Field.Contracts
{
    using System.Data;

    public interface IField
    {
        bool DrawSymbol(string symbol);

        void Update(int x, int y);

        void SetOnRandomCell();

        void DrawEmptyField();
    }
}
