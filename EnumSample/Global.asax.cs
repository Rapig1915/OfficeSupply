
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using OfficeSupply.Models;

namespace OfficeSupply
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Clear the database each time we start; not recommended for production use
            //SetInitializer<MyDataContext>(new DropCreateDatabaseAlways<MyDataContext>());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
