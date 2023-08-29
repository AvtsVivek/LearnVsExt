using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Text.Tagging;

namespace OokLanguage.Intellisence
{
    /// <summary>
    /// Provides QuickInfo information to be displayed in a text buffer
    /// </summary>
    class OokQuickInfoSource : IQuickInfoSource
    {
        private ITagAggregator<OokTokenTag> _aggregator;
        private ITextBuffer _buffer;
        private bool _disposed = false;


        public OokQuickInfoSource(ITextBuffer buffer, ITagAggregator<OokTokenTag> aggregator)
        {
            _aggregator = aggregator;
            _buffer = buffer;
        }

        /// <summary>
        /// Determine which pieces of Quickinfo content should be displayed
        /// </summary>
        public void AugmentQuickInfoSession(IQuickInfoSession session, IList<object> quickInfoContent, out ITrackingSpan applicableToSpan)
        {
            applicableToSpan = null;

            if (_disposed)
                throw new ObjectDisposedException("TestQuickInfoSource");

            var triggerPoint = (SnapshotPoint)session.GetTriggerPoint(_buffer.CurrentSnapshot);

            if (triggerPoint == null)
                return;

            foreach (IMappingTagSpan<OokTokenTag> curTag in _aggregator.GetTags(new SnapshotSpan(triggerPoint, triggerPoint)))
            {
                if (curTag.Tag.type == OokTokenTypes.OokExclamation)
                {
                    var tagSpan = curTag.Span.GetSpans(_buffer).First();
                    applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan(tagSpan, SpanTrackingMode.EdgeExclusive);
                    quickInfoContent.Add("Exclaimed Ook!");
                }
                else if (curTag.Tag.type == OokTokenTypes.OokQuestion)
                {
                    var tagSpan = curTag.Span.GetSpans(_buffer).First();
                    applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan(tagSpan, SpanTrackingMode.EdgeExclusive);
                    quickInfoContent.Add("Question Ook?");
                }
                else if (curTag.Tag.type == OokTokenTypes.OokPeriod)
                {
                    var tagSpan = curTag.Span.GetSpans(_buffer).First();
                    applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan(tagSpan, SpanTrackingMode.EdgeExclusive);
                    quickInfoContent.Add("Regular Ook.");
                }
            }
        }

        public void Dispose()
        {
            _disposed = true;
        }
    }

}
