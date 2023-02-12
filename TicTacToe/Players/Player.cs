namespace TicTacToe.Players
{
    using static System.Console;
    using Contracts;

    public abstract class Player : IPlayer
    {
        private string symbol;

        protected Player(string symbol)
        {
            this.symbol = symbol;
        }

        public void DrawSymbol()
        {
            Write(this.symbol);
        }

        public string GetSymbol()
        {
            return this.symbol;
        }
    }
}
