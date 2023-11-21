using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit2022Api.CreateGrids
{
    public static class CreateGridAppshow
    {
        public static CreateGridWpf formCreateGrid;

        public static void Showform()
        {

            try
            {
                formCreateGrid.Close();
            }
            catch { }


            CreateGridHandeler createGridHandeler = new CreateGridHandeler();
            ExternalEvent createGridEvent = ExternalEvent.Create(createGridHandeler);

            formCreateGrid = new CreateGridWpf(createGridEvent);
            formCreateGrid.Show();  


        }
       

    }
}
