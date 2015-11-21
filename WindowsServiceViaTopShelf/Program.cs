using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using Owin;
using Topshelf;

//More info here: http://dontcodetired.com/blog/post/Painless-NET-Windows-Service-Creation-with-Topshelf.aspx
//For install use: WindowsServiceViaTopShelf.exe install
//For uninstall use WindowsServiceViaTopShelf.exe uninstall
//For test open browser on: http://localhost:8084/api/time/now

namespace WindowsServiceViaTopShelf
{
    public class Program
    {
        private static void Main(string[] args) {
            HostFactory.Run(
            configuration => {
                configuration.Service<TimeService>(
                    service => {
                        service.ConstructUsing( x => new TimeService() );
                        service.WhenStarted( x => x.Start() );
                        service.WhenStopped( x => x.Stop() );
                    } );

                configuration.RunAsLocalSystem();

                configuration.SetServiceName( "ASimpleService" );
                configuration.SetDisplayName( "A Simple Service" );
                configuration.SetDescription( "Don't Code Tired Demo" );
            } );
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder) {
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute( "DefaultApi", "api/{controller}/{time}" );
            config.Formatters.Remove( config.Formatters.XmlFormatter );
            config.Formatters.Add( config.Formatters.JsonFormatter );

            appBuilder.UseWebApi( config );
        }
    }

    public class TimeController : ApiController
    {
        [HttpGet]
        public string Now() {
            return DateTime.Now.ToLongTimeString();
        }
    }

    public class TimeService
    {
        private IDisposable _webServer;

        public void Start() {
            // code that runs when the Windows Service starts up
            _webServer = WebApp.Start<Startup>( "http://localhost:8084" );
        }

        public void Stop() {
            // code that runs when the Windows Service stops
            _webServer.Dispose();
        }
    }
}