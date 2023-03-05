namespace TicTacToe.Core
{
    using static System.Console;
    using Field;
    using Field.Contracts;
    using Contracts;

    public class Game : IGame
    {
        private IField field = null!;
        string player1 = null!;
        string player2 = null!;
        private bool isStarted = false;

        public void Start()
        {
            Clear();
            if (!this.isStarted)
                DisplayIntro();

            ChoosePlayer();
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

        private void KeyPressed(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    this.field.Update(0, -2);
                    break;
                case ConsoleKey.UpArrow:
                    this.field.Update(-2, 0);
                    break;
                case ConsoleKey.RightArrow:
                    this.field.Update(0, 2);
                    break;
                case ConsoleKey.DownArrow:
                    this.field.Update(2, 0);
                    break;
                case ConsoleKey.Enter:
                    Turn(this.player1);
                    break;
                case ConsoleKey.R:
                    Start();
                    break;
                case ConsoleKey.D1:
                    this.player1 = "X";
                    this.player2 = "O";
                    break;
                case ConsoleKey.D2:
                    this.player1 = "O";
                    this.player2 = "X";
                    break;
            }
        }

        private void PlayerMove()
        {
            ConsoleKey key = default(ConsoleKey);
            while (key != ConsoleKey.Enter)
            {
                key = GetKey();
                KeyPressed(key);
            }
        }

        private ConsoleKey GetKey()
        {
            ConsoleKeyInfo keyInfo = ReadKey(true);
            ConsoleKey key = keyInfo.Key;

            return key;
        }

        private void Turn(string symbol)
        {
            bool isWinning = this.field.DrawSymbol(symbol);

            if (isWinning) DisplayOutro(symbol);
        }

        private void ComputerMove()
        {
            this.field.SetOnRandomCell();
            Turn(this.player2);
        }

        private void DisplayIntro()
        {
            WriteLine("This is TicTacToe Game by Ivaylo Milanov");
            WriteLine("Enter - Draw symbol");
            WriteLine("Up Arrow - move up");
            WriteLine("Down Arrow - move down");
            WriteLine("Left Arrow - move left");
            WriteLine("Right Arrow - move right");
            WriteLine("R - restart the game");
            WriteLine("Press ENTER to start the game");

            WaitUntilCertainKeyIsPressed(ConsoleKey.Enter);
            Clear();
        }

        private void DisplayOutro(string winner)
        {
            Clear();
            WriteLine($"The winner is {winner}");
            WriteLine("Press R to restart the game");
            WaitUntilCertainKeyIsPressed(ConsoleKey.R);
            Start();
        }

        private void WaitUntilCertainKeyIsPressed(ConsoleKey certainKey)
        {
            ConsoleKey key;
            do
            {
                key = GetKey();
            } while (key != certainKey);
        }

        private void ReadyGame()
        {
            this.isStarted = true;
            this.field = new Field();
        }

        private void ChoosePlayer()
        {
            WriteLine("Press 1 for X");
            WriteLine("Press 2 for O");

            ConsoleKey key = GetKey();
            KeyPressed(key);
            Clear();
        }
    }
}
