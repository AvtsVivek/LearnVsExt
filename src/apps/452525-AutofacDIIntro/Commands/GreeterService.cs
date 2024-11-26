using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacDIIntro.Commands
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
