using Alptulupta.Contracts;
using Microsoft.Xna.Framework;

namespace Alptulupta.Core
{
    public interface IRandomHelper
    {
        Vector2 RandomPosition(Vector2 screenSize);
        Color RandomColor();
        Vector2 RandomRectSize(Vector2 screenSize, Vector2 position);
    }

    internal sealed class RandomHelper : IRandomHelper
    {
        private readonly IRandom rand;

        public RandomHelper(IRandom rand)
        {
            this.rand = rand;
        }

        public Vector2 RandomPosition(Vector2 screenSize)
        {
            var x = rand.Next(0, (int)screenSize.X);
            var y = rand.Next(0, (int)screenSize.Y);

            return new Vector2(x, y);
        }

        public Vector2 RandomRectSize(Vector2 screenSize, Vector2 position)
        {
            var maxWidth = screenSize.X - position.X;
            var maxHeight = screenSize.Y - position.Y;
            var width = rand.Next(1, (int)maxWidth);
            var height = rand.Next(1, (int)maxHeight);

            return new Vector2(width, height);
        }

        public Color RandomColor()
        {
            var r = rand.Next(0, 256);
            var g = rand.Next(0, 256);
            var b = rand.Next(0, 256);

            return new Color(r, g, b);
        }
    }
}
