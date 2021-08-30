using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Alptulupta.Core
{
    internal interface IGamepadHelper : IUpdateable
    {
        IReadOnlyDictionary<int, Buttons> GetNewButtonsPressed();
    }

    internal sealed class GamepadHelper : IGamepadHelper
    {
        private readonly object syncLock = new object();
        private readonly Dictionary<int, Buttons> lastPressedDict = new Dictionary<int, Buttons>();
        private readonly Dictionary<int, Buttons> newPressedDict = new Dictionary<int, Buttons>();

        public IReadOnlyDictionary<int, Buttons> GetNewButtonsPressed()
        {
            lock (syncLock)
            {
                return newPressedDict;
            }
        }

        public void Update(GameTime gameTime)
        {
            var states = GetConnectedStates();

            lock (syncLock)
            {
                foreach (var state in states)
                {
                    CheckButtons(state.Key, state.Value);
                }
            }
        }

        private void CheckButtons(int index, GamePadState state)
        {
            var pressed = state.GetPressed();

            if (!lastPressedDict.TryGetValue(index, out var lastPressed))
            {
                lastPressed = 0;
            }

            newPressedDict[index] = pressed & ~lastPressed;
            lastPressedDict[index] = pressed;
        }

        private IReadOnlyDictionary<int, GamePadState> GetConnectedStates()
        {
            var dict = new Dictionary<int, GamePadState>();

            for (int i = 0; i < GamePad.MaximumGamePadCount; i++)
            {
                var state = GamePad.GetState(i);
                if (state.IsConnected)
                {
                    dict.Add(i, state);
                }
            }

            return dict;
        }
    }
}
