using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Projet3.Startup))]
namespace Projet3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
