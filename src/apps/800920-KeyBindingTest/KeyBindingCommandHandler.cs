using Microsoft.VisualStudio.Commanding;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Editor.Commanding.Commands;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace KeyBindingTest
{
    [Export(typeof(ICommandHandler))]
    [ContentType("text")]
    [Name("KeyBindingTest")]
    internal class KeyBindingCommandHandler : ICommandHandler<TypeCharCommandArgs>
    {
        public string DisplayName => "KeyBindingTest";

        /// <summary>
        /// Defines the adornment layer for the adornment. This layer is ordered
        /// after the selection layer in the Z-order
        /// </summary>
        [Export(typeof(AdornmentLayerDefinition))]
        [Name("PurpleCornerBox")]
        [Order(After = PredefinedAdornmentLayers.Selection, Before = PredefinedAdornmentLayers.Text)]
        private AdornmentLayerDefinition editorAdornmentLayer;

        public bool ExecuteCommand(TypeCharCommandArgs args, CommandExecutionContext executionContext)
        {
            if (args.TypedChar == '+')
            {
                bool alreadyAdorned = args.TextView.Properties.TryGetProperty(
                    "KeyBindingTextAdorned", out bool adorned) && adorned;
                if (!alreadyAdorned)
                {
                    new KeyBindingTest((IWpfTextView)args.TextView);
                    args.TextView.Properties.AddProperty("KeyBindingTextAdorned", true);
                }
            }

            return false;
        }

        public CommandState GetCommandState(TypeCharCommandArgs args)
        {
            return CommandState.Unspecified;
        }
    }
}
