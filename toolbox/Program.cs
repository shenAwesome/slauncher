using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace slauncher
{
    static class Program
    {
        static Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (mutex.WaitOne(TimeSpan.Zero, true))
                { //if no running instance, create one
                    var form = new Form1(); 
                    if (args.Length == 1) form.FilePath = args[0];
                    Application.Run(form); 
                    mutex.ReleaseMutex();
                }
                else
                {
                    if (args.Length == 1)
                    {  //pass path to existing instance 
                        MessageHelper.Send("SLauncher", args[0]);
                    }
                    else
                    {   //bring it to front 
                        MessageHelper.Send("SLauncher", 100);
                    } 
                }

            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
           
        }
    }

}
