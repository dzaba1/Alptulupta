using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace Alptulupta.Core
{
    public static class Extensions
    {
        public static Buttons GetPressed(this GamePadState state)
        {
            Buttons result = 0;
            var allValues = Enum.GetValues(typeof(Buttons)).Cast<Buttons>();
            foreach (var value in allValues)
            {
                if (state.IsButtonDown(value))
                {
                    result |= value;
                }
            }

            return result;
        }
    }
}
