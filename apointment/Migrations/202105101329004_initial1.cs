namespace apointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PendingAppointments", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.PendingAppointments", "RequestDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PendingAppointments", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "Appointment_AppointId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Appointment_AppointId");
            AddForeignKey("dbo.AspNetUsers", "Appointment_AppointId", "dbo.PendingAppointments", "AppointId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Appointment_AppointId", "dbo.PendingAppointments");
            DropIndex("dbo.AspNetUsers", new[] { "Appointment_AppointId" });
            DropColumn("dbo.AspNetUsers", "Appointment_AppointId");
            DropColumn("dbo.PendingAppointments", "IsDelete");
            DropColumn("dbo.PendingAppointments", "RequestDate");
            DropColumn("dbo.PendingAppointments", "IsActive");
        }
    }
}
