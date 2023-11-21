using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopoEditAddin2022.Button;

namespace Revit2022Api
{
    internal class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {

            new CreateGridButton().GridButton(a);
            new CreateBeamButton().BeamButton(a);
            new CreateFloorButton().FloorButton(a); 
            new GetGeometryButton().GeometyButton(a);
            new CreateColumnButton().ColumnButton(a);
            new BeamTypeExcelBidingButton().BeamTypeExcel(a);
            
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
