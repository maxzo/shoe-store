using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShoeStore.Startup))]
namespace ShoeStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
