using DBC.DataAccess.EntityFramework;
using DBC.Models;
using DBC.WebHost.Helpers;
using System;

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

                    db.Municipalities.AddRange(SeedDataGenerator.Generate());
                    db.SaveChanges();

                    Console.WriteLine("Database is been created");
                }
            }
        }
    }
}