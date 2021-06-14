namespace apointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initiallast : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        rateid = c.Int(nullable: false, identity: true),
                        review = c.String(),
                        AppointId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.rateid)
                .ForeignKey("dbo.PendingAppointments", t => t.AppointId, cascadeDelete: true)
                .Index(t => t.AppointId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "AppointId", "dbo.PendingAppointments");
            DropIndex("dbo.Ratings", new[] { "AppointId" });
            DropTable("dbo.Ratings");
        }
    }
}
