using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit2022Api.CreateFloor
{
    public class CreateFloorHandeler : IExternalEventHandler
    {
        void IExternalEventHandler.Execute(UIApplication app)
        {
            Document doc = app.ActiveUIDocument.Document;






        }

        string IExternalEventHandler.GetName()
        {
            return "CreateFloorHandeler";
        }
    }
}