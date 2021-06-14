namespace apointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Register_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Register_Id");
            AddForeignKey("dbo.AspNetUsers", "Register_Id", "dbo.Registers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Register_Id", "dbo.Registers");
            DropIndex("dbo.AspNetUsers", new[] { "Register_Id" });
            DropColumn("dbo.AspNetUsers", "Register_Id");
        }
    }
}
