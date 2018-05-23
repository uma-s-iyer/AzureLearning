using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AzureLearning.Startup))]
namespace AzureLearning
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
