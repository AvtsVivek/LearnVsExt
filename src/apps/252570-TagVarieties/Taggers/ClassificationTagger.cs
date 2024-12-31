//using Microsoft.VisualStudio.Text.Operations;
//using Microsoft.VisualStudio.Text.Tagging;
//using Microsoft.VisualStudio.Text;
//using System.Collections.Generic;
//using System;
//using Microsoft.VisualStudio.Text.Classification;
//using Microsoft.VisualStudio.Utilities;
//using System.ComponentModel.Composition;
//using System.Windows.Media;

//namespace TagVarieties.Taggers
//{
//    [Export(typeof(EditorFormatDefinition))]
//    [ClassificationType(ClassificationTypeNames = "asdf")]
//    [Name("asdf")]
//    //this should be visible to the end user
//    [UserVisible(false)]
//    //set the priority to be after the default classifiers
//    [Order(Before = Priority.Default)]
//    internal sealed class Asdf : ClassificationFormatDefinition
//    {
//        /// <summary>
//        /// Defines the visual format for the "exclamation" classification type
//        /// </summary>
//        public Asdf()
//        {
//            DisplayName = "asdf"; //human readable version of the name
//            ForegroundColor = Colors.Red;
//        }
//    }
//    internal static class OrdinaryClassificationDefinition
//    {
//        /// <summary>
//        /// Defines the "ookExclamation" classification type.
//        /// </summary>
//        [Export(typeof(ClassificationTypeDefinition))]
//        [Name("asdf")]
//        internal static ClassificationTypeDefinition ookExclamation = null;
//    }

//    public class ClassificationTagger : ITagger<ClassificationTag>
//    {
//        private readonly ITextSearchService2 _textSearchService;
//        private readonly IClassificationTypeRegistryService _classificationTypeRegistryService;

//        public ClassificationTagger(ITextSearchService2 textSearchService, IClassificationTypeRegistryService classificationTypeRegistryService)
//        {
//            _textSearchService = textSearchService;
//            _classificationTypeRegistryService = classificationTypeRegistryService;
//        }

//        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

//        public IEnumerable<ITagSpan<ClassificationTag>> GetTags(NormalizedSnapshotSpanCollection spans)
//        {
//            var snapshot = spans[0].Snapshot;
//            var fullSnapshotSpan = new SnapshotSpan(snapshot,
//                    new Span(0, snapshot.Length));

//            var helloWords = _textSearchService
//                    .FindAll(fullSnapshotSpan, "hello", FindOptions.WholeWord);

//            var asdfClassification = _classificationTypeRegistryService.GetClassificationType("asdf");

//            var classificationTag = new ClassificationTag(asdfClassification);
                
//            var classificationTagSpan = new TagSpan<ClassificationTag>(spans[0], new ClassificationTag(asdfClassification));

//            var tagSpanList = new List<ITagSpan<ClassificationTag>>
//            {
//                classificationTagSpan
//            };

//            return tagSpanList;
//        }
//    }
//}
