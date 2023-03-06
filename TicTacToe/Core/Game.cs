namespace TicTacToe.Core
{
    using static System.Console;
    using Field;
    using Field.Contracts;
    using Contracts;
    using EventListener;

    public class Game : IGame
    {
        private IField field = null!;
        private IListener listener = null!;

        public void Start(bool started = false)
        {
            Clear();
            ReadyGame();
            if (!started) DisplayIntro();

            GameLoop();
        }

        private void GameLoop()
        {
            this.field.DrawEmptyField();
            while (true)
            {
                PlayerMove();
                ComputerMove();
            }
        }

        private void PlayerMove()
        {
            while (true)
            {
                ConsoleKey key = this.listener.GetKey();
                this.listener.KeyListener(key);

                if (key == ConsoleKey.Spacebar) break;
            }
        }
        private void ComputerMove()
        {
            this.field.SetOnRandomCell();
            Turn("O");
        }

        private void Turn(string symbol)
        {
            bool isWinning = this.field.DrawSymbol(symbol);
            if (isWinning)
            {
                Clear();
                DisplayOutro(symbol);
            }
        }

        private void DisplayIntro()
        {
            WriteLine("This is TicTacToe Game by Ivaylo Milanov");
            WriteLine("Spacebar - Draw symbol");
            WriteLine("W - move up");
            WriteLine("S - move down");
            WriteLine("A - move left");
            WriteLine("D - move right");
            WriteLine("R - restart the game");
            WriteLine("F - start the game");
            this.listener.KeyListener(ConsoleKey.F, true);
            Clear();
        }

        private void DisplayOutro(string winner)
        {
            WriteLine($"The winner is {winner}");
            WriteLine("Press R to restart the game");
            this.listener.KeyListener(ConsoleKey.R, true);
        }

        private void ReadyGame()
        {
            this.listener = new Listener();
            this.field = new Field();
            AssignAllKeys();
        }

        private void AssignAllKeys()
        {
            this.listener.Assign(ConsoleKey.F);
            this.listener.Assign(ConsoleKey.R, () => this.Start(true));
            this.listener.Assign(ConsoleKey.A, () => this.field.Update(0, -2));
            this.listener.Assign(ConsoleKey.D, () => this.field.Update(0, 2));
            this.listener.Assign(ConsoleKey.W, () => this.field.Update(-2, 0));
            this.listener.Assign(ConsoleKey.S, () => this.field.Update(2, 0));
            this.listener.Assign(ConsoleKey.Spacebar, () => Turn("X"));
        }
    }
}
