using Homory.Startup;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Homory.Startup
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

		}
    }
}
