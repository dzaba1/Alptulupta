namespace Alptulupta.KeyboardListener
{
    /// <summary>
    /// Key event
    /// </summary>
    internal enum KeyEvent : int
    {
        /// <summary>
        /// Key down
        /// </summary>
        WM_KEYDOWN = 256,

        /// <summary>
        /// Key up
        /// </summary>
        WM_KEYUP = 257,

        /// <summary>
        /// System key up
        /// </summary>
        WM_SYSKEYUP = 261,

        /// <summary>
        /// System key down
        /// </summary>
        WM_SYSKEYDOWN = 260
    }
}