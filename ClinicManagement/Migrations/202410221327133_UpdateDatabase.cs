namespace ClinicManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "AspNetUsersID", c => c.String());
            AddColumn("dbo.Patients", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Patients", "User_Id");
            AddForeignKey("dbo.Patients", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Patients", new[] { "User_Id" });
            DropColumn("dbo.Patients", "User_Id");
            DropColumn("dbo.Patients", "AspNetUsersID");
        }
    }
}
