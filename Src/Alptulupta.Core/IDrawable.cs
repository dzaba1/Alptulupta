using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Alptulupta.Core
{
    internal interface IDrawable : IDisposable
    {
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
