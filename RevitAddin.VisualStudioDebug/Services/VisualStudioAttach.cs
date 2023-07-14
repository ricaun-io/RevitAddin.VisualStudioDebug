using System;
using System.Diagnostics;

namespace RevitAddin.VisualStudioDebug.Services
{
    [DebuggerStepThrough]
    public class VisualStudioAttach : IDisposable
    {
        private Process process;
        private bool IsDebuggerAttached;

        public VisualStudioAttach(Process process = null)
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