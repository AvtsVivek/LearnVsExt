using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text.Editor;

namespace TodoClassification
{
    class ToDoClassifier : IClassifier
    {
        private IClassificationType _classificationType;
        private ITagAggregator<ToDoTag> _tagger;

        internal ToDoClassifier(ITagAggregator<ToDoTag> tagger, IClassificationType todoType)
        {
            _tagger = tagger;
            _classificationType = todoType;
        }

        /// <summary>
        /// Get every ToDoTag instance within the given span. Generally, the span in 
        /// question is the displayed portion of the file currently open in the Editor
        /// </summary>
        /// <param name="span">The span of text that will be searched for ToDo tags</param>
        /// <returns>A list of every relevant tag in the given span</returns>
        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            var classificationSpanList = new List<ClassificationSpan>();

            var tags = _tagger.GetTags(span);

            foreach (IMappingTagSpan<ToDoTag> tagSpan in tags)
            {
                SnapshotSpan todoSpan = tagSpan.Span.GetSpans(span.Snapshot).First();
                classificationSpanList.Add(new ClassificationSpan(todoSpan, _classificationType));
            }

            return classificationSpanList;
        }

        /// <summary>
        /// Create an event for when the Classification changes
        /// </summary>
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged
        {
            add { }
            remove { }
        }
    }
}
