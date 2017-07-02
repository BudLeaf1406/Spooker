using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prankxd
{
    public partial class frmgame : Form
    {
        #region emt stuff
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA")]
        public static extern void mciSendStringA(string lpstrCommand,
       string lpstrReturnString, int uReturnLength, int hwndCallback);
        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);
        [DllImport("User32.dll")]
        private static extern int FindWindowEx(int hWnd1, int hWnd2, string lpsz1, string lpsz2);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetDesktopWindow();
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, uint cButtons, uint dwExtraInfo);

        private const int SW_SHOW = 1;
        private const int SW_HIDE = 0;
        public static void hClock()
        {
            int hwnd = 0;
            ShowWindow(
              FindWindowEx(FindWindowEx(FindWindow("Shell_TrayWnd", null), hwnd, "TrayNotifyWnd", null),
              hwnd, "TrayClockWClass", null),
              SW_HIDE);
        }

        public static void sClock()
        {
            int hwnd = 0;
            ShowWindow(
              FindWindowEx(FindWindowEx(FindWindow("Shell_TrayWnd", null), hwnd, "TrayNotifyWnd", null),
              hwnd, "TrayClockWClass", null),
              SW_SHOW);
        }

        public static void hTaskBar()
        {
            ShowWindow(FindWindow("Shell_TrayWnd", null), SW_HIDE);
        }

        public static void sTaskBar()
        {
            ShowWindow(FindWindow("Shell_TrayWnd", null), SW_SHOW);
        }

        public static void hDesktop()
        {
            ShowWindow(FindWindow(null, "Program Manager"), SW_HIDE);
        }

        public static void sDesktop()
        {
            ShowWindow(FindWindow(null, "Program Manager"), SW_SHOW);
        }

        public static void hTrayIcons()
        {
            int hwnd = 0;
            ShowWindow(FindWindowEx(FindWindow("Shell_TrayWnd", null),
                            hwnd, "TrayNotifyWnd", null),
                            SW_HIDE);
        }

        public static void sTrayIcons()
        {
            int hwnd = 0;
            ShowWindow(FindWindowEx(FindWindow("Shell_TrayWnd", null),
                            hwnd, "TrayNotifyWnd", null),
                            SW_SHOW);
        }


        #endregion
        public static string GetUniqueUSG(int maxSize)
        {
            char[] chars = new char[62];
            chars =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
        public static string GetUniqueNum(int maxSize)
        {
            char[] chars = new char[62];
            chars =
                "123456789".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
        public static int GetUniqueNum2(int maxSize)
        {
            char[] chars = new char[62];
            chars =
                "1234".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            int result2 = int.Parse(result.ToString());
            return result2;
        }
        int qNum = 0;
        string[] math = new string[5] {"+", "+", "-", "*", "/" };
        int wrong = 0;
        double answer = 0;
        public frmgame()
        {
            InitializeComponent();
            RefreshGame();
        }
        int RandSeed()
        {    
            return GetUniqueNum2(1);
        }
        void RefreshGame()
        {
            if (wrong == 3)
            {
                Troll();
                return;
            }
            else if(wrong != 3 && qNum == 10)
            {
                Clipboard.SetText("taskkill /im \"prankxd.exe\" /f");
                MessageBox.Show("I'ave copied the needed cmd to you'r clipboard go to cmd and paste!","Lucky...",MessageBoxButtons.OK,MessageBoxIcon.Information);              
            }
            else if (wrong != 3 && qNum != 10)
            {
                qNum++;
                double num1 = double.Parse(GetUniqueNum(RandSeed()));
                double num2 = double.Parse(GetUniqueNum(RandSeed()));               
                string func1 = math[GetUniqueNum2(1)];
                lbNumber.Text = "Question num: " + qNum.ToString() + ".";
                txtQuestion.Text =  num1 + func1 + num2;

                #region checks
                if(func1 == "+")
                {
                    answer = num1 + num2;
                }
                else if(func1 == "-")
                {
                    answer = num1 - num2;
                }
                else if (func1 == "*")
                {
                    answer = num1 * num2;
                }
                else if (func1 == "/")
                {
                    answer = num1 / num2;
                }
                #endregion
            }
            
        }
        void Troll()
        {
            timer1.Start();           
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(double.Parse(txtAnswer.Text) == answer)
            {
                MessageBox.Show("Nice job...");
                RefreshGame();
            }
            else
            {
                wrong++;
                RefreshGame();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            hClock();
            hDesktop();
            hTaskBar();
            hTrayIcons();
            Thread.Sleep(500);
            sClock();
            sDesktop();
            sTaskBar();
            sTrayIcons();
        }
    }
}
