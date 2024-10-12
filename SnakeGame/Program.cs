using SnakeGame;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

int width = 40;
int height = 20;

IRenderer renderer = new Renderer(width, height);
IInputHandler inputHandler = new InputHandler();

Game game = new Game(width, height, renderer, inputHandler);
game.Run();
  

