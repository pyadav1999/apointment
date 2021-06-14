using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace apointment.Context
{
    public class AppointDbInitializer : IDatabaseInitializer<AppointDbContext>
    {
        public void InitializeDatabase(AppointDbContext context)
        {
            if (!context.Database.Exists())
            {
                // if database did not exist before - create it
                context.Database.Create();
            }
            else
            {
                // query to check if MigrationHistory table is present in the database 
                var migrationHistoryTableExists = ((IObjectContextAdapter)context).ObjectContext.ExecuteStoreQuery<int>(
                string.Format(
                  "SELECT COUNT(*) FROM information_schema.tables WHERE table_catalog = '{0}' AND table_name = '__MigrationHistory'",
                  "dbDevo"));

                // if MigrationHistory table is not there (which is the case first time we run) - create it
                //if (migrationHistoryTableExists.FirstOrDefault() == 0)
                //{
                //    context.Database.Delete();
                //    context.Database.Create();
                //}
            }
        }
        protected void Seed(AppointDbContext context)
        {
            //base.Seed(context);

            //SuperAdmin User. 


            // Sample User


            // Order

            //


            //



            //



            //




        }
    }
}