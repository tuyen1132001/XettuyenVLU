using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XettuyenDGNLTHPT.Startup))]
namespace XettuyenDGNLTHPT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
