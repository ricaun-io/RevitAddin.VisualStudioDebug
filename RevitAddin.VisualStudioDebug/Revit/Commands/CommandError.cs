using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitAddin.VisualStudioDebug.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandError : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            int i = 1;
            int j = 0;
            int k = i / j;

            return Result.Succeeded;
        }
    }

}
