namespace apointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialfirst1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PendingAppointments", "occupation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PendingAppointments", "occupation");
        }
    }
}
