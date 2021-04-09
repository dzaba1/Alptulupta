using System;

namespace Alptulupta.KeyboardListener
{
    /// <summary>
    /// Raw KeyEvent arguments.
    /// </summary>
    public sealed class RawKeyEventArgs : EventArgs
    {
        /// <summary>
        /// VKCode of the key.
        /// </summary>
        public int VkCode { get; set; }

        /// <summary>
        /// Is the hitted key system key.
        /// </summary>
        public bool IsSysKey { get; set; }

        /// <summary>
        /// Unicode character of key pressed.
        /// </summary>
        public string Character { get; set; }

        /// <summary>
        /// Convert to string.
        /// </summary>
        /// <returns>Returns string representation of this key, if not possible empty string is returned.</returns>
        public override string ToString()
        {
            return Character;
        }

        /// <summary>
        /// Create raw keyevent arguments.
        /// </summary>
        /// <param name="vkCode"></param>
        /// <param name="isSysKey"></param>
        /// <param name="character">Character</param>
        public RawKeyEventArgs(int vkCode, bool isSysKey, string character)
        {
            VkCode = vkCode;
            IsSysKey = isSysKey;
            Character = character;
        }
    }
}