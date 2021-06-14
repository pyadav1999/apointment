namespace apointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialfirst : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PendingAppointments", "senderName", c => c.String());
            AddColumn("dbo.PendingAppointments", "senderAddess", c => c.String());
            AddColumn("dbo.PendingAppointments", "senderState", c => c.String());
            AddColumn("dbo.PendingAppointments", "senderEmail", c => c.String());
            AddColumn("dbo.PendingAppointments", "sendermobile", c => c.String());
            AddColumn("dbo.PendingAppointments", "senderCity", c => c.String());
            AddColumn("dbo.PendingAppointments", "specialistName", c => c.String());
            AddColumn("dbo.PendingAppointments", "specialistAddress", c => c.String());
            AddColumn("dbo.PendingAppointments", "specialistState", c => c.String());
            AddColumn("dbo.PendingAppointments", "specialistEmail", c => c.String());
            AddColumn("dbo.PendingAppointments", "specialistmobile", c => c.String());
            AddColumn("dbo.PendingAppointments", "specialistcity", c => c.String());
            AddColumn("dbo.PendingAppointments", "AppointmentDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.PendingAppointments", "AppointmentTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PendingAppointments", "AppointmentTime");
            DropColumn("dbo.PendingAppointments", "AppointmentDate");
            DropColumn("dbo.PendingAppointments", "specialistcity");
            DropColumn("dbo.PendingAppointments", "specialistmobile");
            DropColumn("dbo.PendingAppointments", "specialistEmail");
            DropColumn("dbo.PendingAppointments", "specialistState");
            DropColumn("dbo.PendingAppointments", "specialistAddress");
            DropColumn("dbo.PendingAppointments", "specialistName");
            DropColumn("dbo.PendingAppointments", "senderCity");
            DropColumn("dbo.PendingAppointments", "sendermobile");
            DropColumn("dbo.PendingAppointments", "senderEmail");
            DropColumn("dbo.PendingAppointments", "senderState");
            DropColumn("dbo.PendingAppointments", "senderAddess");
            DropColumn("dbo.PendingAppointments", "senderName");
        }
    }
}
