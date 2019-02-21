namespace Inspection.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddSiteDetailsInInspectionExtTableUpdate : DbMigration
    {
        public override void Up()
        {
            // Add this line
            RenameColumn("dbo.Inspection", "SiteID", "SiteId");
            RenameColumn("dbo.Inspection", "Created By", "CreatedBy");
            RenameColumn("dbo.InspectionExt", "InspectionID", "InspectionId");
        }

        public override void Down()
        {
            // Add this line
            RenameColumn("dbo.Inspection", "SiteId", "SiteID");
            RenameColumn("dbo.Inspection", "CreatedBy", "Created By");
            RenameColumn("dbo.InspectionExt", "InspectionId", "InspectionID");
        }
    }
}
