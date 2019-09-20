using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelliCAD;
using Teigha;


using IntelliCAD.ApplicationServices;
using IntelliCAD.EditorInput;
using IntelliCAD.Runtime;
using Teigha.DatabaseServices;
using Teigha.GraphicsInterface;
using Teigha.Geometry;
using Teigha.Runtime;

namespace testingActcad
{
    public class Class1
    {

        [CommandMethod("testing")]
        public void testing()
        {
           IntelliCAD.ApplicationServices.Application.ShowAlertDialog("tesing");

        }
    }
}
