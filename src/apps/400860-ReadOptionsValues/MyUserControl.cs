using System;
using System.Windows.Forms;

namespace ReadOptionsValues
{
    public partial class MyUserControl : UserControl
    {
        public MyUserControl()
        {
            InitializeComponent();
        }
        internal OptionPageCustom optionsPage;

        public void Initialize()
        {
            textBox1.Text = optionsPage.OptionString;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            optionsPage.OptionString = textBox1.Text;
        }
    }
}
