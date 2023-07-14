using EnvDTE;
using System.Diagnostics;
using System.Linq;

namespace RevitAddin.VisualStudioDebug.Services
{
    /// <summary>
    /// VisualStudioDebugUtils 
    /// </summary>
    /// <remarks>
    /// <![CDATA[<PackageReference Include="Microsoft.VisualStudio.Interop" Version="*">]]>
    /// </remarks>
    [DebuggerStepThrough]
    public static class VisualStudioDebugUtils
    {
        public static DTE DTE { get; set; } = GetDTE();

        public static bool IsDebuggerAttached { get; set; } = System.Diagnostics.Debugger.IsAttached;

        public static System.Diagnostics.Process AttachDTE(this System.Diagnostics.Process process, DTE dte = null)
        {
            if (IsDebuggerAttached == false) return process;
            dte = dte ?? DTE;
            try
            {
                var processDTE = process.GetProcessDTE(dte);
                processDTE?.Attach();
                Debug.WriteLine($"DTE.Debugger.Attach[{process.Id}]: {dte?.Name} {dte?.Version}");
            }
            catch { }
            return process;
        }

        public static System.Diagnostics.Process DetachDTE(this System.Diagnostics.Process process, DTE dte = null)
        {
            if (IsDebuggerAttached == false) return process;
            dte = dte ?? DTE;
            try
            {
                var processDTE = process.GetProcessDTE(dte);
                processDTE?.Detach(false);
                Debug.WriteLine($"DTE.Debugger.Detach[{process.Id}]: {dte?.Name} {dte?.Version}");
            }
            catch { }
            return process;
        }

        internal static EnvDTE.Process GetProcessDTE(this System.Diagnostics.Process process, DTE dte)
        {
            return dte.Debugger.LocalProcesses.OfType<EnvDTE.Process>().FirstOrDefault(e => e.ProcessID == process.Id);
        }

        internal static DTE GetDTE(int versionMax = 23, int versionMin = 9)
        {
            for (int i = versionMax; i >= versionMin; i--)
            {
                try
                {
                    var objectDTE = $"VisualStudio.DTE.{i}.0";
                    var dte = (DTE)System.Runtime.InteropServices.Marshal.GetActiveObject(objectDTE);
                    if (dte is not null)
                    {
                        Debug.WriteLine($"DTE.Debugger: {objectDTE}");
                        return dte;
                    }
                }
                catch { }
            }
            return null;
        }

        public static string GetName()
        {
            return DTE.GetName();
        }

        public static string GetName(this DTE dte)
        {
            if (dte is null) return null;
            return string.Format("{0} {1}", dte.Name, dte.Version);
        }
    }

}