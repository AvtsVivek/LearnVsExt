using AutofacComnityToolKitNotWorking.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AutofacComnityToolKitNotWorking
{
    public partial class MyToolWindowViewModel : ObservableObject
    {
        private IGreeterService _greeterService;

        public MyToolWindowViewModel(IGreeterService greeterService)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            _greeterService = greeterService;
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}