namespace TicTacToe.Core
{
    using static System.Console;
    using Field;
    using Field.Contracts;
    using Contracts;

    public class Game : IGame
    {
        private IField field = null!;

        public void Start(bool started = false)
        {
            if (!started) DisplayIntro();

            ReadyGame();
            GameLoop();
        }

        private void GameLoop()
        {
            while (true)
            {
                PlayerMove();
                ComputerMove();
            }
        }

        private void Move(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.A:
                    this.field.Update(0, -2);
                    break;
                case ConsoleKey.W:
                    this.field.Update(-2, 0);
                    break;
                case ConsoleKey.D:
                    this.field.Update(0, 2);
                    break;
                case ConsoleKey.S:
                    this.field.Update(2, 0);
                    break;
            }
        }

        private void PlayerMove()
        {
            ConsoleKey key = default;
            while (true)
            {
                key = GetKey();
                if (key == ConsoleKey.Spacebar) break;
                Move(key);
            }
            Turn("X");
        }

        private void WaitForKey(ConsoleKey certainKey)
        {
            while (GetKey() != certainKey)
            {
            }
        }

        private void ComputerMove()
        {
            this.field.SetOnRandomCell();
            Turn("O");
        }

        private ConsoleKey GetKey() => ReadKey(true).Key;

        private void Turn(string symbol)
        {
            bool isWinning = this.field.DrawSymbol(symbol);
            if (isWinning) DisplayOutro(symbol);
        }

        private void DisplayIntro()
        {
            Clear();
            WriteLine("This is TicTacToe Game by Ivaylo Milanov");
            WriteLine("Spacebar - Draw symbol");
            WriteLine("W - move up");
            WriteLine("S - move down");
            WriteLine("A - move left");
            WriteLine("D - move right");
            WriteLine("R - restart the game");
            WriteLine("F - start the game");
            WaitForKey(ConsoleKey.F);
        }

        private void DisplayOutro(string winner)
        {
            Clear();
            WriteLine($"The winner is {winner}");
            WriteLine("Press R to restart the game");
            WaitForKey(ConsoleKey.R);
            Start(true);
        }

        private void ReadyGame()
        {
            Clear();
            this.field = new Field();
        }
    }
}
