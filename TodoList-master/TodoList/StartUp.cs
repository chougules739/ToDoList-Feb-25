using Microsoft.Owin;
using Owin;
using TodoList;

[assembly: OwinStartup(typeof(Startup))]

namespace TodoList
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        { 
            
        }
    }
}