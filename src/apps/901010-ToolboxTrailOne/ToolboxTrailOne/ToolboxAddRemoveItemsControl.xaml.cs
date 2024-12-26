using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace ToolboxTrailOne
{
    /// <summary>
    /// Interaction logic for ToolboxAddRemoveItemsControl.
    /// </summary>
    public partial class ToolboxAddRemoveItemsControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolboxAddRemoveItemsControl"/> class.
        /// </summary>
        public ToolboxAddRemoveItemsControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
                "ToolboxAddRemoveItems");
        }

        private void AddToToolBox_Click(object sender, RoutedEventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            IVsToolbox toolbox = (IVsToolbox) Package.GetGlobalService(typeof(SVsToolbox));

            if (toolbox == null)
            {
                MessageBox.Show("Add attempted, but Toolbox is null. Cannot continue",
                "Toolbox null");
                return;
            }

            TBXITEMINFO[] itemInfo = new TBXITEMINFO[1];
            itemInfo[0].bstrText = "Toolbox Sample Item one";
            itemInfo[0].hBmp = IntPtr.Zero;
            itemInfo[0].dwFlags = (uint)__TBXITEMINFOFLAGS.TBXIF_DONTPERSIST;
            var toolboxData = new OleDataObject();

            toolboxData.SetData(typeof(ToolboxItemData), new ToolboxItemData("Test string one"));

            toolbox.AddItem(toolboxData, itemInfo, "Toolbox Test one");

            toolbox.UpdateToolboxUI();
        }

        private void RemoveToToolBox_Click(object sender, RoutedEventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            IVsToolbox toolbox = (IVsToolbox)Package.GetGlobalService(typeof(SVsToolbox));

            if (toolbox == null)
            {
                MessageBox.Show("Add attempted, but Toolbox is null. Cannot continue",
                "Toolbox null");
                return;
            }
            
            // toolbox.RemoveItem()
        }
    }
}