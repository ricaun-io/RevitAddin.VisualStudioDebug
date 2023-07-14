using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.VisualStudioDebug.Services;
using System;
using System.Diagnostics;

namespace RevitAddin.VisualStudioDebug.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            using (new VisualStudioAttach())
            {
                Console.WriteLine(uiapp);
                //System.Windows.MessageBox.Show(uiapp.Application.VersionName);
            }

            return Result.Succeeded;
        }
    }
}
