﻿using Microsoft.VisualStudio.Language.Intellisense.AsyncCompletion;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace AsyncCompletionSourceIntro.JsonElementCompletion
{
    [Export(typeof(IAsyncCompletionCommitManagerProvider))]
    [Name("Chemical element commit manager provider")]
    [ContentType("text")]
    class SampleCompletionCommitManagerProvider : IAsyncCompletionCommitManagerProvider
    {
        IDictionary<ITextView, IAsyncCompletionCommitManager> cache = new Dictionary<ITextView, IAsyncCompletionCommitManager>();

        public IAsyncCompletionCommitManager GetOrCreate(ITextView textView)
        {
            if (cache.TryGetValue(textView, out var itemSource))
                return itemSource;

            var manager = new SampleCompletionCommitManager();
            textView.Closed += (o, e) => cache.Remove(textView); // clean up memory as files are closed
            cache.Add(textView, manager);
            return manager;
        }
    }
}
