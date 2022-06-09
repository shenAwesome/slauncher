using System;
using System.Runtime.InteropServices;
using PInvoke;

namespace slauncher {

    class Helpers {
        internal const int AttachParentProcess = -1;
        internal static void Jiggle(int delta) {
            var inp = new User32.INPUT {
                type = User32.InputType.INPUT_MOUSE,
                Inputs = new User32.INPUT.InputUnion {
                    mi = new User32.MOUSEINPUT {
                        dx = delta,
                        dy = delta,
                        mouseData = 0,
                        dwFlags = User32.MOUSEEVENTF.MOUSEEVENTF_MOVE,
                        time = 0,
                        dwExtraInfo_IntPtr = IntPtr.Zero,
                    },
                },
            };

            uint returnValue = User32.SendInput(nInputs: 1, pInputs: new[] { inp, },
                cbSize: Marshal.SizeOf<User32.INPUT>());
        }
    }
}
