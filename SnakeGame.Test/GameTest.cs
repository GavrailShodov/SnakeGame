using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Test
{
    public class GameTests
    {

        private readonly Mock<IRenderer> mockRenderer;
        private readonly Mock<IInputHandler> mockInputHandler;
        private readonly Game game;

        public GameTests()
        {
            mockRenderer = new Mock<IRenderer>();
            mockInputHandler = new Mock<IInputHandler>();
            game = new Game(20, 20, mockRenderer.Object, mockInputHandler.Object);
        }

        [Fact]
        public void GenerateFood_ShouldGenerateFoodInValidPosition()
        {
            game.GetType().GetMethod("GenerateNewSnake", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(game, null);
            var initialFoodPosition = typeof(Game).GetMethod("GenerateFood", BindingFlags.NonPublic | BindingFlags.Instance);

            var food = (Food)initialFoodPosition.Invoke(game, null);

            Assert.NotNull(food);
            Assert.InRange(food.Position.X, 1, 20);
            Assert.InRange(food.Position.Y, 1, 20);
        }

        [Fact]
        public void GenerateNewSnake_ShouldResetGameState()
        {
            var generateNewSnakeMethod = typeof(Game).GetMethod("GenerateNewSnake", BindingFlags.NonPublic | BindingFlags.Instance);

            generateNewSnakeMethod.Invoke(game, null);

            var score = typeof(Game).GetField("score", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(game);
            var level = typeof(Game).GetField("level", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(game);
            var speed = typeof(Game).GetField("speed", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(game);

            Assert.Equal(0, (int)score);
            Assert.Equal(1, (int)level);
            Assert.Equal(100, (int)speed);
        }
    }
}
