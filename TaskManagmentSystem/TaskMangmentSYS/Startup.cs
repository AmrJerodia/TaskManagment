using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaskMangmentSYS.Startup))]
namespace TaskMangmentSYS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

    }
}
