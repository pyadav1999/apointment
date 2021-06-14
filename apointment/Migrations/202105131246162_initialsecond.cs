namespace apointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialsecond : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Nexts",
                c => new
                    {
                        NextId = c.Int(nullable: false, identity: true),
                        AppointId = c.Int(nullable: false),
                        SenderId = c.Int(nullable: false),
                        senderName = c.String(),
                        senderAddess = c.String(),
                        senderState = c.String(),
                        senderEmail = c.String(),
                        sendermobile = c.String(),
                        senderCity = c.String(),
                        SpecialistId = c.Int(nullable: false),
                        specialistName = c.String(),
                        occupation = c.String(),
                        specialistAddress = c.String(),
                        specialistState = c.String(),
                        specialistEmail = c.String(),
                        specialistmobile = c.String(),
                        specialistcity = c.String(),
                        AppointmentDate = c.DateTime(nullable: false),
                        AppointmentTime = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.NextId);
            
            CreateTable(
                "dbo.Previous",
                c => new
                    {
                        PId = c.Int(nullable: false, identity: true),
                        AppointId = c.Int(nullable: false),
                        SenderId = c.Int(nullable: false),
                        senderName = c.String(),
                        senderAddess = c.String(),
                        senderState = c.String(),
                        senderEmail = c.String(),
                        sendermobile = c.String(),
                        senderCity = c.String(),
                        SpecialistId = c.Int(nullable: false),
                        specialistName = c.String(),
                        occupation = c.String(),
                        specialistAddress = c.String(),
                        specialistState = c.String(),
                        specialistEmail = c.String(),
                        specialistmobile = c.String(),
                        specialistcity = c.String(),
                        AppointmentDate = c.DateTime(nullable: false),
                        AppointmentTime = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Previous");
            DropTable("dbo.Nexts");
        }
    }
}
