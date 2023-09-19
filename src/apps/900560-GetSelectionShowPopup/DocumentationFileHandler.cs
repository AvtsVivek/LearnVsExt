using System.Collections.Generic;
using System.Linq;

namespace GetSelectionShowPopup
{
    public static class DocumentationFileHandler
    {
        public static void AddDocumentationFragment(DocumentationFragment fragment, string filepath)
        {
            var content = DocumentationFileSerializer.Deserialize(filepath);
            var newFragments = content.Fragments
                .Where(f => !f.Selection.Value.StartPosition.Equals(fragment.Selection.Value.StartPosition)
                             || !f.Selection.Value.EndPosition.Equals(fragment.Selection.Value.EndPosition));
            newFragments = newFragments.Concat(new List<DocumentationFragment>() { fragment });
            content.Fragments = newFragments.ToList();
            DocumentationFileSerializer.Serialize(filepath, content);
        }
    }
}
