namespace apointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initiallaste : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "SenderId", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "SpecialistId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ratings", "SpecialistId");
            DropColumn("dbo.Ratings", "SenderId");
        }
    }
}
