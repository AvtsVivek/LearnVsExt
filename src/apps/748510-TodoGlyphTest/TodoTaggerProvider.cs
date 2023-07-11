﻿using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.Text.Classification;

namespace TodoGlyphTest
{
    [Export(typeof(ITaggerProvider))]
    [ContentType("code")]
    [TagType(typeof(TodoTag))]
    public class TodoTaggerProvider : ITaggerProvider
    {
        [Import]
        internal IClassifierAggregatorService AggregatorService;
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            return new TodoTagger(AggregatorService.GetClassifier(buffer)) as ITagger<T>;
        }
    }

}
