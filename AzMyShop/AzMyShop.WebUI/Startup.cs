using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AzMyShop.WebUI.Startup))]
namespace AzMyShop.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
