using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public interface IRenderer
    {
        void Render(Position newHead, Position oldTail, Position foodPosition, List<Position> walls, int score, int level);
        void DrawBorders();
        bool GameOver(int width, int height, int score);
        bool StartGame(int width, int height);
    }
}
