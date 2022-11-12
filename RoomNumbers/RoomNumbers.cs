using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomNumbers
{
    [Transaction(TransactionMode.Manual)]
    public class RoomNumbers : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            List<Room> rooms = new FilteredElementCollector(doc)
                .OfClass(typeof(Room))
                .Cast<Room>()
                .ToList();
            Transaction ts = new Transaction(doc);
            ts.Start();
            for (int i = 0; i < rooms.Count; i++)
            {
                rooms[i].get_Parameter(BuiltInParameter.ROOM_NAME).Set($"{i}");
            }
            ts.Commit();

            return Result.Succeeded;
        }
    }
}
