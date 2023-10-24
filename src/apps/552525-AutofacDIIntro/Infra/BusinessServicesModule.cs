using Autofac;
using AutofacDIIntro.Commands;
using Microsoft.VisualStudio;
using System.Net.Http;

namespace AutofacDIIntro.Infra
{
    public class BusinessServicesModule : Module
    {
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

            builder.RegisterType<TrialToolWindow>();
            builder.RegisterType<TrialToolWindowControl>();
            builder.RegisterType<TrialToolWindowViewModel>().SingleInstance();
            builder.RegisterType<GreeterService>().As<IGreeterService>().SingleInstance();

            //builder.RegisterType<GistClientService>().As<IGistClientService>();
            //builder.RegisterType<WpfAuthenticationHandler>().As<IAuthenticationHandler>();

            //builder.RegisterType<WpfErrorHandler>().As<IErrorHandler>().SingleInstance();
            //builder.RegisterType<AsyncOperationStatusManager>().As<IAsyncOperationStatusManager>().SingleInstance();

            // builder.Register<HttpClient>(ctx => CreateHttpClient()).SingleInstance();
        }
    }
}
