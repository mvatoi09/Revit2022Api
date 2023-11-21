using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Revit2022Api.CreateFloor;
using Autodesk.Revit.DB.Architecture;

namespace Revit2022Api.CreateFloor
{
    [Transaction(TransactionMode.Manual)]
    public class CreateFloorBiding : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData,
            ref string message, ElementSet elements)

        {

            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            // show type tren form
            CreateFloorAppshow.Showform();

            var allRoom = new FilteredElementCollector(doc, doc.ActiveView.Id)
                .OfCategory(BuiltInCategory.OST_Rooms)
                .WhereElementIsNotElementType().Cast<Room>().ToList();

            SpatialElementBoundaryOptions optionRoom= new SpatialElementBoundaryOptions();
            optionRoom.SpatialElementBoundaryLocation= new SpatialElementBoundaryLocation();

           

           

           






            return Result.Succeeded;
        }
    }
}
