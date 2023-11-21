using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;

namespace Revit2022Api.CreateGrids
{
    [Transaction(TransactionMode.Manual)]
    public class CreateGridBiding : IExternalCommand
    {
        
        public Result Execute(ExternalCommandData commandData, 
            ref string message, ElementSet elements)

        {
            CreateGridAppshow.Showform();


            return Result.Succeeded;
        }
    }
}
