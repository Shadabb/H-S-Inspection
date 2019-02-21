using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Inspection.Startup))]
namespace Inspection
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
