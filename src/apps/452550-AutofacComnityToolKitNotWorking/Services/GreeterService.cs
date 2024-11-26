namespace AutofacComnityToolKitNotWorking.Services
{
    public interface IGreeterService
    {
        string GetGreetingsMessage();
    }
    public class GreeterService : IGreeterService
    {
        public string GetGreetingsMessage()
        {
            return "Have a nice day!!!";
        }
    }
}
