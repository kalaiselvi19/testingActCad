using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testingActcad
{
    public partial class BeamPropertiesWindow : Form
    {
        public BeamPropertiesWindow()
        {
            InitializeComponent();
            this.ControlBox = false;
            //this.Text = " ";
        }

        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {
        //    //this.Close();
        }

        private void elementHost1_ChildChanged_1(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
    }
}
