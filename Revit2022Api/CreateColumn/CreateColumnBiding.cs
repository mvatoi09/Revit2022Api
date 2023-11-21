using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit2022Api.CreateColumn
{
    [Transaction(TransactionMode.Manual)]
    public class CreateColumnBiding : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            CreateColumnAppshow.Showform();


            var typeColums= new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_StructuralColumns)
                .WhereElementIsElementType().Cast<FamilySymbol>().ToList();



            var allBottomLevels = new FilteredElementCollector(doc, doc.ActiveView.Id)
                .OfCategory(BuiltInCategory.OST_Levels)
                .WhereElementIsNotElementType().Cast<Level>().ToList();

            var allTopLevels = new FilteredElementCollector(doc, doc.ActiveView.Id)
                .OfCategory(BuiltInCategory.OST_Levels)
                .WhereElementIsNotElementType().Cast<Level>().ToList();



            CreateColumnAppshow.formCreateColumn.comboboxColumnType.ItemsSource= typeColums;
            CreateColumnAppshow.formCreateColumn.comboboxBottomLevel.ItemsSource= allBottomLevels;
            CreateColumnAppshow.formCreateColumn.comboboxTopLevel.ItemsSource= allTopLevels;




            return Result.Succeeded;


        }
    }
}
