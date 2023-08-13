using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using System.Collections.Generic;
using System;

namespace DiffClassifier
{
    public class DiffClassifier : IClassifier
    {
        IClassificationTypeRegistryService _classificationTypeRegistry;

        internal DiffClassifier(IClassificationTypeRegistryService registry)
        {
            this._classificationTypeRegistry = registry;
        }

        #region Public Events
#pragma warning disable 67
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
#pragma warning restore 67
        #endregion // Public Events

        #region Public Methods
        /// <summary>
        /// Classify the given spans, which, for diff files, classifies
        /// a line at a time.
        /// </summary>
        /// <param name="span">The span of interest in this projection buffer.</param>
        /// <returns>The list of <see cref="ClassificationSpan"/> as contributed by the source buffers.</returns>
        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            ITextSnapshot snapshot = span.Snapshot;

            List<ClassificationSpan> spans = new List<ClassificationSpan>();

            if (snapshot.Length == 0)
                return spans;

            int startno = span.Start.GetContainingLine().LineNumber;
            int endno = (span.End - 1).GetContainingLine().LineNumber;

            for (int i = startno; i <= endno; i++)
            {
                ITextSnapshotLine line = snapshot.GetLineFromLineNumber(i);

                IClassificationType type = null;
                string text = line.Snapshot.GetText(
                        new SnapshotSpan(line.Start, Math.Min(4, line.Length))); // We only need the first 4 

                if (text.StartsWith("!", StringComparison.Ordinal))
                    type = _classificationTypeRegistry.GetClassificationType("diff.changed");
                else if (text.StartsWith("---", StringComparison.Ordinal))
                    type = _classificationTypeRegistry.GetClassificationType("diff.header");
                else if (text.StartsWith("-", StringComparison.Ordinal))
                    type = _classificationTypeRegistry.GetClassificationType("diff.removed");
                else if (text.StartsWith("<", StringComparison.Ordinal))
                    type = _classificationTypeRegistry.GetClassificationType("diff.removed");
                else if (text.StartsWith("@@", StringComparison.Ordinal))
                    type = _classificationTypeRegistry.GetClassificationType("diff.patchline");
                else if (text.StartsWith("+++", StringComparison.Ordinal))
                    type = _classificationTypeRegistry.GetClassificationType("diff.header");
                else if (text.StartsWith("+", StringComparison.Ordinal))
                    type = _classificationTypeRegistry.GetClassificationType("diff.added");
                else if (text.StartsWith(">", StringComparison.Ordinal))
                    type = _classificationTypeRegistry.GetClassificationType("diff.added");

                else if (text.StartsWith("***", StringComparison.Ordinal))
                {
                    if (i < 2)
                        type = _classificationTypeRegistry.GetClassificationType("diff.header");
                    else
                        type = _classificationTypeRegistry.GetClassificationType("diff.infoline");
                }
                else if (text.Length > 0 && !char.IsWhiteSpace(text[0]))
                    type = _classificationTypeRegistry.GetClassificationType("diff.infoline");

                if (type != null)
                    spans.Add(new ClassificationSpan(line.Extent, type));
            }

            return spans;
        }

        #endregion // Public Methods
    }

}
