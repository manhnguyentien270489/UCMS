using Microsoft.Owin;
using Owin;
using UCMS.Web;

[assembly: OwinStartup(typeof(Startup))]

namespace UCMS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
