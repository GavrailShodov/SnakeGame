using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class InputHandler : IInputHandler
    {
        public void ProcessInput(ISnake snake)
        {
            if (!Console.KeyAvailable)
                return;

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (snake.Direction.Y != 1)
                        snake.Direction = new Position(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    if (snake.Direction.Y != -1)
                        snake.Direction = new Position(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    if (snake.Direction.X != 1)
                        snake.Direction = new Position(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    if (snake.Direction.X != -1)
                        snake.Direction = new Position(1, 0);
                    break;
            }
        }
    }
}
