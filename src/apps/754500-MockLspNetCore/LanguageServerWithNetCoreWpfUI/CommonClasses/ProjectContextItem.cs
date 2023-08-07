using Microsoft.VisualStudio.LanguageServer.Protocol;
using System.ComponentModel;

namespace LanguageServerWithNetCoreWpfUI.CommonClasses
{
    public class ProjectContextItem : INotifyPropertyChanged
    {
        private string label;
        private VSProjectKind kind;
        private string id;
        private string vsKind;
        private string vsKindModifier;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Label
        {
            get => label;
            set
            {
                if (label != value)
                {
                    label = value;
                    OnPropertyChanged(nameof(Label));
                }
            }
        }

        public VSProjectKind Kind
        {
            get => kind;
            set
            {
                if (kind != value)
                {

                }
                kind = value;
                OnPropertyChanged(nameof(Kind));
            }
        }

        public string Id
        {
            get => id;
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
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

        public VSProjectContext ToVSContext()
        {
            var context = new VSProjectContext()
            {
                Label = Label,
                Kind = Kind,
                Id = Id,
            };

            return context;
        }
    }
}
