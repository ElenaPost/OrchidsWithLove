using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OrchidsWithLove_.Startup))]
namespace OrchidsWithLove_
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
