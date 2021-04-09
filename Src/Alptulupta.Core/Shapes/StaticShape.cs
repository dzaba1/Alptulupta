using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Alptulupta.Core.Shapes
{
    internal abstract class StaticShape : IDrawable
    {
        private Texture2D texture;

        protected StaticShape(Vector2 position, Color color)
        {
            Position = position;
            Color = color;
        }

        protected abstract Texture2D BuildTexture(SpriteBatch spriteBatch);

        protected abstract Vector2 GetScale();

        public Vector2 Position { get; }

        public Color Color { get; }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (texture == null)
            {
                texture = BuildTexture(spriteBatch);
            }

            spriteBatch.Draw(texture, Position, null,
                Color, 0, Vector2.Zero, GetScale(),
                SpriteEffects.None, 0f);
        }

        public void Dispose()
        {
            texture?.Dispose();
        }
    }
}
