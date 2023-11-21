using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit2022Api.CreateViewFilters
{
    [Transaction(TransactionMode.Manual)]
    public class ViewFilterBiding : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;


            var listIds = uidoc.Selection.GetElementIds();
            List<Category> listCategory= new List<Category>();
            foreach ( ElementId id in listIds ) 
            { 
                Element element = doc.GetElement( id );
                if (element.Category != null)
                {
                    if (!listCategory.Exists(x => x.Id == element.Category.Id))
                    {
                        listCategory.Add(element.Category);
                    }
                }
            }
             List<Parameter> parameters = new List<Parameter>();
            foreach ( Category itemCategory in listCategory)
            {
                var filterCategoryEl= new FilteredElementCollector(doc)
                    .OfCategory((BuiltInCategory)itemCategory.Id.IntegerValue)
                    .WhereElementIsNotElementType().ToElements().FirstOrDefault();

                if(filterCategoryEl != null)
                {
                    var parameterItem = filterCategoryEl.Parameters;

                    foreach (Parameter paraItem in parameterItem)
                    {
                        //if(!parameters.Exists(x=>x.Definition.Name== paraItem.Definition.Name))
                        //{
                        //    parameters.Add(paraItem); 
                        //}

                        parameters.Add(paraItem);
                    }


                    var typeElement=doc.GetElement(filterCategoryEl.GetTypeId());
                    if (parameterItem != null)
                    {
                        var parameterType = typeElement.Parameters;

                        foreach (Parameter paraType in parameterType)
                        {
                            //if (!parameters.Exists(x => x.Definition.Name == paraType.Definition.Name))
                            //{
                            //    parameters.Add(paraType);
                            //}

                            parameters.Add(paraType);
                        }
                    }
                }

            }

            List<Parameter> listParameterResult= new List<Parameter>();
            if (listCategory.Count == 1) listParameterResult = parameters;
            else
            {
                foreach(Parameter para in parameters)
                {
                    int countSame = 0;
                    foreach(var para2 in parameters)
                    {
                        if (para.Id == para2.Id)
                        {
                            countSame++;
                        }
                    }
                    if (countSame == listCategory.Count)
                    {
                        if (!listParameterResult.Exists(x => x.Id == para.Id))
                        {
                            listParameterResult.Add(para);
                        }
                      
                    }

                }
            }

            var form = new CreateViewFiltersWpf();
            form.comboboxParameter.ItemsSource = listParameterResult;
            form.ShowDialog();


            return Result.Succeeded;

        }
    }
}
