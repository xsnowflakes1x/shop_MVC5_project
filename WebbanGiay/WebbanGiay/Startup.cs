using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebbanGiay.Startup))]
namespace WebbanGiay
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
