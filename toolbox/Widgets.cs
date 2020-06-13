using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace slauncher
{ 
    class NoFocusCueButton : Button
    {
        protected override bool ShowFocusCues {
            get {
                return false;
            }
        }
    }
}
