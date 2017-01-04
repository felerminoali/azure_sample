using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestGit.Startup))]
namespace TestGit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
