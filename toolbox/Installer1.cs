using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace slauncher
{
    [RunInstaller(true)]
    public partial class Installer1 : System.Configuration.Install.Installer
    {
        public Installer1()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);


            
            string home = this.Context.Parameters["assemblypath"];
            //Computer\HKEY_CURRENT_USER\Software\Classes\Directory\Background\shell 
            //Software\Classes\Directory\Background\shell  hkey_users
            //RegistryKey _key = Registry.CurrentUser.OpenSubKey(@"Software", true); 
            //RegistryKey newkey = _key.CreateSubKey("Create new launcher"); 
            /*

            RegistryKey newkey = _key.CreateSubKey("Create new launcher");
            //newkey.SetValue("AppliesTo", "under:T:"); 
            RegistryKey subNewkey = newkey.CreateSubKey("Command");
            subNewkey.SetValue("", home+" %1");
            subNewkey.Close(); 
            newkey.Close();
            _key.Close(); 
            */
        }
    }
}
