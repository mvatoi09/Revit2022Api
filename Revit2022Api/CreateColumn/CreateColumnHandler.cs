using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit2022Api.CreateColumn
{
    internal class CreateColumnHandler : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {

            Document doc = app.ActiveUIDocument.Document;

            var typeColumn = CreateColumnAppshow.formCreateColumn
                .comboboxColumnType.SelectedItem as FamilySymbol;


            var bottomLevel = CreateColumnAppshow.formCreateColumn
                .comboboxBottomLevel.SelectedItem as Level;

            var topLevel = CreateColumnAppshow.formCreateColumn
                .comboboxTopLevel.SelectedItem as Level;

            using (Transaction t = new Transaction(doc, "create Column"))
            {
                t.Start();

                FamilyInstance column=doc.Create.NewFamilyInstance(new XYZ(0,0,0), typeColumn,bottomLevel,
                    Autodesk.Revit.DB.Structure.StructuralType.Column);

                column.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_PARAM).Set(topLevel.Id);
                //column.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_PARAM);
                //column.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_OFFSET_PARAM).Set(1500/304.8);



                // get parameter cua project parameter thif dung 

                //Parameter para = column.LookupParameter("ten cua parameter muon tim kiem");
                //string value1 = para.AsString();
                //double value2=para.AsDouble();
                //para.Set("string");
                //para.Set(100 / 304.8);

                //IList<Parameter> paras = column.GetParameters("ten cua parameter tim kim");

                t.Commit();

            }
        }

        public string GetName()
        {
            return "CreateColumnHandler";
        }
    }
}
