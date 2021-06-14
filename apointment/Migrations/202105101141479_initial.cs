namespace apointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PendingAppointments",
                c => new
                    {
                        AppointId = c.Int(nullable: false, identity: true),
                        SenderId = c.Int(nullable: false),
                        SpecialistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AppointId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PendingAppointments");
        }
    }
}
