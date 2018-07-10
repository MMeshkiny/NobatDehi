using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NobatDehi.Startup))]
namespace NobatDehi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
