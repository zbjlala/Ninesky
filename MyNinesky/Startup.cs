using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyNinesky.Startup))]
namespace MyNinesky
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
