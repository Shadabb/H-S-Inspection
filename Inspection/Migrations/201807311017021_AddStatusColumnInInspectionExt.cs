namespace Inspection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusColumnInInspectionExt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InspectionExt", "Status", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InspectionExt", "Status");
        }
    }
}
