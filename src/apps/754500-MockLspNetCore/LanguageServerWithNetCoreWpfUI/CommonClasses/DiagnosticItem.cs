using Microsoft.VisualStudio.LanguageServer.Protocol;
using System.ComponentModel;

namespace LanguageServerWithNetCoreWpfUI.CommonClasses
{
    public class DiagnosticItem : INotifyPropertyChanged
    {
        private string text;
        private ProjectContextItem context;
        private DiagnosticSeverity severity;
        private MockDiagnosticTags tag;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Text
        {
            get => text;
            set
            {
                if (text != value)
                {
                    text = value;
                    OnPropertyChanged(nameof(Text));
                }
            }
        }

        public ProjectContextItem Context
        {
            get => context;
            set
            {
                if (context != value)
                {
                    context = value;
                    OnPropertyChanged(nameof(Context));
                }
            }
        }

        public DiagnosticSeverity Severity
        {
            get => severity;
            set
            {
                if (severity != value)
                {
                    severity = value;
                    OnPropertyChanged(nameof(Severity));
                }
            }
        }

        public MockDiagnosticTags Tag
        {
            get => tag;
            set
            {
                if (tag != value)
                {
                    tag = value;
                    OnPropertyChanged(nameof(tag));
                }
            }
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
