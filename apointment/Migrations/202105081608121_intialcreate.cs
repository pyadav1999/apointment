namespace apointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intialcreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Registers", "imgurl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Registers", "imgurl");
        }
    }
}
