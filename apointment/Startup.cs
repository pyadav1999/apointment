using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(apointment.Startup))]
namespace apointment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
