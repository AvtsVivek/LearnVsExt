using System;
using System.Windows;

namespace GetSelectionShowPopup
{
    /// <summary>
    /// Interaction logic for AddDocumentationWindow.xaml
    /// </summary>
    public partial class AddDocumentationWindow : BaseDialogWindow
    {
        public AddDocumentationWindow(string documentPath, TextViewSelection? selection)
        {
            InitializeComponent();
            this._documentPath = documentPath;
            this._selectionText = selection;
            this.Loaded += (s, e) => this.SelectionTextBox.Text = selection.Value.Text;
        }


        private string _documentPath;
        private TextViewSelection? _selectionText;

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            if (this.DocumentationTextBox.Text.Trim() == "")
            {
                MessageBox.Show("Documentation can't be empty.");
                return;
            }

            var newDocFragment = new DocumentationFragment()
            {
                Documentation = this.DocumentationTextBox.Text,
                Selection = this._selectionText
            };
            try
            {
                DocumentationFileHandler.AddDocumentationFragment(newDocFragment, this._documentPath + ".doc");
                var successMessage = "Documentation added successfully" + Environment.NewLine + 
                    "at the following location" + Environment.NewLine + 
                    System.IO.Path.GetDirectoryName(this._documentPath)
                    + Environment.NewLine + "Do you want to open this folder to see the doc file?";

                var messageBoxResult = MessageBox.Show(successMessage, "Saved Successifully", MessageBoxButton.YesNo, 
                    MessageBoxImage.Information, MessageBoxResult.Yes);

                if (messageBoxResult == MessageBoxResult.Yes)
                    System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(this._documentPath));
                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Documentation add failed. Exception: " + ex.ToString());
            }

        }
    }
}
