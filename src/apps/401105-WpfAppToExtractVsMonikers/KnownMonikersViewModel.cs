using Microsoft.VisualStudio.Imaging.Interop;
using System.Windows.Media.Imaging;
using Microsoft.VisualStudio.Interop;
using Microsoft.VisualStudio.Shell.Interop;

namespace WpfAppToExtractVsMonikers
{
    public class KnownMonikersViewModel
    {
        public KnownMonikersViewModel(string name, ImageMoniker moniker)
        {
            ImageMonikerName = name;
            Moniker = moniker;

            // Image = Moniker.ToBitmapSourceAsync(size);
            
            //Community.
            // Image = Moniker.ToBitmapSourceAsync(16);

            if (MonikerKeywords.Keywords.TryGetValue(name, out var filters))
            {
                Filters = filters;
            }
        }

        public string ImageMonikerName { get; set; }
        public ImageMoniker Moniker { get; set; }
        public BitmapSource Image { get; set; } 
        public string Filters { get; set; } = "";

        public bool MatchSearchTerm(string searchTerm)
        {
            return ImageMonikerName.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0
                   || Filters.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0
                   || (int.TryParse(searchTerm, out int id) && id == Moniker.Id)
                   || (Guid.TryParse(searchTerm, out Guid guid) && guid == Moniker.Guid);
        }
    }
}