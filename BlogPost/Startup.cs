using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogPost.Startup))]
namespace BlogPost
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
