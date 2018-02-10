using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SP.MVC.Startup))]
namespace SP.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
