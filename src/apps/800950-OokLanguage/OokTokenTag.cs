using Microsoft.VisualStudio.Text.Tagging;

namespace OokLanguage
{

    public class OokTokenTag : ITag
    {
        public OokTokenTypes type { get; private set; }

        public OokTokenTag(OokTokenTypes type)
        {
            this.type = type;
        }
    }
}
