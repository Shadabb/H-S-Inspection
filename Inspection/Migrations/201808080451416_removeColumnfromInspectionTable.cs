namespace Inspection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeColumnfromInspectionTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Inspection", "UserID");
            DropColumn("dbo.Inspection", "ActivityQ");
            DropColumn("dbo.Inspection", "SelectedForm");
            DropColumn("dbo.Inspection", "FurtherActionRequired");
            DropColumn("dbo.Inspection", "Comments");
            DropColumn("dbo.Inspection", "Assignee");
            DropColumn("dbo.Inspection", "Priority");
            DropColumn("dbo.Inspection", "Due");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Inspection", "Due", c => c.DateTime(storeType: "date"));
            AddColumn("dbo.Inspection", "Priority", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.Inspection", "Assignee", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.Inspection", "Comments", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.Inspection", "FurtherActionRequired", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.Inspection", "SelectedForm", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.Inspection", "ActivityQ", c => c.String(nullable: false, maxLength: 200, unicode: false));
            AddColumn("dbo.Inspection", "UserID", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
    }
}
