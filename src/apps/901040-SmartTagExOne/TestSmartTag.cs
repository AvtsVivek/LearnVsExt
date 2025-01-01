using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text.Tagging;
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Utilities;

namespace SmartTagExOne
{
    internal class TestSmartTag : ErrorTag
    {
        public TestSmartTag(string errorType, string toolTipContent) 
            : base(errorType, toolTipContent) { }
    }
}
