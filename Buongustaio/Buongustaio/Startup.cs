using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Buongustaio.Startup))]
namespace Buongustaio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
