using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit2022Api.CreateBeam
{
    public static class CreateBeamAppshow
    {
        public static CreateBeamWpf formCreateBeam;

        public static void Showform()
        {

            try
            {
                formCreateBeam.Close();
            }
            catch { }

            // tao even tao beam event
            CreateBeamHandeler createBeamHandeler = new CreateBeamHandeler();
            ExternalEvent createBeamEvent = ExternalEvent.Create(createBeamHandeler);

            // tao even get  type beam event

            CreateGetBeamTypeHandeler createGetBeamTypeHandeler =new CreateGetBeamTypeHandeler();
            ExternalEvent createGetTypeBeam=ExternalEvent.Create(createGetBeamTypeHandeler);




            formCreateBeam = new CreateBeamWpf(createBeamEvent,createGetTypeBeam);
            formCreateBeam.Show();


        }


    }
}
