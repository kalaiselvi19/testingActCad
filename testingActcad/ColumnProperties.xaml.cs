using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace testingActcad
{
    /// <summary>
    /// Interaction logic for ColumnProperties.xaml
    /// </summary>
    public partial class ColumnProperties : UserControl
    {
        private Beam sb;
        private ucProfilesWindow upfw;
        private static string xml;
        public static string xmfile
        {
            get { return xml; }
            set { xml = value; }
        }
        private static string pfname;
        public static string Profname
        {
            get { return pfname; }
            set { pfname = value; }
        }
        public ColumnProperties()
        {
            InitializeComponent();
            this.cmboPlane.SelectedIndex = 1;
            this.cmboRotation.SelectedIndex = 1;
            this.cmboDepth.SelectedIndex = 1;
        }
        private void btnProfileSelect_Click(object sender, RoutedEventArgs e)
        {
            OPTIONCHANGE();
            pfname = this.txtProfile.Text;
            // pf = new Profiles(); 

            //if (pfw == null)
            //{
            //    pfw = new ProfileWindow();
            //}
            if (upfw == null)
            {
                upfw = new ucProfilesWindow();
            }
            //pf.Readprofiles();
            IntelliCAD.ApplicationServices.Application.ShowModelessDialog(upfw);
        }

        private void chkCheck_Changed(object sender, RoutedEventArgs e)
        {
            if (chkCheck.IsChecked == true)
            {
                this.chkName.IsChecked = true;
                this.chkMaterial.IsChecked = true;
                this.chkProfile.IsChecked = true;
                this.chkFinish.IsChecked = true;
                this.chkcolor.IsChecked = true;
                this.chkPlane.IsChecked = true;
                this.chkRotation.IsChecked = true;
                this.chkDepth.IsChecked = true;
                //if( chkUncheck.IsChecked==true)
                //{
                //    this.chkUncheck.IsChecked = false;
                //}
            }
            else
            {
                this.chkName.IsChecked = false;
                this.chkMaterial.IsChecked = false;
                this.chkProfile.IsChecked = false;
                this.chkFinish.IsChecked = false;
                this.chkcolor.IsChecked = false;
                this.chkPlane.IsChecked = false;
                this.chkRotation.IsChecked = false;
                this.chkDepth.IsChecked = false;
                //if (chkUncheck.IsChecked == false)
                //{
                //    this.chkUncheck.IsChecked = true;
                //}
            }
        }

        //private void chkUncheck_Changed(object sender, RoutedEventArgs e)
        //{
        //    if (chkUncheck.IsChecked == true)
        //    {
        //        this.chkName.IsChecked = false;
        //        this.chkMaterial.IsChecked = false;
        //        this.chkProfile.IsChecked = false;
        //        this.chkFinish.IsChecked = false;
        //        this.chkcolor.IsChecked = false;
        //        if (chkCheck.IsChecked == true)
        //        {
        //            chkCheck.IsChecked = false;
        //        }
        //    }
        //    else
        //    {
        //        if (chkCheck.IsChecked == false)
        //        {
        //            chkCheck.IsChecked = true;
        //        }
        //    }
        //}

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            assignvalues();
            // sb.SaveBeamProperties();
            sb.closeColumn();
            // sb = null;
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            assignvalues();
            sb = null;
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            assignvalues();
            sb.ModifyBeamProperties();
            sb = null;
        }

        private void btnGet_Click(object sender, RoutedEventArgs e)
        {
            assignvalues();
            sb.GetBeamProperties();
            getvalues();
            sb = null;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            sb = new Beam();
            sb.closeColumn();
            sb = null;
        }

        private void btnProfileSelect_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //sb = new Beam();
            //sb.getprofiles();
            //sb = null;
        }
        /// <summary>
        /// assigns values to the beam property window from class variables
        /// </summary>
        private void getvalues()
        {
            this.txtMaterial.Text = sb.Material;
            this.txtFinish.Text = sb.finish;
            this.txtColor.Text = sb.clr;
            this.txtRotation.Text = sb.rotation_txt;
            this.txtPlane.Text = sb.plane_txt;
            this.txtDepth.Text = sb.depth_txt;
            this.cmboPlane.SelectedValue = sb.plane;
            this.cmboRotation.SelectedValue = sb.rotation;
            this.cmboDepth.SelectedValue = sb.depth;
            this.txtName.Text = sb.Name;
        }

        /// <summary>
        /// assigns the bream properties window values to class variables
        /// </summary>
        private void assignvalues()
        {
            sb = new Beam();
            sb.ReadProfiles();
            sb.ProfileName = this.txtProfile.Text;
            sb.Material = this.txtMaterial.Text;
            sb.finish = this.txtFinish.Text;
            sb.clr = this.txtColor.Text;
            sb.plane = this.cmboPlane.Text;
            sb.rotation = this.cmboRotation.Text;
            sb.depth = this.cmboDepth.Text;
            sb.depth_txt = this.txtDepth.Text;
            sb.plane_txt = this.txtPlane.Text;
            sb.rotation_txt = this.txtRotation.Text;
            sb.Name = this.txtName.Text;
        }
        private void OPTIONCHANGE()
        {
            if (this.optionAustralian.IsChecked == true)
            {
                xml = "AUSTRALIAN";
            }

            if (this.optionUSimperial.IsChecked == true)
            {
                xml = "USIMPERIAL";
            }
            if (this.optionUSmetric.IsChecked == true)
            {
                xml = "USMETRIC";
            }
            if (this.optionChina.IsChecked == true)
            {
                xml = "CHINA";
            }
        }

        private void btnLoadXml_Click(object sender, RoutedEventArgs e)
        {
            OPTIONCHANGE();
        }
    }
}
