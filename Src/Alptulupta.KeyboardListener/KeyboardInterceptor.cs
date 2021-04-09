using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Alptulupta.KeyboardListener
{
    public sealed class KeyboardInterceptor : IDisposable
    {
        /// <summary>
        /// Hook ID
        /// </summary>
        private readonly IntPtr hookId = IntPtr.Zero;

        /// <summary>
        /// It should be kept as a field to not to be garbage collected
        /// </summary>
        private readonly LowLevelKeyboardProc hookHandle;

        public KeyboardInterceptor()
        {
            hookHandle = LowLevelKeyboardProc;

            // Set the hook
            hookId = InterceptKeys.SetHook(hookHandle);
        }

        public event RawKeyEventHandler OnKeyAction;

        /// <summary>
        /// Destroys global keyboard listener.
        /// </summary>
        ~KeyboardInterceptor()
        {
            Dispose();
        }

        public void Dispose()
        {
            InterceptKeys.UnhookWindowsHookEx(hookId);
        }

        /// <summary>
        /// Actual callback hook.
        /// 
        /// <remarks>Calls asynchronously the asyncCallback.</remarks>
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        private IntPtr LowLevelKeyboardProc(int nCode, UIntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && OnKeyAction != null)
            {
                var keyEvent = (KeyEvent)wParam.ToUInt32();

                if (keyEvent == KeyEvent.WM_KEYDOWN ||
                    keyEvent == KeyEvent.WM_KEYUP ||
                    keyEvent == KeyEvent.WM_SYSKEYDOWN ||
                    keyEvent == KeyEvent.WM_SYSKEYUP)
                {
                    // Captures the character(s) pressed only on WM_KEYDOWN
                    var character = InterceptKeys.VKCodeToString((uint) Marshal.ReadInt32(lParam),
                        (wParam.ToUInt32() == (int) KeyEvent.WM_KEYDOWN ||
                         wParam.ToUInt32() == (int) KeyEvent.WM_SYSKEYDOWN));
                    var vkCode = Marshal.ReadInt32(lParam);
                    var isSystem = keyEvent == KeyEvent.WM_SYSKEYDOWN || keyEvent == KeyEvent.WM_SYSKEYUP;
                    var arg = new RawKeyEventArgs(vkCode, isSystem, character);

                    if (OnKeyAction(this, arg))
                    {
                        //Return a dummy value to trap the keystroke
                        return (IntPtr)1;
                    }
                }
            }

            return InterceptKeys.CallNextHookEx(hookId, nCode, wParam, lParam);
        }
    }
}
