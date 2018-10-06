using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Expenses.Backend.Startup))]
namespace Expenses.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
