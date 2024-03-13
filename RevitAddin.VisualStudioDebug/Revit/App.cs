using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.VisualStudioDebug.Services;
using ricaun.Revit.UI;
using ricaun.Revit.UI.Drawing;
using System;

namespace RevitAddin.VisualStudioDebug.Revit
{
    [AppLoader]
    public class App : IExternalApplication
    {
        private static RibbonPanel ribbonPanel;
        private static VisualStudioDebugAttach visualStudioDebugAttach;
        public Result OnStartup(UIControlledApplication application)
        {
            //Console.WriteLine(application);
            //visualStudioDebugAttach = new VisualStudioDebugAttach();

            ribbonPanel = application.CreatePanel("Debug");

            //var button = ribbonPanel.CreatePushButton<Commands.Command>()
            //    .SetLargeImage(Properties.Resources.Revit.GetBitmapSource());

            //ribbonPanel.SetDialogLauncher(button);

            var startButton = ribbonPanel.CreatePushButton<CommandPlay>("Start")
                .SetLargeImage("Resources/Play-Light.ico")
                .SetToolTip("Start Debugging using Visual Studio process.");

            var eventButton = ribbonPanel.CreatePushButton<CommandEvent>("Event")
                .SetLargeImage("Resources/Event-Light.ico")
                .SetToolTip("Start Debugging using Visual Studio process when AssemblyLoad event happen.");

            var stopButton = ribbonPanel.CreatePushButton<CommandStop>("Stop")
                .SetLargeImage("Resources/Stop-Light.ico")
                .SetToolTip("Stop Debugging using Visual Studio process.");

            ribbonPanel.RowStackedItems(
                startButton,
                eventButton,
                //ribbonPanel.CreatePushButton<CommandPause>("Pause").SetLargeImage("Resources/Pause-Light.ico"),
                stopButton
            );

            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;

            return Result.Succeeded;
        }

        private void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            if (EventLoad)
            {
                EventLoad = false;
                visualStudioDebugAttach?.Dispose();
                visualStudioDebugAttach = new VisualStudioDebugAttach();
            }
        }

        private static bool EventLoad;

        private static void EventMonitor()
        {
            EventLoad = !EventLoad;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();
            visualStudioDebugAttach?.Dispose();

            AppDomain.CurrentDomain.AssemblyLoad -= CurrentDomain_AssemblyLoad;

            return Result.Succeeded;
        }


        [Transaction(TransactionMode.Manual)]
        public class CommandPlay : IExternalCommand, IExternalCommandAvailability
        {
            public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
            {
                UIApplication uiapp = commandData.Application;
                visualStudioDebugAttach?.Dispose();
                visualStudioDebugAttach = new VisualStudioDebugAttach();

                return Result.Succeeded;
            }

            public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
            {
                return System.Diagnostics.Debugger.IsAttached == false && EventLoad == false;
            }
        }

        [Transaction(TransactionMode.Manual)]
        public class CommandEvent : IExternalCommand, IExternalCommandAvailability
        {
            public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
            {
                UIApplication uiapp = commandData.Application;

                EventMonitor();

                return Result.Succeeded;
            }

            public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
            {
                return System.Diagnostics.Debugger.IsAttached == false;
            }
        }

        [Transaction(TransactionMode.Manual)]
        public class CommandPause : IExternalCommand, IExternalCommandAvailability
        {
            public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
            {
                UIApplication uiapp = commandData.Application;

                VisualStudioDebugUtils.DTE.Debugger.Break();

                return Result.Succeeded;
            }

            public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
            {
                return visualStudioDebugAttach is not null;
            }
        }

        [Transaction(TransactionMode.Manual)]
        public class CommandStop : IExternalCommand, IExternalCommandAvailability
        {
            public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
            {
                UIApplication uiapp = commandData.Application;

                VisualStudioDebugUtils.DTE.Debugger.Stop();

                visualStudioDebugAttach?.Dispose();
                visualStudioDebugAttach = null;

                return Result.Succeeded;
            }

            public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
            {
                return System.Diagnostics.Debugger.IsAttached;
            }
        }

    }

}