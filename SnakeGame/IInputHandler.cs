using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public interface IInputHandler
    {
        void ProcessInput(ISnake snake);
    }
}
