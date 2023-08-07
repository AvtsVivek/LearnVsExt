using System.ComponentModel;

namespace LanguageServerWithNetCoreWpfUI.CommonClasses
{
    public class FoldingRangeItem : INotifyPropertyChanged
    {
        private int startLine;
        private int? startCharacter;
        private int endLine;
        private int? endCharacter;

        public event PropertyChangedEventHandler PropertyChanged;

        public int StartLine
        {
            get => startLine;
            set
            {
                if (startLine != value)
                {
                    startLine = value;
                    OnPropertyChanged(nameof(StartLine));
                }
            }
        }

        public int? StartCharacter
        {
            get => startCharacter;
            set
            {
                if (startCharacter != value)
                {
                    startCharacter = value;
                    OnPropertyChanged(nameof(StartCharacter));
                }
            }
        }

        public int EndLine
        {
            get => endLine;
            set
            {
                if (endLine != value)
                {
                    endLine = value;
                    OnPropertyChanged(nameof(EndLine));
                }
            }
        }

        public int? EndCharacter
        {
            get => endCharacter;
            set
            {
                if (endCharacter != value)
                {
                    endCharacter = value;
                    OnPropertyChanged(nameof(EndCharacter));
                }
            }
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
