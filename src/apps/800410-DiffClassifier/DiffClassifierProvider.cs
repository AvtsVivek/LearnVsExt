using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace DiffClassifier
{
    [Export(typeof(IClassifierProvider))]
    [ContentType("diff")]
    internal class DiffClassifierProvider : IClassifierProvider
    {
        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry = null;

        static DiffClassifier diffClassifier;

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            if (diffClassifier == null)
                diffClassifier = new DiffClassifier(ClassificationRegistry);

            return diffClassifier;
        }
    }
}
