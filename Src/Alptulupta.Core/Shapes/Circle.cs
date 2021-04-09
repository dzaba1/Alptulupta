using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Alptulupta.Core.Shapes
{
    internal sealed class Circle : StaticShape
    {
        public Circle(Vector2 position, Color color, float diameter)
            : base(position, color)
        {
            Diameter = diameter;
        }

        public float Diameter { get; }

        protected override Texture2D BuildTexture(SpriteBatch spriteBatch)
        {
            var intDiameter = (int) Diameter;
            var texture = new Texture2D(spriteBatch.GraphicsDevice, intDiameter, intDiameter);
            var colorData = new Color[intDiameter * intDiameter];

            var radius = Diameter / 2f;
            var radiussq = radius * radius;

            for (var x = 0; x < intDiameter; x++)
            {
                for (var y = 0; y < intDiameter; y++)
                {
                    var index = x * intDiameter + y;
                    var pos = new Vector2(x - radius, y - radius);
                    if (pos.LengthSquared() <= radiussq)
                    {
                        colorData[index] = Color;
                    }
                    else
                    {
                        colorData[index] = Color.Transparent;
                    }
                }
            }

            texture.SetData(colorData);
            return texture;
        }

        protected override Vector2 GetScale()
        {
            return Vector2.One;
        }
    }
}
