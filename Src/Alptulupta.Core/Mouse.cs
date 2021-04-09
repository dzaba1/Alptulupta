using Microsoft.Xna.Framework.Input;

namespace Alptulupta.Core
{
    internal interface IMouse
    {
        MouseState GetState();
    }

    internal sealed class MouseWrap : IMouse
    {
        public MouseState GetState()
        {
            return Mouse.GetState();
        }
    }
}
