using Microsoft.VisualStudio.Shell;
using System.ComponentModel;

namespace ReadOptionsValues
{
    public class OptionPageGrid : DialogPage
    {
        private int optionInt = 256;

        [Category(category: "My int Category")]
        [DisplayName(displayName: "My Integer Option")]
        [Description(description: "An option of the type integer")]
        public int OptionInteger
        {
            get { return optionInt; }
            set { optionInt = value; }
        }
    }
}
