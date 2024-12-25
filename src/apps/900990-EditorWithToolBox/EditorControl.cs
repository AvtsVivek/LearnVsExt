using System.Windows.Forms;

namespace EditorWithToolBox
{
    public partial class EditorControl : RichTextBox
    {
        #region Constructor
        /// <summary>
        /// Explicitly defined default constructor.
        /// Initialize new instance of the EditorControl.
        /// </summary>
        public EditorControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            WordWrap = false;
        }
        #endregion
    }
}
