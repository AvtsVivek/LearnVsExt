﻿using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using System.Linq;
using System.Collections.Generic;

namespace TagAggregatorIntro
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class ShowTagsCountCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("9eb4e394-4fa4-4935-950c-77d031308bf3");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShowTagsCountCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private ShowTagsCountCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static ShowTagsCountCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
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
            // Switch to the main thread - the call to AddCommand in ShowTagsCountCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new ShowTagsCountCommand(package, commandService);
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

            var vsTextManager = GetGlobalService<IVsTextManager>(typeof(SVsTextManager));

            int mustHaveFocus = 1;

            vsTextManager.GetActiveView(mustHaveFocus, null, out IVsTextView vsTextView);

            if (vsTextView == null)
            {
                VsShellUtilities.ShowMessageBox(
                    package,
                    "No text view is currently open. Probably no text file is open. Open any text file and try again.",
                    "No text view!!",
                    OLEMSGICON.OLEMSGICON_INFO,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

                return;
            }

            var componentModel = GetGlobalService<IComponentModel>(typeof(SComponentModel));

            var vsEditorAdaptersFactoryService = componentModel.GetService<IVsEditorAdaptersFactoryService>();

            var wpfTextView = vsEditorAdaptersFactoryService.GetWpfTextView(vsTextView);

            var classificationTypeRegistryService = componentModel.GetService<IClassificationTypeRegistryService>();

            var currentTextSnapShot = wpfTextView.TextBuffer.CurrentSnapshot;

            var currentSnapShotSpan = new Span(0, currentTextSnapShot.Length);

            var normalizedSnapshotSpanCollection = new NormalizedSnapshotSpanCollection(currentTextSnapShot, currentSnapShotSpan);

            var tagAggregatorFactoryService = componentModel.GetService<IBufferTagAggregatorFactoryService>();

            var tagAggregator = tagAggregatorFactoryService.CreateTagAggregator<HelloTagOne>(wpfTextView.TextBuffer);

            var mappingTagSpanList = new List<IMappingTagSpan<HelloTagOne>>();

            foreach (SnapshotSpan span in normalizedSnapshotSpanCollection)
            {
                var tags = tagAggregator.GetTags(span);
                mappingTagSpanList.AddRange(tags);
            }

            //// We can get the tag list from the following as well, where in we directly instanciate the tagger object.
            //// This we have seen in earlier example.
            //var todoTagger = new HelloTagOneTagger();
            //var tagList = todoTagger.GetTags(normalizedSnapshotSpanCollection).ToList();

            VsShellUtilities.ShowMessageBox(
                package,
                $"Todo count in this current document is {mappingTagSpanList.Count}",
                "Show Todo Word Count",
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }
        private static T GetGlobalService<T>(Type serviceType)
        {
            return (T)Package.GetGlobalService(serviceType);
        }
    }
}
