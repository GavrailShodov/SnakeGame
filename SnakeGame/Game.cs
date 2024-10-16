﻿using System;
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
        private List<Position> walls;
        private int highestScore;

        public Game(int width, int height, IRenderer renderer, IInputHandler inputHandler)
        {
            this.width = width;
            this.height = height;
            this.renderer = renderer;
            this.inputHandler = inputHandler;
            snake = new Snake(new Position(width / 2, height / 2));
            rand = new Random();
            food = GenerateFood();
            score = 0;
            level = 1;
            speed = 100;
            highestScore = 0;
            walls = new List<Position>(); 

            renderer.Render(snake.Head, null, food.Position, walls, score, level);
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

                    if (nextHeadPosition.X <= 0 || nextHeadPosition.X >= width + 1 ||
                        nextHeadPosition.Y <= 0 || nextHeadPosition.Y >= height + 1 ||
                        snake.IsCollision(nextHeadPosition) ||
                        IsWallCollision(nextHeadPosition))
                    {
                        if (highestScore < score)
                        {
                            highestScore = score;
                        }
                        if (renderer.GameOver(width, height, score,highestScore))
                        {
                            Console.Clear();
                            GenerateNewSnake();
                            renderer.DrawBorders();
                            continue;
                        }
                        else
                        {
                            break;
                        }
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
                            AddRandomWall();
                        }

                        renderer.Render(nextHeadPosition, null, food.Position, walls, score, level);
                    }
                    else
                    {
                        snake.Move();
                        renderer.Render(nextHeadPosition, oldTail, food.Position, walls, score, level);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("An error occurred:");
                Console.WriteLine(ex.Message);
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
            } while (snake.IsCollision(position) || IsWallCollision(position));
            return new Food(position);
        }

        private void GenerateNewSnake()
        {
            snake = new Snake(new Position(width / 2, height / 2));
            food = GenerateFood();
            score = 0;
            level = 1;
            speed = 100;
            walls.Clear();
        }

        private void AddRandomWall()
        {
            bool isHorizontal = rand.Next(0, 2) == 0;

            int wallLength = rand.Next(1, 16);

            Position startPosition;
            bool isValidPosition = false;

            do
            {
                if (isHorizontal)
                {
                    startPosition = new Position(rand.Next(1, width - wallLength + 1), rand.Next(1, height + 1));
                }
                else
                {
                    startPosition = new Position(rand.Next(1, width + 1), rand.Next(1, height - wallLength + 1));
                }

                isValidPosition = true;
                for (int i = 0; i < wallLength; i++)
                {
                    Position wallSegment = isHorizontal
                        ? new Position(startPosition.X + i, startPosition.Y)
                        : new Position(startPosition.X, startPosition.Y + i);

                    if (snake.IsCollision(wallSegment) || IsWallCollision(wallSegment) ||
                        (food.Position.X == wallSegment.X && food.Position.Y == wallSegment.Y))
                    {
                        isValidPosition = false;
                        break;
                    }
                }
            } while (!isValidPosition);

            for (int i = 0; i < wallLength; i++)
            {
                Position wallSegment = isHorizontal
                    ? new Position(startPosition.X + i, startPosition.Y)
                    : new Position(startPosition.X, startPosition.Y + i);

                walls.Add(wallSegment);
            }
        }

        private bool IsWallCollision(Position position)
        {
            return walls != null && walls.Any(w => w.X == position.X && w.Y == position.Y);
        }
    }
}
