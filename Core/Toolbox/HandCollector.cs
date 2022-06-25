using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace Rake_Counter.Core.Toolbox
{
    internal class HandCollector
    {
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;

        public int tryCount;

        public void Collect()
        {
            StringBuilder sb = new StringBuilder(65535);
            IntPtr handle = FindWindow(null, "Instant Hand History");
            SetForegroundWindow(handle);
            IntPtr level1 = FindWindowEx(handle, IntPtr.Zero, "PokerStarsListViewClass", string.Empty);
            IntPtr level5 = FindWindowEx(level1, IntPtr.Zero, "PokerStarsListClass", string.Empty);
            IntPtr textElement = FindWindowEx(level1, IntPtr.Zero, "PokerStarsViewClass", string.Empty);
            AutomationElement txt = AutomationElement.FromHandle(textElement);
            string previous = "";
            List<string> hands = new List<string>();
            while (true)
            {
                if (tryCount == 5) { break; }

                if (previous != txt.Current.Name)
                {
                    previous = txt.Current.Name;
                    tryCount = 0;
                    hands.Add(txt.Current.Name);
                    SendArrowDown(level5);
                }
                else
                {
                    tryCount++;
                }
                Thread.Sleep(250);

            }
            string logFilePath = "output.txt";

            if (File.Exists(logFilePath)) { File.Delete(logFilePath); }
            if (!File.Exists(logFilePath)) { File.Create(logFilePath).Dispose(); }

            using (StreamWriter sw = new StreamWriter(logFilePath, true))
            {
                hands.ForEach(o => sw.Write(o + "\n\n\n"));
            }
        }


        private void SendArrowDown(IntPtr handle)
        {
            SendMessage(handle, WM_KEYDOWN, 0x28, 1);
            SendMessage(handle, WM_KEYUP, 0x28, 1);
        }

    }
}
