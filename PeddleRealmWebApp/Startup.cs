using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PeddleRealmWebApp.Startup))]
namespace PeddleRealmWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
