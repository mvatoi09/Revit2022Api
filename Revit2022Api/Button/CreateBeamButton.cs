using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Windows.Media;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using TopoEditAddin2022;
using Revit2022Api;
using Revit2022Api.Properties;
using Revit2022Api.CreateGrids;

namespace TopoEditAddin2022.Button
{
    public class CreateBeamButton
    {

        public void BeamButton(UIControlledApplication application)
        {

            try
            {
                application.CreateRibbonTab(AppConstants.RibbonName1);  //TAO RIBBON NEU DA TON TAI THU XUONG DUOI HAM CATH 
            }
            catch
            {
            }



            RibbonPanel panelArchitect = null;

            List<RibbonPanel> allPanelOfRevitAPI = application.GetRibbonPanels(AppConstants.RibbonName1);

            foreach (RibbonPanel panelItem in allPanelOfRevitAPI)    //KIEM TRA PANEL DA TON TAI HAY CHUA
            {
                if (panelItem.Name == AppConstants.Panel1)
                {
                    panelArchitect = panelItem;

                    break;
                }
            }

            if (panelArchitect == null)  // tao panel
                panelArchitect = application.CreateRibbonPanel(AppConstants.RibbonName1, AppConstants.Panel1);


            ImageSource imageSource = Extension.GetImageSource(Resources.Beam);

            PushButtonData pushButtonDataTest = new PushButtonData("CreateBeam", "Create\nBeam",
                Assembly.GetExecutingAssembly().Location, "Revit2022Api.CreateBeam.CreateBeamBiding");

            pushButtonDataTest.ToolTip = "CreateBeam\n at grid";
            pushButtonDataTest.LongDescription = "Createbeam";
            pushButtonDataTest.Image = imageSource;
            pushButtonDataTest.LargeImage = imageSource;
            panelArchitect.AddItem(pushButtonDataTest).Enabled = true;

        }




    }
}
