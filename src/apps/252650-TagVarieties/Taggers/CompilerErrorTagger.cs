using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System;
using System.Linq;

namespace TagVarieties.Taggers
{
    //public class CompilerErrorTagger : ITagger<IUrlTag>
    //{
    //    private readonly ITextSearchService2 _textSearchService;

    //    public CompilerErrorTagger(ITextSearchService2 textSearchService)
    //    {
    //        this._textSearchService = textSearchService;
    //    }

    //    public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

    //    public IEnumerable<ITagSpan<IUrlTag>> GetTags(NormalizedSnapshotSpanCollection spans)
    //    {
    //        var snapshot = spans[0].Snapshot;
    //        var fullSnapshotSpan = new SnapshotSpan(snapshot,
    //                new Span(0, snapshot.Length));

    //        var helloWords = _textSearchService
    //                .FindAll(fullSnapshotSpan, "hello", FindOptions.WholeWord);

    //        return helloWords
    //            .Where(s => spans.IntersectsWith(s))
    //            .Select(s => new TagSpan<IUrlTag>(s,
    //                new UrlTag(new Uri("https://en.wikipedia.org/wiki/Hello"))));
    //    }
    //}
}
