using IntelliCAD.ApplicationServices;
using IntelliCAD.EditorInput;
using IntelliCAD.Runtime;
//using IntelliCAD.Windows;
using Teigha.DatabaseServices;
using Teigha.GraphicsInterface;
using Teigha.Geometry;
using Teigha.Runtime;

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Windows;
using System.Windows.Controls;

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

    public class Beam
    {
        private Document doc = IntelliCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
        private Database db = IntelliCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database;
        private Editor ed = IntelliCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;

        
        private beamproperties bp;
        
        public BeamPropertiesWindow bpw;
        public ColumnPropertiesWindow cpw;
        
        private List<ProfileNames> pflist;
       
        public Dictionary<string,List<ProfileNames>> dict;
        public string Name = "";
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

        public void ReadProfiles()
        {
            try
            {
                string projectFolder = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;
                string textFile = Path.Combine(projectFolder, "DB") + "\\CHINA-PROFILES.lis";
                string xmlfile = Path.Combine(projectFolder, "DB") + "\\CHINA-PROFILES.xml"; ;
                ReadXmlProfiles(xmlfile);
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
        /// reads the xml file and appends the profileNames list
        /// </summary>
        /// <param name="xmlfile"></param>

        public void ReadXmlProfiles(string xmlfile)
        {
            try
            {
                bool wbool = false;

                pflist = new List<ProfileNames>();
                using (XmlReader reader = XmlReader.Create(@xmlfile))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                           
                            switch (reader.Name.ToString())
                            {
                                case "ProfileName":
                                    ProfileName = reader.ReadString();
                                    break;

                                case "Height":
                                    Ht = reader.ReadString();
                                    break;

                                case "Width":
                                    Wd = reader.ReadString();
                                    break;

                                case "Web_thickness":
                                    webthk = reader.ReadString();
                                    break;

                                case "Flange_thickness":
                                    flangethk = reader.ReadString();
                                    break;

                                case "weight_per_unit_length":
                                    wt = reader.ReadString();
                                    wbool = true;
                                    break;
                            }
                        }
                        if (wbool == true)
                        {
                            wbool = false;
                            var ProfileNames = new ProfileNames { ProfileName = ProfileName, Height = Ht, Width = Wd, Flange_thickness = flangethk, weight_per_unit_length = wt, Web_thickness = webthk };
                            pflist.Add(ProfileNames);
                            ProfileName = "";
                            Ht = "";
                            Wd = "";
                            flangethk = "";
                            webthk = "";
                        }
                    }
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

        /// <summary>
        /// This command is used to write xml files
        /// </summary>
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
                if (bpw==null)
                {
                    bpw = new BeamPropertiesWindow();
                }
                
               IntelliCAD.ApplicationServices.Application.ShowModelessDialog(bpw);
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

        /// <summary>
        /// updates profile data values if profilename matches with the profilenames list
        /// </summary>
        internal void ModifyBeamProperties()
        {
            try
            {
                if (ProfileName != "")
                {
                    foreach (ProfileNames pf in pflist)
                    {
                        if (pf.ProfileName == ProfileName)
                        {
                            pf.Material = Material;
                            pf.finish = finish;
                            pf.color = clr;
                            pf.depth = depth;
                            pf.plane = plane;
                            pf.rotation = rotation;
                            pf.depth_txt = depth_txt;
                            pf.rotation_txt = rotation_txt;
                            pf.plane_txt = plane_txt;
                            pf.top_txt = Top_txt;
                            pf.bottom_txt = Bottom_txt;
                        }
                    }
                }
            }
            catch (Teigha.Runtime.Exception ex)
            {
                IntelliCAD.ApplicationServices.Application.ShowAlertDialog(ex.ToString());
               
            }
        }
        /// <summary>
        /// closes the beamproperties window
        /// </summary>
        internal void close()
        {
            var count = System.Windows.Forms.Application.OpenForms.Count;
            for (int i = System.Windows.Forms.Application.OpenForms.Count - 1; i >= 0; i--)
            {
                //if (System.Windows.Forms.Application.OpenForms[i].Name == "BeamPropertiesWindow")
                    System.Windows.Forms.Application.OpenForms[i].Hide();
                
            }
        }

        /// <summary>
        /// get the profile data of the matching profile from the profiles list
        /// </summary>
        internal void GetBeamProperties()
        {
            try
            {
                if (ProfileName != "")
                {
                   
                    foreach (ProfileNames pf in pflist)
                    {
                        if (pf.ProfileName == ProfileName)
                        {
                            Material = pf.Material;
                            finish = pf.finish;
                            clr = pf.color;
                            depth = pf.depth;
                            depth_txt = pf.depth_txt;
                            rotation = pf.rotation;
                            rotation_txt = pf.rotation_txt;
                            plane = pf.plane;
                            plane_txt = pf.plane_txt;
                            Top_txt = pf.top_txt;
                            Bottom_txt = pf.bottom_txt;
                         
                        }
                    }
                }
            }
            catch (Teigha.Runtime.Exception ex)
            {
                IntelliCAD.ApplicationServices.Application.ShowAlertDialog(ex.ToString());
                
            }
        }

        internal void SaveBeamProperties()
        {
            try
            {
                
               
            }
            catch (Teigha.Runtime.Exception ex)
            {
                IntelliCAD.ApplicationServices.Application.ShowAlertDialog(ex.ToString());
               
            }
        }

        

        internal void closeColumn()
        {
            var count = System.Windows.Forms.Application.OpenForms.Count;
            for (int i = System.Windows.Forms.Application.OpenForms.Count - 1; i >= 0; i--)
            {
               // if (System.Windows.Forms.Application.OpenForms[i].Name == "ColumnPropertiesWindow")
                    System.Windows.Forms.Application.OpenForms[i].Hide();
               
            }
        }



        internal void updateVariables()
        {
            try
            {
                string xmlname = beamproperties.xmfile;
                string projectFolder = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;               
                string xmlfile = System.IO.Path.Combine(projectFolder, "DB") + "\\"+ xmlname+"-PROFILES.xml"; 
                XmlDataDocument xmldoc = new XmlDataDocument();
                XmlNodeList xmlnode;
                int i = 0;                
                FileStream fs = new FileStream(xmlfile, FileMode.Open, FileAccess.Read);
                xmldoc.Load(fs);
                xmlnode = xmldoc.GetElementsByTagName("ProfileNames");              
                dict = new Dictionary<string,List<ProfileNames>>();
                for (i = 0; i <= xmlnode.Count - 1; i++)
                {
                    xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                    //str = xmlnode[i].ChildNodes.Item(0).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(1).InnerText.Trim() + "  " + xmlnode[i].ChildNodes.Item(2).InnerText.Trim();
                    //MessageBox.Show(str);
                    ProfileName = xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                    Ht = xmlnode[i].ChildNodes.Item(1).InnerText.Trim();
                    Wd = xmlnode[i].ChildNodes.Item(2).InnerText.Trim();
                    webthk = xmlnode[i].ChildNodes.Item(3).InnerText.Trim();
                    flangethk = xmlnode[i].ChildNodes.Item(4).InnerText.Trim();
                    wt = xmlnode[i].ChildNodes.Item(5).InnerText.Trim();

                    var ProfileNames = new ProfileNames { ProfileName = ProfileName, Height = Ht, Width = Wd, Flange_thickness = flangethk, weight_per_unit_length = wt, Web_thickness = webthk };
                      pflist = new List<ProfileNames>();
                    pflist.Add(ProfileNames);
                    
                    dict.Add(ProfileName, pflist);
                }

            }
            catch (Teigha.Runtime.Exception ex)
            {
                IntelliCAD.ApplicationServices.Application.ShowAlertDialog(ex.ToString());
               
            }

        }

        internal void closeProfilesWindow()
        {
            try
            {
                var count = System.Windows.Forms.Application.OpenForms.Count;
                for (int i = System.Windows.Forms.Application.OpenForms.Count - 1; i >= 0; i--)
                {
                    if (System.Windows.Forms.Application.OpenForms[i].Name == "ucProfilesWindow")
                        System.Windows.Forms.Application.OpenForms[i].Close();
                   
                }
            
                
            }
           
             catch (Teigha.Runtime.Exception ex)
            {
                IntelliCAD.ApplicationServices.Application.ShowAlertDialog(ex.ToString());
                //tr.Abort();
            }

        }
    }
}