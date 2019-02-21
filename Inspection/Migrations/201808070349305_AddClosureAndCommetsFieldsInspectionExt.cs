namespace Inspection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClosureAndCommetsFieldsInspectionExt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InspectionExt", "ClosureDate", c => c.String(maxLength: 50));
            AddColumn("dbo.InspectionExt", "UpdatedComments", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InspectionExt", "UpdatedComments");
            DropColumn("dbo.InspectionExt", "ClosureDate");
        }
    }
}
