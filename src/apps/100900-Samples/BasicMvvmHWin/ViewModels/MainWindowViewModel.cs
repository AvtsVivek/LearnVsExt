using System.Diagnostics;
using System.Threading.Tasks;

namespace BasicMvvmHWin.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {

        public string Number { get; set; } = "One";
        public string ProcessId { get; set; } 
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


        public MainWindowViewModel()
        {
            ProcessId = Process.GetCurrentProcess().Id.ToString();
            HWnd = Process.GetCurrentProcess().MainWindowHandle.ToString();
            Task.Run(async () => {
                await GetAndAssignWindowHandle();
            });
        }
        
        private async Task GetAndAssignWindowHandle()
        {
            await Task.Delay(1000);
            HWnd = Process.GetCurrentProcess().MainWindowHandle.ToString();
        }

    }
}
