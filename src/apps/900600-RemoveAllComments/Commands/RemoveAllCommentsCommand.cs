using EnvDTE;
using EnvDTE80;
using Microsoft;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using RemoveAllComments.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Task = System.Threading.Tasks.Task;
using System.Text.RegularExpressions;

namespace RemoveAllComments.Commands
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class RemoveAllCommentsCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("52dc7de1-f658-46de-b1b8-a52489ce61ed");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveAllCommentsCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private RemoveAllCommentsCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new OleMenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static RemoveAllCommentsCommand Instance
        {
            get;
            private set;
        }

        public static DTE2 DteInstance
        {
            get;
            private set;
        }

        // Todo: Do we need this? Need to check 
        // private static OleMenuCommandService CommandService { get; set; }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in RemoveAllCommentsCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            //CommandService = commandService;

            Instance = new RemoveAllCommentsCommand(package, commandService);
            DteInstance = await package.GetServiceAsync(typeof(DTE)) as DTE2;
            Assumes.Present(DteInstance);

        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            
            var wpfTextView = ProjectHelpers.GetCurentWpfTextView();
            
            if (wpfTextView == null)
            {
                // Show a message box to prove we were here
                VsShellUtilities.ShowMessageBox(
                    this.package,
                    "The View is null. Possibly no text file is not open",
                    "Info",
                    OLEMSGICON.OLEMSGICON_INFO,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return;
            }

            var mappingSpans = GetClassificationSpans(wpfTextView, "comment");

            if (!mappingSpans.Any())
            {
                // Show a message box to prove we were here
                VsShellUtilities.ShowMessageBox(
                    this.package,
                    "There are no comments in this document.",
                    "Info",
                    OLEMSGICON.OLEMSGICON_INFO,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return;
            }


            try
            {
                var button = (OleMenuCommand)sender;
                DteInstance.UndoContext.Open(button.Text);

                DeleteFromBuffer(wpfTextView, mappingSpans);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
            }
            finally
            {
                DteInstance.UndoContext.Close();
            }
        }

        private static void DeleteFromBuffer(IWpfTextView wpfTextView, IEnumerable<IMappingSpan> mappingSpans)
        {
            var affectedLines = new List<int>();

            RemoveCommentSpansFromBuffer(wpfTextView, mappingSpans, affectedLines);
            RemoveAffectedEmptyLines(wpfTextView, affectedLines);
        }

        private static IEnumerable<IMappingSpan> GetClassificationSpans(IWpfTextView wpfTextView, string classificationName)
        {
            if (wpfTextView == null)
            {
                return Enumerable.Empty<IMappingSpan>();
            }

            var componentModel = ProjectHelpers.GetComponentModel();
            var bufferTagAggregatorFactoryService = componentModel.GetService<IBufferTagAggregatorFactoryService>();
            ITagAggregator<IClassificationTag> classifier = bufferTagAggregatorFactoryService.CreateTagAggregator<IClassificationTag>(wpfTextView.TextBuffer);
            var snapshot = new SnapshotSpan(wpfTextView.TextBuffer.CurrentSnapshot, 0, wpfTextView.TextBuffer.CurrentSnapshot.Length);

            return from s in classifier.GetTags(snapshot).Reverse()
                   where s.Tag.ClassificationType.Classification.IndexOf(classificationName, StringComparison.OrdinalIgnoreCase) > -1
                   select s.Span;
        }

        private static void RemoveCommentSpansFromBuffer(IWpfTextView wpfTextView, IEnumerable<IMappingSpan> mappingSpans, IList<int> affectedLines)
        {
            using (var edit = wpfTextView.TextBuffer.CreateEdit())
            {
                foreach (var mappingSpan in mappingSpans)
                {
                    var start = mappingSpan.Start.GetPoint(wpfTextView.TextBuffer, PositionAffinity.Predecessor).Value;
                    var end = mappingSpan.End.GetPoint(wpfTextView.TextBuffer, PositionAffinity.Successor).Value;

                    var span = new Span(start, end - start);
                    var lines = wpfTextView.TextBuffer.CurrentSnapshot.Lines.Where(l => l.Extent.IntersectsWith(span));

                    foreach (var line in lines)
                    {
                        if (IsXmlDocComment(line))
                        {
                            edit.Replace(line.Start, line.Length, string.Empty.PadLeft(line.Length));
                        }

                        if (!affectedLines.Contains(line.LineNumber))
                            affectedLines.Add(line.LineNumber);
                    }

                    var mappingText = wpfTextView.TextBuffer.CurrentSnapshot.GetText(span.Start, span.Length);
                    string empty = Regex.Replace(mappingText, "([\\S]+)", string.Empty);

                    edit.Replace(span.Start, span.Length, empty);
                }

                edit.Apply();
            }
        }

        private static void RemoveAffectedEmptyLines(IWpfTextView view, IList<int> affectedLines)
        {
            if (!affectedLines.Any())
                return;

            using (var edit = view.TextBuffer.CreateEdit())
            {
                foreach (var lineNumber in affectedLines)
                {
                    var line = view.TextBuffer.CurrentSnapshot.GetLineFromLineNumber(lineNumber);

                    if (IsLineEmpty(line))
                    {
                        // Strip next line if empty
                        if (view.TextBuffer.CurrentSnapshot.LineCount > line.LineNumber + 1)
                        {
                            var next = view.TextBuffer.CurrentSnapshot.GetLineFromLineNumber(lineNumber + 1);

                            if (IsLineEmpty(next))
                                edit.Delete(next.Start, next.LengthIncludingLineBreak);
                        }

                        edit.Delete(line.Start, line.LengthIncludingLineBreak);
                    }
                }

                edit.Apply();
            }
        }
        private static bool IsLineEmpty(ITextSnapshotLine line)
        {
            var text = line.GetText().Trim();

            return (string.IsNullOrWhiteSpace(text)
                   || text == "<!--"
                   || text == "-->"
                   || text == "<%%>"
                   || text == "<%"
                   || text == "%>"
                   || Regex.IsMatch(text, @"<!--(\s+)?-->"));
        }

        private static bool IsXmlDocComment(ITextSnapshotLine line)
        {
            var text = line.GetText().Trim();

            Microsoft.VisualStudio.Utilities.IContentType contentType = line.Snapshot.TextBuffer.ContentType;

            if (contentType.IsOfType("CSharp") && text.StartsWith("///"))
            {
                return true;
            }

            if (contentType.IsOfType("FSharp") && text.StartsWith("///"))
            {
                return true;
            }

            if (contentType.IsOfType("Basic") && text.StartsWith("'''"))
            {
                return true;
            }

            return false;
        }

    }
}
