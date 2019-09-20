using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace testingActcad
{
    /// <summary>
    /// Interaction logic for ucProfiles.xaml
    /// </summary>
    public partial class ucProfiles : UserControl
    {
        private string selectedPfname = "";
        private Beam sb = null;
        private beamproperties bp = null;

        public ucProfiles()
        {
            InitializeComponent();
            getupdate();
        }
        /// <summary>
        /// all the profile values appended to dictionary  assigned to the itemsource of treeview
        /// </summary>
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
        /// <summary>
        /// assigns the selected profile to the Beam class variable ProfileName and closes the profile window 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            sb = new Beam();

            sb.ProfileName = selectedPfname;
            sb.closeProfilesWindow();
            sb = null;
        }
        /// <summary>
        /// Closes the profiles window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            sb = new Beam();
            sb.closeProfilesWindow();
            sb = null;
        }
        /// <summary>
        /// this is triggered on selecting the treeview items and assigned to  local variable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trProfiles_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var lt = e.NewValue;

            System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.List<ProfileNames>> ls = (System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.List<ProfileNames>>)lt;
            //MessageBox.Show(ls.Key);
            selectedPfname = ls.Key;
        }
    }
}