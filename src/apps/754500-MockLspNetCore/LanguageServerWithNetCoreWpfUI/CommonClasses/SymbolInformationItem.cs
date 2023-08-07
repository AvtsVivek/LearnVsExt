using Microsoft.VisualStudio.LanguageServer.Protocol;
using System.ComponentModel;

namespace LanguageServerWithNetCoreWpfUI.CommonClasses
{
    public class SymbolInformationItem : INotifyPropertyChanged
    {
        private string name;
        private SymbolKind kind;
        private string container;
        private string vsKind;
        private string vsKindModifier;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public SymbolKind Kind
        {
            get => kind;
            set
            {
                if (kind != value)
                {
                    kind = value;
                    OnPropertyChanged(nameof(Kind));
                }
            }
        }

        public string Container
        {
            get => container;
            set
            {
                if (container != value)
                {
                    container = value;
                    OnPropertyChanged(nameof(Container));
                }
            }
        }

        public string VsKind
        {
            get => vsKind;
            set
            {
                if (vsKind != value)
                {
                    vsKind = value;
                    OnPropertyChanged(nameof(VsKind));
                }
            }
        }

        public string VsKindModifier
        {
            get => vsKindModifier;
            set
            {
                if (vsKindModifier != value)
                {
                    vsKindModifier = value;
                    OnPropertyChanged(nameof(VsKindModifier));
                }
            }
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
