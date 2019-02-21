namespace Inspection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSiteDetailsInInspectionExtTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InspectionExt", "InspectionForm", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.InspectionExt", "SiteName", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.InspectionExt", "SiteId", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InspectionExt", "SiteId");
            DropColumn("dbo.InspectionExt", "SiteName");
            DropColumn("dbo.InspectionExt", "InspectionForm");
        }
    }
}
