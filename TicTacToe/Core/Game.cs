namespace TicTacToe.Core
{
    using static System.Console;
    using Players;
    using Players.Contracts;
    using Field;
    using Field.Contracts;
    using Parser;
    using Contracts;
    
    public class Game : IGame
    {
        private IField field;
        private IPlayer playerOne;
        private IPlayer playerTwo;
        private IPlayer curPlayer;
        private int x;
        private int y;
        private int index = 0;
        private bool isWinning;

        public void Start()
        {
            string[,] grid = FieldParser.GetField("../../../Field/Parser/FieldFile.txt");
            this.field = new Field(grid);
            DisplayIntro();

            GameLoop();
            Clear();

            DisplayOutro();
        }

        private void GameLoop()
        {
            this.index = -1;
            while (true)
            {
                KeyPressed();
                
                if (this.isWinning)
                {
                    break;
                }
            }
        }

        private void Move(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    if (this.field.IsWalkable(this.x, this.y - 2))
                    {
                        this.y -= 2;
                    }
                    break;
                case ConsoleKey.UpArrow:
                    if (this.field.IsWalkable(this.x - 2, this.y))
                    {
                        this.x -= 2;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (this.field.IsWalkable(this.x, this.y + 2))
                    {
                        this.y += 2;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (this.field.IsWalkable(this.x + 2, this.y))
                    {
                        this.x += 2;
                    }
                    break;
            }

            SetCursorPosition(this.y, this.x);
        }

        private void KeyPressed()
        {
            ConsoleKey key = GetKey();
            
            if (key == ConsoleKey.Enter)
            {
                Turn();
                this.index++;
            }
            else if (key == ConsoleKey.R)
            {
                this.field.Clear();
                ReadyGame();
            }
            else
            {
                Move(key);
            }
        }

        private ConsoleKey GetKey()
        {
            ConsoleKeyInfo keyInfo = ReadKey(true);
            ConsoleKey key = keyInfo.Key;

            return key;
        }

        private void Turn()
        {
            this.curPlayer = this.index % 2 == 0 ? this.playerOne : this.playerTwo;
            string symbol = this.field.GetElementAt(this.x, this.y);
            if (symbol == "O" || symbol == "X")
            {
                return;
            }

            this.curPlayer.DrawSymbol();
            this.field.Add(this.x, this.y, this.curPlayer.GetSymbol());

            if (this.field.IsWinning(this.curPlayer.GetSymbol()))
            {
                this.isWinning = true;
            }
        }

        private void ReadyGame()
        {
            this.playerOne = new PlayerOne();
            this.playerTwo = new PlayerTwo();
            this.x = 1;
            this.y = 1;
            this.field.DrawEmptyField();
            SetCursorPosition(this.y, this.x);
            this.isWinning = false;
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

            ConsoleKey key = GetKey();

            if (key == ConsoleKey.Enter)
            {
                ReadyGame();
            }
        }

        private void DisplayOutro()
        {
            WriteLine($"The winner is {curPlayer.GetSymbol()}");
            WriteLine("Press R to restart the game");
            ConsoleKey key = GetKey();

            if (key == ConsoleKey.R)
            {
                Clear();
                ReadyGame();
                Start();
            }
        }
    }
}
