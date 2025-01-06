using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.VisualStudio.Text.Adornments;
using System.Windows.Controls;

namespace TagVarieties.Taggers
{
    public class ErrorTagger : ITagger<IErrorTag>
    {
        private readonly ITextSearchService2 _textSearchService;

        public ErrorTagger(ITextSearchService2 textSearchService)
        {
            this._textSearchService = textSearchService;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public IEnumerable<ITagSpan<IErrorTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            var snapshot = spans[0].Snapshot;

            var fullSnapshotSpan = new SnapshotSpan(snapshot,
                    new Span(0, snapshot.Length));


            var errorTagList = new List<TagSpan<IErrorTag>>();

            var errorTypeNameString = nameof(PredefinedErrorTypeNames.SyntaxError);

            var errorName = PredefinedErrorTypeNames.SyntaxError;

            var errorTag = new ErrorTag(errorName, $"{errorName} tool tip");

            var errorTypeWords = _textSearchService.FindAll(fullSnapshotSpan, errorTypeNameString, FindOptions.WholeWord);

            var errorTags = errorTypeWords.Where(s => spans.IntersectsWith(s))
                .Select(s => new TagSpan<IErrorTag>(s, errorTag));

            errorTagList.AddRange(errorTags);

            errorTypeNameString = nameof(PredefinedErrorTypeNames.CompilerError);

            errorName = PredefinedErrorTypeNames.CompilerError;

            errorTag = new ErrorTag(errorName, $"{errorName} tool tip");

            errorTypeWords = _textSearchService.FindAll(fullSnapshotSpan, errorTypeNameString, FindOptions.WholeWord);

            errorTags = errorTypeWords.Where(s => spans.IntersectsWith(s))
                .Select(s => new TagSpan<IErrorTag>(s, errorTag));

            errorTagList.AddRange(errorTags);


            errorTypeNameString = nameof(PredefinedErrorTypeNames.OtherError);

            errorName = PredefinedErrorTypeNames.OtherError;

            errorTag = new ErrorTag(errorName, $"{errorName} tool tip");

            errorTypeWords = _textSearchService.FindAll(fullSnapshotSpan, errorTypeNameString, FindOptions.WholeWord);

            errorTags = errorTypeWords.Where(s => spans.IntersectsWith(s))
                .Select(s => new TagSpan<IErrorTag>(s, errorTag));

            errorTagList.AddRange(errorTags);

            //var warningTag = new ErrorTag(PredefinedErrorTypeNames.Warning, "Warning tool tip");

            errorTypeNameString = nameof(PredefinedErrorTypeNames.Warning);

            errorName = PredefinedErrorTypeNames.Warning;

            errorTag = new ErrorTag(errorName, $"{errorName} tool tip");

            errorTypeWords = _textSearchService.FindAll(fullSnapshotSpan, errorTypeNameString, FindOptions.WholeWord);

            errorTags = errorTypeWords.Where(s => spans.IntersectsWith(s))
                .Select(s => new TagSpan<IErrorTag>(s, errorTag));

            errorTagList.AddRange(errorTags);

            //var informationTag = new ErrorTag(PredefinedErrorTypeNames.Information, "Information tool tip");

            errorTypeNameString = nameof(PredefinedErrorTypeNames.Information);

            errorName = PredefinedErrorTypeNames.Information;

            errorTag = new ErrorTag(errorName, $"{errorName} tool tip");

            errorTypeWords = _textSearchService.FindAll(fullSnapshotSpan, errorTypeNameString, FindOptions.WholeWord);

            errorTags = errorTypeWords.Where(s => spans.IntersectsWith(s))
                .Select(s => new TagSpan<IErrorTag>(s, errorTag));

            errorTagList.AddRange(errorTags);


            //var suggestionTag = new ErrorTag(PredefinedErrorTypeNames.Suggestion, "Suggestion tool tip");

            errorTypeNameString = nameof(PredefinedErrorTypeNames.Suggestion);

            errorName = PredefinedErrorTypeNames.Suggestion;

            errorTag = new ErrorTag(errorName, $"{errorName} tool tip");

            errorTypeWords = _textSearchService.FindAll(fullSnapshotSpan, errorTypeNameString, FindOptions.WholeWord);

            errorTags = errorTypeWords.Where(s => spans.IntersectsWith(s))
                .Select(s => new TagSpan<IErrorTag>(s, errorTag));

            errorTagList.AddRange(errorTags);

            //var hintedSuggestionTag = new ErrorTag(PredefinedErrorTypeNames.HintedSuggestion, "Hinted Suggestion tool tip");

            errorTypeNameString = nameof(PredefinedErrorTypeNames.HintedSuggestion);

            errorName = PredefinedErrorTypeNames.HintedSuggestion;

            errorTag = new ErrorTag(errorName, $"{errorName} tool tip");

            errorTypeWords = _textSearchService.FindAll(fullSnapshotSpan, errorTypeNameString, FindOptions.WholeWord);

            errorTags = errorTypeWords.Where(s => spans.IntersectsWith(s))
                .Select(s => new TagSpan<IErrorTag>(s, errorTag));

            errorTagList.AddRange(errorTags);


            return errorTagList;
        }
    }
}
