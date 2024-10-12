using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public interface ISnake
    {
        LinkedList<Position> Body { get; }
        Position Direction { get; set; }
        Position Head { get; }
        void Move();
        void Grow();
        bool IsCollision(Position position);
    }
}
