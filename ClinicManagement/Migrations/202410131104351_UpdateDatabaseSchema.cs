namespace ClinicManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabaseSchema : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "email");
        }
    }
}
