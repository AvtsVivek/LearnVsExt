using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CommentAdornmentTest
{
    internal class CommentAdornmentProvider
    {
        private ITextBuffer buffer;
        private IList<CommentAdornment> comments = new List<CommentAdornment>();
        private CommentAdornmentProvider(ITextBuffer buffer)
        {
            this.buffer = buffer;
            //listen to the Changed event so we can react to deletions. 
            this.buffer.Changed += OnBufferChanged;
        }

        public static CommentAdornmentProvider Create(IWpfTextView view)
        {
            return view.Properties.GetOrCreateSingletonProperty<CommentAdornmentProvider>(delegate 
            { 
                return new CommentAdornmentProvider(view.TextBuffer); 
            });
        }

        public void Detach()
        {
            if (this.buffer != null)
            {
                //remove the Changed listener 
                this.buffer.Changed -= OnBufferChanged;
                this.buffer = null;
            }
        }

        private void OnBufferChanged(object sender, TextContentChangedEventArgs e)
        {
            //Make a list of all comments that have a span of at least one character after applying the change. There is no need to raise a changed event for the deleted adornments. The adornments are deleted only if a text change would cause the view to reformat the line and discard the adornments.
            IList<CommentAdornment> keptComments = new List<CommentAdornment>(this.comments.Count);

            foreach (CommentAdornment comment in this.comments)
            {
                Span span = comment.Span.GetSpan(e.After);
                //if a comment does not span at least one character, its text was deleted.
                if (span.Length != 0)
                {
                    keptComments.Add(comment);
                }
            }

            this.comments = keptComments;
        }

        public event EventHandler<CommentsChangedEventArgs> CommentsChanged;
        public void Add(SnapshotSpan span, string author, string text)
        {
            if (span.Length == 0)
                throw new ArgumentOutOfRangeException("span");
            if (author == null)
                throw new ArgumentNullException("author");
            if (text == null)
                throw new ArgumentNullException("text");

            //Create a comment adornment given the span, author and text.
            CommentAdornment comment = new CommentAdornment(span, author, text);

            //Add it to the list of comments. 
            this.comments.Add(comment);

            //Raise the changed event.
            EventHandler<CommentsChangedEventArgs> commentsChanged = this.CommentsChanged;
            if (commentsChanged != null)
                commentsChanged(this, new CommentsChangedEventArgs(comment, null));
        }

        public void RemoveComments(SnapshotSpan span)
        {
            EventHandler<CommentsChangedEventArgs> commentsChanged = this.CommentsChanged;

            //Get a list of all the comments that are being kept
            IList<CommentAdornment> keptComments = new List<CommentAdornment>(this.comments.Count);

            foreach (CommentAdornment comment in this.comments)
            {
                //find out if the given span overlaps with the comment text span. If two spans are adjacent, they do not overlap. To consider adjacent spans, use IntersectsWith. 
                if (comment.Span.GetSpan(span.Snapshot).OverlapsWith(span))
                {
                    //Raise the change event to delete this comment. 
                    if (commentsChanged != null)
                        commentsChanged(this, new CommentsChangedEventArgs(null, comment));
                }
                else
                    keptComments.Add(comment);
            }

            this.comments = keptComments;
        }

        public Collection<CommentAdornment> GetComments(SnapshotSpan span)
        {
            IList<CommentAdornment> overlappingComments = new List<CommentAdornment>();
            foreach (CommentAdornment comment in this.comments)
            {
                if (comment.Span.GetSpan(span.Snapshot).OverlapsWith(span))
                    overlappingComments.Add(comment);
            }

            return new Collection<CommentAdornment>(overlappingComments);
        }

    }
}
