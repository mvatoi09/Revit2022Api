using Autodesk.Revit.UI;
using Revit2022Api.CreateBeam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit2022Api.CreateFloor
{
    public static  class CreateFloorAppshow
    {
        public static CreateFloorWpf formCreateFloorWpf;

        public static void Showform()
        {
            try 
            {
                formCreateFloorWpf.Close();
            }

            catch { }

            // tao even tao Floor event
            //CreateFloorHandeler createFloorHandeler =  new CreateFloorHandeler();
            //ExternalEvent createFloorEvent = ExternalEvent.Create(createFloorEvent);


            //formCreateFloorWpf = new CreateFloorWpf();
            //formCreateFloorWpf.Show();



        }

    }
}
