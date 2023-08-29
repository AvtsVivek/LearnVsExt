﻿using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text;
using System.Collections.Generic;

namespace OokLanguage.Intellisence
{
    internal class TemplateQuickInfoController : IIntellisenseController
    {
        #region Private Data Members

        private ITextView _textView;
        private IList<ITextBuffer> _subjectBuffers;
        private TemplateQuickInfoControllerProvider _componentContext;

        private IQuickInfoSession _session;

        #endregion

        #region Constructors

        internal TemplateQuickInfoController(ITextView textView, IList<ITextBuffer> subjectBuffers, TemplateQuickInfoControllerProvider componentContext)
        {
            _textView = textView;
            _subjectBuffers = subjectBuffers;
            _componentContext = componentContext;

            _textView.MouseHover += OnTextViewMouseHover;
        }

        #endregion

        #region IIntellisenseController Members

        public void ConnectSubjectBuffer(ITextBuffer subjectBuffer)
        {
        }

        public void DisconnectSubjectBuffer(ITextBuffer subjectBuffer)
        {
        }

        public void Detach(ITextView textView)
        {
            if (_textView == textView)
            {
                _textView.MouseHover -= OnTextViewMouseHover;
                _textView = null;
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Determine if the mouse is hovering over a token. If so, highlight the token and display QuickInfo
        /// </summary>
        private void OnTextViewMouseHover(object sender, MouseHoverEventArgs e)
        {
            SnapshotPoint? point = GetMousePosition(new SnapshotPoint(_textView.TextSnapshot, e.Position));

            if (point != null)
            {
                ITrackingPoint triggerPoint = point.Value.Snapshot.CreateTrackingPoint(point.Value.Position,
                    PointTrackingMode.Positive);

                // Find the broker for this buffer

                if (!_componentContext.QuickInfoBroker.IsQuickInfoActive(_textView))
                {
                    _session = _componentContext.QuickInfoBroker.CreateQuickInfoSession(_textView, triggerPoint, true);
                    _session.Start();
                }
            }
        }

        #endregion

        #region Private Implementation

        /// <summary>
        /// get mouse location onscreen. Used to determine what word the cursor is currently hovering over
        /// </summary>
        private SnapshotPoint? GetMousePosition(SnapshotPoint topPosition)
        {
            // Map this point down to the appropriate subject buffer.

            return _textView.BufferGraph.MapDownToFirstMatch
                (
                topPosition,
                PointTrackingMode.Positive,
                snapshot => _subjectBuffers.Contains(snapshot.TextBuffer),
                PositionAffinity.Predecessor
                );
        }

        #endregion
    }

}
