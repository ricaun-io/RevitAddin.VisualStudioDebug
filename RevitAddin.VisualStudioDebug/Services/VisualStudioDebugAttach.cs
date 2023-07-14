using System;
using System.Diagnostics;

namespace RevitAddin.VisualStudioDebug.Services
{
    [DebuggerStepThrough]
    public class VisualStudioDebugAttach : IDisposable
    {
        private Process process;
        private bool IsDebuggerAttached;

        public VisualStudioDebugAttach(Process process = null)
        {
            IsDebuggerAttached = VisualStudioDebugUtils.IsDebuggerAttached;
            VisualStudioDebugUtils.IsDebuggerAttached = true;
            this.process = process ?? Process.GetCurrentProcess();
            this.process.AttachDTE();
        }

        public void Dispose()
        {
            this.process.DetachDTE();
            VisualStudioDebugUtils.IsDebuggerAttached = IsDebuggerAttached;
        }
    }

}