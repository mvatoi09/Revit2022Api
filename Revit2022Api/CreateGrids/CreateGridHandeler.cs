
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit2022Api.CreateGrids
{
    public class CreateGridHandeler : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            Document doc = app.ActiveUIDocument.Document;

            //lay gia tri input tu form,lay gia gia tri nhap vao 2 textbox
            // nhap theo cu phap la 2000x4+3000x2

            string txtX = CreateGridAppshow.formCreateGrid.txtAsisX.Text;
            string txtY=CreateGridAppshow.formCreateGrid.txtAsisY.Text;

            // cat chuoi string bang dau "+"

            string[] listTxtX = txtX.Split('+');  ////---> thanh list phan tu thu [0] la 2000x4,[1] la 3000x2
            string[] listTxtY = txtY.Split('+');

            // voi moi string split ra  tu dau "+"thi tao  list tong chieu dai theo truc X
            // va so phan tu trong list do. vi du 2000x4 thi listnumber la 4
            // 
            List<double> listLengthX = new List<double>();
            List<int> listNumberX= new List<int>();
            List<double> listLengthY = new List<double>();
            List<int> listNumberY = new List<int>();

            // tao 1 chieu dau tong theo truc X
            double totalLengthX = 0;
            foreach(var item in listTxtX)
            {
                string[] stringItem = item.Split('x');      ////---> thanh list phan tu thu [0] la 2000 va ep kieu double 2000
                double length = double.Parse(stringItem[0])/304.8;

                int count = int.Parse(stringItem[1]);  ////---> thanh list phan tu thu [1] la 4 va ep kieu thanh int

                 totalLengthX= totalLengthX+(length*count); // tinh tong chieu khoang cach truc X

                listLengthX.Add(length); // add chieu mot doan vao list da tao o tren
                listNumberX.Add(count); // add so luong span giong nhau tinh tu dau "x4"
            }

            // tao 1 chieu dau tong theo truc Y
            double totalLengthY = 0;
            foreach (var item in listTxtY)
            {
                string[] stringItem = item.Split('x');      ////---> thanh list phan tu thu [0] la 2000 va ep kieu double 2000
                double length = double.Parse(stringItem[0]) / 304.8;

                int count = int.Parse(stringItem[1]);  ////---> thanh list phan tu thu [1] la 4 va ep kieu thanh int

                totalLengthY = totalLengthY + (length * count); // tinh tong chieu khoang cach truc X

                listLengthY.Add(length); // add chieu mot doan vao list da tao o tren
                listNumberY.Add(count); // add so luong span giong nhau tinh tu dau "x4"
            }

            // tao hai point de tao Line. tinh tu diem goc xyz=0
            XYZ pX = new XYZ(0, totalLengthY, 0);
            XYZ pY = new XYZ(totalLengthX,0, 0);
            using (Transaction t = new Transaction(doc,"CreateGrids"))
            {

                t.Start();

                Grid new0GridsX=Grid.Create(doc,Line.CreateBound(XYZ.Zero,pX));
                double totalLengthItem = 0;
                for( int i = 0;i<listLengthX.Count;i++) 
                {
                    for(int j = 1;j<=listLengthX.Count;j++)
                    {

                        totalLengthItem = totalLengthItem + listLengthX[i];
                        XYZ p1 = new XYZ(totalLengthItem, 0, 0);
                        XYZ p2 = new XYZ(totalLengthItem,totalLengthY, 0);
                        Grid new1Grids = Grid.Create(doc, Line.CreateBound(p1,p2));
                    }
                }

                Grid new0GridsY = Grid.Create(doc, Line.CreateBound(XYZ.Zero, pY));
                double totalLengthItemY = 0;
                for (int i = 0; i < listLengthY.Count; i++)
                {
                    for (int j = 1; j <= listLengthX.Count; j++)
                    {

                        totalLengthItemY = totalLengthItemY + listLengthY[i];
                        XYZ p1 = new XYZ(0, totalLengthItemY, 0);
                        XYZ p2 = new XYZ(totalLengthX, totalLengthItemY, 0);
                        Grid new1Grids = Grid.Create(doc, Line.CreateBound(p1, p2));
                    }
                }


                t.Commit();


            }



        }

        public string GetName()
        {
            return " creategridhandler";
        }
    }
}
