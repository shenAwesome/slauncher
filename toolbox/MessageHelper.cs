
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;

namespace slauncher
{
    class MessageHelper
    {
        public const int HWND_BROADCAST = 0xffff;
        public const int WM_COPYDATA = 0x4A;

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string message);

        public static IntPtr GetWindowHandle(string appName)
        {
            IntPtr windowHandle = IntPtr.Zero; 
            Process currentProcess = Process.GetCurrentProcess();
            var handles = Process.GetProcessesByName(appName)
                .Select(p => p.MainWindowHandle)
                .Where(h => h != currentProcess.MainWindowHandle); 
            if (handles.Count() > 0)
            {
                windowHandle = handles.First();
            }
            return windowHandle;
        }

        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }

        public static int Send(string appName, int msgType)
        {
            var hWnd = GetWindowHandle(appName);

            return SendMessage(hWnd, msgType, 0, 0);
        }

        /**
         * send a WM_COPYDATA message
         */
        public static int Send(string appName, string msg)
        {
            var hWnd = GetWindowHandle(appName);
            byte[] sarr = System.Text.Encoding.Default.GetBytes(msg);
            int len = sarr.Length;
            COPYDATASTRUCT cds;
            cds.dwData = (IntPtr)100;
            cds.lpData = msg;
            cds.cbData = len + 1;
            return SendMessage(hWnd, WM_COPYDATA, 0, ref cds);
        }

        public static string ReadStr(Message m)
        {
            var ret = "";
            try
            {
                COPYDATASTRUCT mystr = new COPYDATASTRUCT();
                Type mytype = mystr.GetType();
                mystr = (COPYDATASTRUCT)m.GetLParam(mytype);
                ret = mystr.lpData;
            }
            catch { }
            return ret;
        }
    }
}
