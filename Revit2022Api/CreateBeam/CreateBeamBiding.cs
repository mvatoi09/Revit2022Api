using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;

namespace Revit2022Api.CreateBeam
{
    [Transaction(TransactionMode.Manual)]
    public class CreateBeamBiding : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData,
            ref string message, ElementSet elements)

        {
            CreateBeamAppshow.Showform();

            // show type tren form

            Document doc = commandData.Application.ActiveUIDocument.Document;


            var collectionFamilyName = new FilteredElementCollector(doc)
                .OfClass(typeof(Family)).Cast<Family>().Where(x => x.FamilyCategory != null &&
                x.FamilyCategory.Id.IntegerValue == (int)BuiltInCategory.OST_StructuralFraming);

            // truy cap den form,gan du lieu vao combobox
            CreateBeamAppshow.formCreateBeam.ComboBoxFamilyName.ItemsSource = collectionFamilyName;



                


            return Result.Succeeded;
        }
    }
}
