using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JPTLaserMarkWPF.Samlight
{
    public partial class SamlightForm : Form
    {
        public SamlightForm()
        {
            InitializeComponent();
        }

        public AxSAMLIGHT_CLIENT_CTRL_OCXLib.AxScSamlightClientCtrl GetControl()
        {
            return SLCtrl;
        }
    }
}
