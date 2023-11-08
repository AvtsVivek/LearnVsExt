using BasicMvvmHWin.ViewModels;
using BasicMvvmHWin.Infra;
using System.Collections.Generic;
using System.Windows.Input;
using System;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Windows.Interop;

namespace BasicMvvmHWin.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {

        public string Number { get; set; } = "One";
        public string ProcessId { get; set; } = Process.GetCurrentProcess().Id.ToString();
        public string _hWnd = string.Empty;

        public string HWnd
        {
            get
            {
                return _hWnd;
            }
            set
            {
                _hWnd = value;
                OnPropertyChanged(nameof(HWnd));
            }
        }

        private ICommand? _hWinButtonClick;

        // public event EventHandler<EventArgs<string>>? ViewChanged;

        public ICommand HWinButtonClick
        {
            get
            {
                return _hWinButtonClick ??= new RelayCommand(x =>
                {
                    HWnd = Process.GetCurrentProcess().MainWindowHandle.ToString();
                });
            }
        }


        public MainWindowViewModel()
        {
            HWnd = Process.GetCurrentProcess().MainWindowHandle.ToString();
        }
    }
}
