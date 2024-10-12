using System;
using System.Collections.Generic;

namespace SnakeGame
{
    public class Renderer : IRenderer
    {
        private int width;
        private int height;

        public Renderer(int width, int height)
        {
            this.width = width;
            this.height = height;
            Console.CursorVisible = false;

            try
            {
                Console.SetWindowSize(width + 2, height + 5);
                Console.SetBufferSize(width + 2, height + 5);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Please resize your console window to be larger.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        public void DrawBorders()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("+" + new string('-', width) + "+");

            for (int y = 1; y <= height; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.WriteLine("|" + new string(' ', width) + "|");
            }

            Console.SetCursorPosition(0, height + 1);
            Console.WriteLine("+" + new string('-', width) + "+");
        }

        public void Render(Position newHead, Position oldTail, Position foodPosition, List<Position> walls, int score, int level)
        {
            Console.SetCursorPosition(0, height + 2);
            Console.Write($"Score: {score} Level: {level}   ");

            Console.SetCursorPosition(newHead.X, newHead.Y);
            Console.Write("@");

            if (oldTail != null)
            {
                Console.SetCursorPosition(oldTail.X, oldTail.Y);
                Console.Write(" ");
            }

            Console.SetCursorPosition(foodPosition.X, foodPosition.Y);
            Console.Write("*");

            foreach (var wall in walls)
            {
                Console.SetCursorPosition(wall.X, wall.Y);
                Console.Write("#");
            }
        }

        public bool GameOver(int width, int height, int score)
        {
            Console.Clear();
            Console.WriteLine("Game Over!");
            Console.WriteLine($"Final Score: {score}");
            Console.WriteLine("Press Arrow up to restart");
            return Console.ReadKey(true).Key == ConsoleKey.UpArrow;
        }

        public bool StartGame(int width, int height)
        {
            bool start = false;

            do
            {
                Console.Clear();
                Console.WriteLine("Can you become the largest snake?");
                Console.WriteLine("Play with arrows, collect food");
                Console.WriteLine("Level up on every 30 points");
                Console.WriteLine("Press ArrowUp to start");

                if (Console.ReadKey(true).Key == ConsoleKey.UpArrow)
                {
                    start = true;
                }
            } while (!start);
            return start;
        }
    }
}
