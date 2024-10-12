using SnakeGame;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

int width = Console.LargestWindowWidth / 2;
int height = Console.LargestWindowHeight / 2;

IRenderer renderer = new Renderer(width, height);
IInputHandler inputHandler = new InputHandler();

Game game = new Game(width, height, renderer, inputHandler);
game.Run();
  

