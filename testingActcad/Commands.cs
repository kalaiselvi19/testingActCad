using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelliCAD.ApplicationServices;
using IntelliCAD.EditorInput;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Teigha.DatabaseServices;
using Teigha.Runtime;
using System.Windows.Input;
using IntelliCAD.Windows;

namespace testingActcad
{
    public class ProfileNames
    {
        public string ProfileName { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string Web_thickness { get; set; }
        public string Flange_thickness { get; set; }
        public string weight_per_unit_length { get; set; }
        public string Profilename { get; set; }
        public string Material { get; set; }
        public string finish { get; set; }
        public string color { get; set; }
        public string plane { get; set; }
        public string rotation { get; set; }
        public string depth { get; set; }
        public string plane_txt { get; set; }
        public string rotation_txt { get; set; }
        public string depth_txt { get; set; }
        public string top_txt { get; set; }
        public string bottom_txt { get; set; }
    }
    public static class values
    {
        public static string ProfileName = "";
    }
    public class Commands
    {
        private Document doc = IntelliCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
        private Database db = IntelliCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database;
        private Editor ed = IntelliCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;


        IntelliCAD.Windows.PaletteSet ps;

        private beamproperties bp;

        public BeamPropertiesWindow bpw;
        public ColumnPropertiesWindow cpw;

        private List<ProfileNames> pflist;

        public Dictionary<string, List<ProfileNames>> dict;
        public string Name = "";
        //public string ProfileName { get; set; }
        public string ProfileName = "";
        public string Material = "";
        public string webthk = "";
        public string flangethk = "";
        public string wt = "";
        public string finish = "";
        public string clr = "";
        public string Ht = "";
        public string Wd = "";
        public string plane = "";
        public string rotation = "";
        public string depth = "";
        public string plane_txt = "";
        public string rotation_txt = "";
        public string depth_txt = "";
        public string Top_txt = "";
        public string Bottom_txt = "";

        ///<summary>
        ///this command brings up the Columnproperties UI
        ///</summary>
        [CommandMethod("column")]
        public void DrawColumn()
        {
            try
            {
                if (cpw == null)
                {
                    cpw = new ColumnPropertiesWindow();
                }

                IntelliCAD.ApplicationServices.Application.ShowModelessDialog(cpw);
            }
            catch (Teigha.Runtime.Exception ex)
            {
                IntelliCAD.ApplicationServices.Application.ShowAlertDialog(ex.ToString());
            }
        }

        /// <summary>
        /// this command brings up the beamproperties UI
        /// </summary>
        [CommandMethod("beam")]
        public void DrawBeam()
        {
            try
            {            

                if (bpw == null)
                {
                    bpw = new BeamPropertiesWindow();
                }
                bpw.ShowDialog();
                IntelliCAD.ApplicationServices.Application.ShowAlertDialog(values.ProfileName);
                
                // IntelliCAD.ApplicationServices.Application.ShowModelessDialog(bpw);
            }
            catch (Teigha.Runtime.Exception ex)
            {
                IntelliCAD.ApplicationServices.Application.ShowAlertDialog(ex.ToString());
            }
            catch (System.Exception objError)
            {
                System.Exception xx = objError;
            }
        }

        /// <summary>
        /// reads the lis files and writes as xml file
        /// txtFile is the .lis file
        /// </summary>
        /// <param name="textFile"></param>

        [CommandMethod("WriteXmlProfiles")]
        public void WriteXmlProfiles()
        {
            try
            {
                string projectFolder = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;
                string textFile = Path.Combine(projectFolder, "DB") + "\\CHINA-PROFILES.lis";
                readProfile(textFile);
                textFile = Path.Combine(projectFolder, "DB") + "\\USIMPERIAL-PROFILES.lis";
                readProfile(textFile);
                textFile = Path.Combine(projectFolder, "DB") + "\\USMETRIC-PROFILES.lis";
                readProfile(textFile);
            }
            catch (Teigha.Runtime.Exception ex)
            {
                IntelliCAD.ApplicationServices.Application.ShowAlertDialog(ex.ToString());
            }
            catch (System.Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
        }
        /// <summary>
        /// This command is used to write xml files
        /// </summary>

        private void readProfile(string textFile)
        {
            try
            {
                string[] lines = File.ReadAllLines(textFile);

                string pfname = "";
                string ht = "";
                string wd = "";
                string fthk = "";
                string wt = "";
                string webthk = "";
                bool wbool = false;
                pflist = new List<ProfileNames>();
                foreach (string line in lines)
                {
                    if (line.StartsWith("PROFILE_NAME") == true)
                    {
                        string[] contents = line.Split(new char[] { '"' });
                        pfname = contents[1];
                    }
                    if (line.Contains("\"HEIGHT\"") == true)
                    {
                        string[] contents = line.Split(new char[] { ' ' });
                        ht = contents[contents.Length - 1];
                    }
                    if (line.Contains("\"WIDTH\"") == true)
                    {
                        string[] contents = line.Split(new char[] { ' ' });
                        wd = contents[contents.Length - 1];
                    }
                    if (line.Contains("\"WEB_THICKNESS\"") == true)
                    {
                        string[] contents = line.Split(new char[] { ' ' });
                        webthk = contents[contents.Length - 1];
                    }
                    if (line.Contains("\"FLANGE_THICKNESS\"") == true)
                    {
                        string[] contents = line.Split(new char[] { ' ' });
                        fthk = contents[contents.Length - 1];
                    }
                    if (line.Contains("\"WEIGHT_PER_UNIT_LENGTH\"") == true)
                    {
                        string[] contents = line.Split(new char[] { ' ' });
                        wt = contents[contents.Length - 1];
                        wbool = true;
                    }
                    if (wbool == true)
                    {
                        wbool = false;
                        var ProfileNames = new ProfileNames { ProfileName = pfname, Height = ht, Width = wd, Flange_thickness = fthk, weight_per_unit_length = wt, Web_thickness = webthk };
                        pflist.Add(ProfileNames);
                    }
                }

                char[] charsToTrim = { '.', 'l', 'i', 's' };
                string xmlFile = Path.Combine(textFile.TrimEnd(charsToTrim)) + ".xml";
                using (FileStream fs = new FileStream(xmlFile, FileMode.Create))
                {
                    new XmlSerializer(typeof(List<ProfileNames>)).Serialize(fs, pflist);
                }
            }
            catch (Teigha.Runtime.Exception ex)
            {
                IntelliCAD.ApplicationServices.Application.ShowAlertDialog(ex.ToString());
            }
            catch (System.Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
        }




    }
}
