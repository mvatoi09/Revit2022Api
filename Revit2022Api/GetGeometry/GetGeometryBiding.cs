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
    public class GetGeometryBiding : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData,
            ref string message, ElementSet elements)

        {

            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            // show type tren form    
            // geometry cua beam column foundation giong nhau

            var selectId = uidoc.Selection.GetElementIds().FirstOrDefault();
            FamilyInstance familyInstance = doc.GetElement(selectId) as FamilyInstance;

            Options options = new Options();
            //options.DetailLevel = ViewDetailLevel.Fine;
            options.IncludeNonVisibleObjects = false;  // bao gom doi tuong khong hien thi
            options.ComputeReferences = true;  // Reference de Dim kich thuoc
            options.View = doc.ActiveView;

            List<Solid> listSolids = new List<Solid>();
            GeometryElement geometryElement = familyInstance.get_Geometry(options);
            foreach (GeometryObject geoObj in geometryElement)
            {
                Solid solid = geoObj as Solid;
                if (solid != null && solid.Volume > 0)
                {
                    listSolids.Add(solid);
                }
                else
                {
                    GeometryInstance geoInst = geoObj as GeometryInstance;
                    if (geoInst != null)
                    {
                        var geoElementInstance = geoInst.GetInstanceGeometry();
                        foreach (GeometryObject geoObjOfInstsance in geoElementInstance)
                        {
                            Solid solidInst = geoObjOfInstsance as Solid;
                            if (solidInst != null && solidInst.Volume > 0)
                            {
                                listSolids.Add(solid);
                            }
                        }
                    }
                }
            }

            // topface

            PlanarFace topPlanarFace = null;
            PlanarFace bottomPlanarFace = null;
            PlanarFace sideFace1 = null;
            PlanarFace sideFace2 = null;

            LocationCurve locationBeam = familyInstance.Location as LocationCurve;
            if (locationBeam == null) return Result.Succeeded;

            Curve curveBeam = locationBeam.Curve;
            Line lineBeam = curveBeam as Line;
            if (lineBeam == null) return Result.Succeeded;
            XYZ directionBeam = lineBeam.Direction.Normalize();
            XYZ normalSideFace = directionBeam.CrossProduct(XYZ.BasisZ).Normalize(); // tim vector vuong goc truc z va direction beam
            XYZ normalTopBottomFace = directionBeam.CrossProduct(normalSideFace).Normalize(); // tim vector vuong goc voi vector 2 mat ben



            foreach (Solid solidItem in listSolids)
            {
                if (solidItem.Faces != null)
                {
                    foreach (Face itemFace in solidItem.Faces)
                    {
                        PlanarFace plannarItem = itemFace as PlanarFace;
                        if (plannarItem != null)
                        {
                            double dotProductSideFace = plannarItem.FaceNormal.Normalize().DotProduct(normalSideFace);
                            if (Math.Abs(Math.Abs(dotProductSideFace) - 1) < 0.0001)
                            {
                                if (sideFace1 == null) sideFace1 = plannarItem;
                                else sideFace2 = plannarItem;

                            }
                            double dotProductTopFace = plannarItem.FaceNormal.Normalize().DotProduct(normalTopBottomFace);
                            if (Math.Abs(Math.Abs(dotProductTopFace) - 1) < 0.0001)
                            {
                                double dotProductofZAsix = plannarItem.FaceNormal.Normalize().DotProduct(XYZ.BasisZ);
                                if (dotProductofZAsix > 0) topPlanarFace = plannarItem;
                                else if (dotProductofZAsix < 0) bottomPlanarFace = plannarItem;

                            }

                        }
                    }
                }
            }



            ReferenceArray referenceDimBeam = new ReferenceArray();

            if (doc.ActiveView.UpDirection.Normalize().IsAlmostEqualTo(XYZ.BasisZ, 0.001))
            {
                referenceDimBeam.Append(topPlanarFace.Reference);
                referenceDimBeam.Append(bottomPlanarFace.Reference);
            }
            else
            {
                referenceDimBeam.Append(sideFace1.Reference);
                referenceDimBeam.Append(sideFace2.Reference);
            }
            


            XYZ directionView = doc.ActiveView.ViewDirection.Normalize();
            XYZ directionLineDim = directionView.CrossProduct(directionBeam);
            XYZ middlePoint = (lineBeam.GetEndPoint(0) + lineBeam.GetEndPoint(1)) / 2;
            Line linePutDim = Line.CreateUnbound(middlePoint, directionLineDim);

            using (Transaction t= new Transaction(doc, "CreateDim"))
            {
                t.Start();
                Dimension dim=doc.Create.NewDimension(doc.ActiveView,linePutDim,referenceDimBeam);

                t.Commit();
            }




            return Result.Succeeded;
        }
    }
}
