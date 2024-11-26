using Autofac;
using CaretPositionOnToolWindow.Services;
using CaretPositionOnToolWindow.ToolWindows;

namespace CaretPositionOnToolWindow.Infra
{
    public class BusinessServicesModule : Module
    {
        // What the hell is the following for?
        //private HttpClient CreateHttpClient()
        //{
        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Accept.ParseAdd(Constants.AcceptHeaderValue);
        //    client.DefaultRequestHeaders.UserAgent.ParseAdd(Constants.UserAgentHeaderValue);
        //    return client;
        //}

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<CaretPositionToolWindow>();
            builder.RegisterType<CaretPositionToolWindowControl>();
            builder.RegisterType<CaretPositionToolWindowViewModel>().SingleInstance();
            builder.RegisterType<DocumentService>().As<IDocumentService>().SingleInstance();
        }
    }
}
