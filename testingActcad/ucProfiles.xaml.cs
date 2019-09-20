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
    /// Interaction logic for ucProfiles.xaml
    /// </summary>
    public partial class ucProfiles : UserControl

    {
        private string selectedPfname="";
        private Beam sb=null;
        private beamproperties bp=null;
        public ucProfiles()
        {
            InitializeComponent();
            getupdate();
            
        }
        private void getupdate()
        {
            sb = new Beam();
            sb.updateVariables();
            string sp = beamproperties.Profname;
            if (sp != "")
            {
                var dt = sb.dict.Where(user => user.Key.StartsWith(sp)).ToList();
                this.trProfiles.ItemsSource = dt;
            }
            else
            {
                var dt = sb.dict;
                this.trProfiles.ItemsSource = dt;
            }
            sb = null;
            bp = null;
        }

        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            sb = new Beam();

            sb.ProfileName = selectedPfname;
            sb.closeProfilesWindow();
            sb = null;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            sb = new Beam();
            sb.closeProfilesWindow();
            sb = null;
        }

        private void trProfiles_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var lt = e.NewValue;
           //Dictionary<string,List<ProfileNames>> ds=lt as Dictionary<string,List<ProfileNames>>;
           // MessageBox.Show(ds.First().Key.ToString());

           System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.List<ProfileNames>> ls = (System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.List<ProfileNames>>)lt;
         //MessageBox.Show(ls.Key);
         selectedPfname = ls.Key;
           //Dictionary<string,List<ProfileNames>> ds =e.NewValue as Dictionary<string,List<ProfileNames>>;
           MessageBox.Show(e.NewValue.ToString());
       
        }
    }
}
