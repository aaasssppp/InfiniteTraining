using System.Web;
using System.Web.Mvc;

namespace CodeChallenge10_Question2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
