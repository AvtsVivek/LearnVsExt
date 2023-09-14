using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;


namespace RemoveAllComments.Helpers
{
    public static class ProjectHelpers
    {
        public static bool IsKind(this Project project, string kindGuid)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            return project.Kind.Equals(kindGuid, StringComparison.OrdinalIgnoreCase);
        }

        private static IEnumerable<Project> GetChildProjects(Project parent)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            try
            {
                if (!parent.IsKind(ProjectKinds.vsProjectKindSolutionFolder) && parent.Collection == null)  // Unloaded
                {
                    return Enumerable.Empty<Project>();
                }

                if (!string.IsNullOrEmpty(parent.FullName))
                {
                    return new[] { parent };
                }
            }
            catch (COMException)
            {
                return Enumerable.Empty<Project>();
            }

            return parent.ProjectItems
                    .Cast<ProjectItem>()
                    .Where(p =>
                    {
                        ThreadHelper.ThrowIfNotOnUIThread();
                        return p.SubProject != null;
                    })
                    .SelectMany(p =>
                    {
                        ThreadHelper.ThrowIfNotOnUIThread();
                        return GetChildProjects(p.SubProject);
                    });
        }

        public static IWpfTextView GetCurentWpfTextView()
        {
            var componentModel = GetComponentModel();
            
            if (componentModel == null)
                return null;

            var editorAdapter = componentModel.GetService<IVsEditorAdaptersFactoryService>();

            var vsTextView = GetCurrentNativeTextView();

            if (vsTextView == null) 
                return null;

            return editorAdapter.GetWpfTextView(vsTextView);
        }

        public static IVsTextView GetCurrentNativeTextView()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var textManager = (IVsTextManager)ServiceProvider.GlobalProvider.GetService(typeof(SVsTextManager));
            Assumes.Present(textManager);

            var tempInt = textManager.GetActiveView(1, null, out IVsTextView activeView);


            if (activeView == null)
                return null;
            
                // ErrorHandler.ThrowOnFailure(tempInt);
            return activeView;
        }

        public static IComponentModel GetComponentModel()
        {
            return (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));
        }
    }
}
