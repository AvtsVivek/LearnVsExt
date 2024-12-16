using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;

namespace TextVersionIntro
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
    [Guid("9108213d-67b7-4936-bd1e-c6ad319ee1ab")]
    public class TextVersionToolWindow : ToolWindowPane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextVersionToolWindow"/> class.
        /// </summary>
        public TextVersionToolWindow() : base(null)
        {
            this.Caption = "TextVersionToolWindow";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            this.Content = new TextVersionToolWindowControl();
        }
    }
}
