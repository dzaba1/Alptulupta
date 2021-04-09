using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Alptulupta.Core.Shapes
{
    internal sealed class Rectangle : StaticShape
    {
        public Vector2 Size { get; }

        public Rectangle(Vector2 position, Color color, Vector2 size)
            : base(position, color)
        {
            Size = size;
        }

        protected override Texture2D BuildTexture(SpriteBatch spriteBatch)
        {
            var tex = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            tex.SetData(new[] { Color.White });
            return tex;
        }

        protected override Vector2 GetScale()
        {
            return Size;
        }
    }
}
