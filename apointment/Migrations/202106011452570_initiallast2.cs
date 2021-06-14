namespace apointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initiallast2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ratings", "AppointId", "dbo.PendingAppointments");
            DropIndex("dbo.Ratings", new[] { "AppointId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Ratings", "AppointId");
            AddForeignKey("dbo.Ratings", "AppointId", "dbo.PendingAppointments", "AppointId", cascadeDelete: true);
        }
    }
}
