using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Alptulupta.Core
{
    internal interface IKeyboardHelper : IUpdateable
    {
        ISet<Keys> GetNewPressed();
    }

    internal sealed class KeyboardHelper : IKeyboardHelper
    {
        private readonly object syncLock = new object();
        private Keys[] lastPressed;
        private HashSet<Keys> newPressed;
        private readonly IKeyboard keyboard;

        public KeyboardHelper(IKeyboard keyboard)
        {
            this.keyboard = keyboard;
        }

        public void Update(GameTime gameTime)
        {
            lock (syncLock)
            {
                var pressed = keyboard.GetState().GetPressedKeys();
                newPressed = new HashSet<Keys>(pressed);
                if (lastPressed != null)
                {
                    newPressed.ExceptWith(lastPressed);
                }
                
                lastPressed = pressed;
            }
        }

        public ISet<Keys> GetNewPressed()
        {
            lock (syncLock)
            {
                return newPressed;
            }
        }
    }
}
