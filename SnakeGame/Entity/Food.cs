using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class Food
    {
        public Position Position { get; private set; }

        public Food(Position position)
        {
            Position = position;
        }
    }
}
