using Microsoft;

namespace AutofacDIIntro.Commands
{
    public class TrialToolWindowViewModel
    {
        private readonly IGreeterService _greeterService;
        public TrialToolWindowViewModel(IGreeterService greeterService)
        {
            Assumes.Present(greeterService);
            _greeterService = greeterService;
            Message = _greeterService.GetGreetingsMessage();
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

    }
}