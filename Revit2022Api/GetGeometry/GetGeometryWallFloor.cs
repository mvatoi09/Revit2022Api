using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Revit2022Api.CreateFloor;
using Autodesk.Revit.DB.Architecture;

namespace Revit2022Api.GetGeometry
{
    [Transaction(TransactionMode.Manual)]
    public class GetGeometryWallFloor : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData,
            ref string message, ElementSet elements)

        {

            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            // show type tren form    
            // geometry cua Wall Floor giong nhau

            var selectId=uidoc.Selection.GetElementIds().FirstOrDefault();
            Wall walls= doc.GetElement(selectId) as Wall;
            LocationCurve locationWall = walls.Location as LocationCurve;
            if (locationWall == null) return Result.Succeeded;
            Line line = locationWall.Curve as Line;
            if(line == null) return Result.Succeeded;

            XYZ lineDirection = line.Direction.Normalize();   // huong cua tuong

            Options options= new Options();
            //options.DetailLevel = ViewDetailLevel.Fine;
            options.IncludeNonVisibleObjects = false;  // bao gom doi tuong khong hien thi
            options.ComputeReferences = true;  // Reference de Dim kich thuoc
            options.View = doc.ActiveView;

            List<Solid> listSolids = new List<Solid>();
            GeometryElement geometryElement = walls.get_Geometry(options);
            foreach (GeometryObject geoObj in geometryElement)
            {
                Solid solid = geoObj as Solid;
                if (solid != null&& solid.Volume>0)
                {
                    listSolids.Add(solid);
                }
            }

            // topface

            PlanarFace topPlanarFace = null;
            PlanarFace bottomPlanarFace = null;
            PlanarFace sideFace1 = null;
            PlanarFace sideFace2 = null;


            foreach (Solid solidItem in listSolids)
            {
                if (solidItem.Faces != null)
                {
                    foreach(Face itemFace in solidItem.Faces)
                    {
                        PlanarFace   plannarItem=itemFace as PlanarFace;
                        if(plannarItem != null)
                        {
                            XYZ normalFace=plannarItem.FaceNormal.Normalize();
                            if (normalFace.IsAlmostEqualTo(XYZ.BasisZ, 0.001))
                            {
                                topPlanarFace = plannarItem;

                            }
                            else if (normalFace.IsAlmostEqualTo(-XYZ.BasisZ, 0.001))
                            {
                                bottomPlanarFace=plannarItem;
                            }
                            else
                            {
                                double dotProduct=normalFace.DotProduct(XYZ.BasisZ);
                                if (Math.Abs(dotProduct) < 0.0001)
                                {
                                    double sideDotProduct = normalFace.DotProduct(lineDirection);
                                    if (Math.Abs(sideDotProduct) < 0.0001)
                                    {
                                        if (sideFace1 == null) sideFace1 = plannarItem;
                                        else sideFace2 = plannarItem;
                                    }
                                }
                            }
                        }
                    }
                }
            }

           

            double zWall = topPlanarFace.Origin.Z*304.8;

            ReferenceArray dimReferenceArray= new ReferenceArray();
            dimReferenceArray.Append(sideFace1.Reference);
            dimReferenceArray.Append(sideFace2.Reference);

            //XYZ midWall = (line.GetEndPoint(0) + line.GetEndPoint(1)) / 2;
            XYZ pickPoint = uidoc.Selection.PickPoint(" Pick Point put Dim");
            XYZ dimDirection=lineDirection.CrossProduct(doc.ActiveView.ViewDirection).Normalize();
            Line dimLine = Line.CreateUnbound(pickPoint, dimDirection);

            using (Transaction t= new Transaction(doc,"Dim wall"))
            {
                t.Start();

                var dimWall = doc.Create.NewDimension(doc.ActiveView, dimLine, dimReferenceArray);

                t.Commit();

            }



            return Result.Succeeded;
        }
    }
}
