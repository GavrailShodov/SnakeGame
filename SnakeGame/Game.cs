using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class Game
    {
        private int width;
        private int height;
        private ISnake snake;
        private Food food;
        private IRenderer renderer;
        private IInputHandler inputHandler;
        private int score;
        private int level;
        private int speed;
        private Random rand;

        public Game(int width, int height, IRenderer renderer, IInputHandler inputHandler)
        {
            this.width = width;
            this.height = height;
            this.renderer = renderer;
            this.inputHandler = inputHandler;
            snake = new Snake(new Position(width / 2, height / 2));
            rand = new Random(); // Initialize Random once
            food = GenerateFood();
            score = 0;
            level = 1;
            speed = 100; // milliseconds

            // Initial rendering of snake and food
            renderer.Render(snake.Head, null, food.Position, score, level);
        }

        public void Run()
        {
            try
            {
                var start = renderer.StartGame(width, height);
                Console.Clear();
                renderer.DrawBorders();

                while (start)
                {
                    Thread.Sleep(speed);

                    inputHandler.ProcessInput(snake);

                    Position nextHeadPosition = new Position(snake.Head.X + snake.Direction.X, snake.Head.Y + snake.Direction.Y);

                    // Check for collision with walls (including borders) or self
                    if (nextHeadPosition.X <= 0 || nextHeadPosition.X >= width + 1 ||
                        nextHeadPosition.Y <= 0 || nextHeadPosition.Y >= height + 1 ||
                        snake.IsCollision(nextHeadPosition))
                    {
                        renderer.DisplayGameOver(width, height, score);
                        Console.ReadKey();
                        break;
                    }

                    Position oldTail = snake.Body.Last.Value;

                    if (nextHeadPosition.X == food.Position.X && nextHeadPosition.Y == food.Position.Y)
                    {
                        snake.Grow();
                        score += 10;
                        food = GenerateFood();

                        if (score % 30 == 0)
                        {
                            level++;
                            speed = Math.Max(20, speed - 20);

                        }

                        renderer.Render(nextHeadPosition, null, food.Position, score, level);
                    }
                    else
                    {
                        snake.Move();
                        renderer.Render(nextHeadPosition, oldTail, food.Position, score, level);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("An error occurred:");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        private Food GenerateFood()
        {
            Position position;
            do
            {
                position = new Position(rand.Next(1, width + 1), rand.Next(1, height + 1));
            } while (snake.IsCollision(position));
            return new Food(position);
        }
    }
}
