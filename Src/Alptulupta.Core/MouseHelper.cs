using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Alptulupta.Core
{
    [Flags]
    internal enum MouseButtonPressed
    {
        Left = 1,
        Right = 2,
        Middle = 4
    }

    internal interface IMouseHelper : IUpdateable
    {
        MouseButtonPressed GetNewPressed();
    }

    internal sealed class MouseHelper : IMouseHelper
    {
        private readonly object syncLock = new object();
        private MouseButtonPressed lastPressed = 0;
        private MouseButtonPressed newPressed = 0;
        private readonly IMouse mouse;

        public MouseHelper(IMouse mouse)
        {
            this.mouse = mouse;
        }

        private MouseButtonPressed GetCurrent()
        {
            var state = mouse.GetState();
            MouseButtonPressed result = 0;
            if (state.LeftButton == ButtonState.Pressed)
            {
                result |= MouseButtonPressed.Left;
            }
            if (state.RightButton == ButtonState.Pressed)
            {
                result |= MouseButtonPressed.Right;
            }
            if (state.MiddleButton == ButtonState.Pressed)
            {
                result |= MouseButtonPressed.Middle;
            }

            return result;
        }

        public void Update(GameTime gameTime)
        {
            lock (syncLock)
            {
                var pressed = GetCurrent();
                newPressed = pressed & ~lastPressed;

                lastPressed = pressed;
            }
        }

        public MouseButtonPressed GetNewPressed()
        {
            lock (syncLock)
            {
                return newPressed;
            }
        }
    }
}
