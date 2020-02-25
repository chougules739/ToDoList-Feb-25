using System.Web;

namespace TodoList.Models
{
    public static class UserSettings
    {
        public static string UserName { get { return HttpContext.Current.User.Identity.Name; } }
    }
}