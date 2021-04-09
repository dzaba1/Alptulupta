using System;
using Alptulupta.Contracts;
using Microsoft.Xna.Framework;

namespace Alptulupta.Core.Shapes
{
    internal interface IShapeFactory
    {
        StaticShape Random(Vector2 screenSize);
    }

    internal sealed class ShapeFactory : IShapeFactory
    {
        private readonly IRandom rand;
        private readonly IRandomHelper randomHelper;

        public ShapeFactory(IRandom rand, IRandomHelper randomHelper)
        {
            this.rand = rand;
            this.randomHelper = randomHelper;
        }

        public StaticShape Random(Vector2 screenSize)
        {
            var shapeType = RandomShapeType();
            switch (shapeType)
            {
                case ShapeType.Rect:
                    return RandomRect(screenSize);
                case ShapeType.Circle:
                    return RandomCircle(screenSize);
                default: throw new ArgumentOutOfRangeException("shapeType", $"Unknown shape type: {shapeType}");
            }
        }

        private StaticShape RandomCircle(Vector2 screenSize)
        {
            var pos = randomHelper.RandomPosition(screenSize);
            var color = randomHelper.RandomColor();
            var size = randomHelper.RandomRectSize(screenSize, pos);

            var diameter = size.X < size.Y ? size.X : size.Y;
            return new Circle(pos, color, diameter);
        }

        private StaticShape RandomRect(Vector2 screenSize)
        {
            var pos = randomHelper.RandomPosition(screenSize);
            var color = randomHelper.RandomColor();
            var size = randomHelper.RandomRectSize(screenSize, pos);

            return new Rectangle(pos, color, size);
        }

        private ShapeType RandomShapeType()
        {
            return (ShapeType)rand.Next(0, 2);
        }

        private enum ShapeType
        {
            Rect,
            Circle
        }
    }
}
