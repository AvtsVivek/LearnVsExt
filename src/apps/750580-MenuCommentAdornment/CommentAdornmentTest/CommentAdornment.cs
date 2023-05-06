using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text;

namespace CommentAdornmentTest
{
    public class CommentAdornment
    {
        public readonly ITrackingSpan Span;
        public readonly string Author;
        public readonly string Text;

        public CommentAdornment(SnapshotSpan span, string author, string text)
        {
            this.Span = span.Snapshot.CreateTrackingSpan(span, SpanTrackingMode.EdgeExclusive);
            this.Author = author;
            this.Text = text;
        }
    }
}
