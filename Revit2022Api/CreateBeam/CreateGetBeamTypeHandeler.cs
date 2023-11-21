
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit2022Api.CreateBeam
{
    public class CreateGetBeamTypeHandeler : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            Document doc = app.ActiveUIDocument.Document;
             // chon selected tu form va ep kieu thanh family
            Family family=CreateBeamAppshow.formCreateBeam.ComboBoxFamilyName.SelectedItem as Family;   
            var typeOfFamilyIds=family.GetFamilySymbolIds();

            List<FamilySymbol> familySymbols=new List<FamilySymbol>();
            foreach(var Id in typeOfFamilyIds)
            {
                FamilySymbol symbol=doc.GetElement(Id) as FamilySymbol;
                familySymbols.Add(symbol);
            }


            // gan gia tri laicombobox type
            CreateBeamAppshow.formCreateBeam.ComboBoxTypeBeam.ItemsSource= familySymbols;



        }

        public string GetName()
        {
            return "CreateGetBeamTypeHandeler";
        }
    }
}
