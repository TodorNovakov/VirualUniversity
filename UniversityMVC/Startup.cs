using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UniversityMVC.Startup))]
namespace UniversityMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
