using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(reader2reader.Startup))]
namespace reader2reader
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
