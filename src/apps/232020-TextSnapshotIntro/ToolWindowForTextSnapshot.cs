using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;

namespace TextSnapshotIntro
{
    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("6a265257-8b29-40c1-ace0-047360411a44")]
    public class ToolWindowForTextSnapshot : ToolWindowPane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolWindowForTextSnapshot"/> class.
        /// </summary>
        public ToolWindowForTextSnapshot() : base(null)
        {
            this.Caption = "ToolWindowForTextSnapshot";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            this.Content = new ToolWindowForTextSnapshotControl();
        }
    }
}
