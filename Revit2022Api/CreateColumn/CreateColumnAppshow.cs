using Autodesk.Revit.UI;
using Revit2022Api.CreateGrids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit2022Api.CreateColumn
{
    public static class CreateColumnAppshow
    {

        public static CreateColumnWpf formCreateColumn;

        public static void Showform()
        {

            try
            {
                formCreateColumn.Close();
            }
            catch { }



            CreateColumnHandler createColumnHandler = new CreateColumnHandler(); // tao handler 
            ExternalEvent createColumnEvent = ExternalEvent.Create(createColumnHandler); // gan handler vao su kien



            // gan su kien vao form
            formCreateColumn = new CreateColumnWpf(createColumnEvent);
            formCreateColumn.Show();


        }




    }
}
