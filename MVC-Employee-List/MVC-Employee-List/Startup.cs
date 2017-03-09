using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_Employee_List.Startup))]
namespace MVC_Employee_List
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
