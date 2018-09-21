using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RestAPICSharp.Startup))]
namespace RestAPICSharp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
