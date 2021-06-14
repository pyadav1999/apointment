namespace apointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initiallast1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ratings", "rating");
        }
    }
}
