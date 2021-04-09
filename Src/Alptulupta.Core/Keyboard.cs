using Microsoft.Xna.Framework.Input;

namespace Alptulupta.Core
{
    internal interface IKeyboard
    {
        KeyboardState GetState();
    }

    internal sealed class KeyboardWrap : IKeyboard
    {
        public KeyboardState GetState()
        {
            return Keyboard.GetState();
        }
    }
}
