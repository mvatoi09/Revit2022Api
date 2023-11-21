using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Revit2022Api.BeamTypeExcel
{
    [Transaction(TransactionMode.Manual)]
    public class BeamTypeExcelBiding : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            Document doc = commandData.Application.ActiveUIDocument.Document;

            var formDialogShow= new System.Windows.Forms.OpenFileDialog();
            formDialogShow.Filter= "Excel Files|*.xls;*.xlsx;*.xlsm";
            formDialogShow.ShowDialog();

            string file = formDialogShow.FileName;
            
            if(File.Exists(file) )
            {
                FileInfo existingFile = new FileInfo(file);
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    //get the first worksheet in the workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    int rowCount = worksheet.Dimension.End.Row;     //get row count

                    for (int row = 1; row <= rowCount; row++)
                    {
                        double b = (double)worksheet.Cells[row + 1, 1].Value;
                    }

                   
                }



            }


            return Result.Succeeded;
        }
    }
}
