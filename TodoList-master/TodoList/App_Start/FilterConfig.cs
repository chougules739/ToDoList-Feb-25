using System.Web.Mvc;
using TodoList.Common;

namespace TodoList
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new SessionExists());
        }
    }
}