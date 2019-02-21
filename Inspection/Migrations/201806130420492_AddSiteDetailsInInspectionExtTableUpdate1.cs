namespace Inspection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSiteDetailsInInspectionExtTableUpdate1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Inspection", "UserID", "UserId");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Inspection", "UserId", "UserID");
        }
    }
}
