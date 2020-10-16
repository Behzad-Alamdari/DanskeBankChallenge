using DBC.DataAccess.EntityFramework;
using DBC.WebHost.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace DBC.WebHost
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var buildHelper = new DatabaseBuildHelper();
            using (var db = new DansBankDbContext(buildHelper))
            {
                if (!db.Database.CanConnect())
                {
                    Console.WriteLine("Database is been created, please be patient. You will be notify when it is done");
                    db.Database.EnsureCreated();
                    Console.WriteLine("Database is been created");
                }
            }
        }
    }
}