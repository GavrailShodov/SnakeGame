using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class Snake : ISnake
    {
        private LinkedList<Position> body;
        private Position direction;

        public Snake(Position initialPosition)
        {
            body = new LinkedList<Position>();
            body.AddFirst(initialPosition);
            direction = new Position(1, 0);
        }

        public LinkedList<Position> Body => body;

        public Position Direction
        {
            get => direction;
            set => direction = value;
        }

        public Position Head => body.First.Value;

        public void Move()
        {
            var newHead = new Position(Head.X + direction.X, Head.Y + direction.Y);
            body.AddFirst(newHead);
            body.RemoveLast();
        }

        public void Grow()
        {
            var newHead = new Position(Head.X + direction.X, Head.Y + direction.Y);
            body.AddFirst(newHead);
        }

        public bool IsCollision(Position position)
        {
            return body.Any(p => p.X == position.X && p.Y == position.Y);
        }
    }
}
