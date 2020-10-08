using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Rollin.Startup))]
namespace Rollin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
