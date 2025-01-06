



















//using Microsoft.VisualStudio.Text.Operations;
//using Microsoft.VisualStudio.Text.Tagging;
//using Microsoft.VisualStudio.Text;
//using Microsoft.VisualStudio.Utilities;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.Composition;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TagVarieties.Taggers;

//namespace TagVarieties.TaggerProviders
//{
//    [Export(typeof(ITaggerProvider))]
//    [TagType(typeof(IErrorTag))]
//    [ContentType(ContentTypeDefsAndExtAssociations.ContentTypeCompilerErrorTag)]
//    public class CompilerErrorTaggerProvider : ITaggerProvider
//    {
//        [Import]
//        public ITextSearchService2 TextSearchService { get; set; }

//        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
//        {
//            Debug.WriteLine(GetType().FullName + " is called");
//            return (ITagger<T>)new CompilerErrorTagger(TextSearchService);
//        }
//    }
//}
