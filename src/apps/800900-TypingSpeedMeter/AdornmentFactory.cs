using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace TypingSpeedMeter
{
    #region Adornment Factory
    /// <summary>
    /// Establishes an <see cref="IAdornmentLayer"/> to place the adornment on and exports the <see cref="IWpfTextViewCreationListener"/>
    /// that instantiates the adornment on the event of a <see cref="IWpfTextView"/>'s creation
    /// </summary>
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("text")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    internal sealed class AdornmentFactory : IWpfTextViewCreationListener
    {
        public AdornmentFactory()
        {
            Debugger.Break();
        }

        /// <summary>
        /// Defines the adornment layer for the scarlet adornment. This layer is ordered 
        /// after the selection layer in the Z-order
        /// </summary>
        [Export(typeof(AdornmentLayerDefinition))]
        [Name("TypingSpeed")]
        [Order(After = PredefinedAdornmentLayers.Caret)]
        [TextViewRole(PredefinedTextViewRoles.Document)]
        public AdornmentLayerDefinition editorAdornmentLayer = null;

        /// <summary>
        /// Creates a PurpleCornerBox adornment manager when a textview is created
        /// </summary>
        /// <param name="textView">The <see cref="IWpfTextView"/> upon which the adornment should be placed</param>
        public void TextViewCreated(IWpfTextView textView)
        {
            Debugger.Break();
            textView.Properties.GetOrCreateSingletonProperty(delegate { return new TypingSpeedMeter(textView); });
        }
    }
    #endregion //Adornment Factory

}
