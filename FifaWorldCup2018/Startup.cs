using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FifaWorldCup2018.Startup))]
namespace FifaWorldCup2018
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
