using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Diabetes1.Startup))]
namespace Diabetes1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
