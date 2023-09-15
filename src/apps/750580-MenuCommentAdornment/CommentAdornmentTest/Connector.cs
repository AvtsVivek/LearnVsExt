using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace CommentAdornmentTest
{
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("text")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    public sealed class Connector : IWpfTextViewCreationListener
    {
        // This method is executed when you open any file in the editor window
        [Export(typeof(AdornmentLayerDefinition))]
        [Name("CommentAdornmentLayer")]
        [Order(After = PredefinedAdornmentLayers.Selection, Before = PredefinedAdornmentLayers.Text)]
        public AdornmentLayerDefinition commentLayerDefinition;
        public void TextViewCreated(IWpfTextView textView)
        {
            CommentAdornmentManager.Create(textView);
        }

        static public void Execute(IWpfTextViewHost wpfTextViewHost)
        {
            IWpfTextView wpfTextView = wpfTextViewHost.TextView;
            //Add a comment on the selected text. 
            if (!wpfTextView.Selection.IsEmpty)
            {
                //Get the provider for the comment adornments in the property bag of the view.
                var commentAdornmentProvider = wpfTextView.Properties.GetProperty<CommentAdornmentProvider>(typeof(CommentAdornmentProvider));

                //Add some arbitrary author and comment text. 
                var author = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                var comment = "Four score....";

                //Add the comment adornment using the provider.
                commentAdornmentProvider.Add(wpfTextView.Selection.SelectedSpans[0], author, comment);
            }
        }
    }
}
