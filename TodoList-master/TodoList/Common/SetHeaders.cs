using System.Web;

namespace TodoList.Common
{
    public class SetHeaders
    {
        public static void Setheaders(HttpResponse response)
        {
            response.AppendHeader("X-Permitted-Cross-Domain-Policies", "none");
            response.AppendHeader("X-Frame-Options", "DENY");
            response.AppendHeader("Content-Security-Policy", "default-src 'self';");
        }
    }
}