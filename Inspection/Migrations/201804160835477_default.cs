namespace Inspection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _default : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompliantQuestion",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CompliantQues = c.String(maxLength: 200, unicode: false),
                        InspectionForm = c.String(maxLength: 50, unicode: false),
                        CompliantNo = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.InspectionExt",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InspectionID = c.Int(nullable: false),
                        CompliantNo = c.Int(nullable: false),
                        Compliant = c.String(maxLength: 50, unicode: false),
                        Remarks = c.String(maxLength: 50, unicode: false),
                        Severity = c.String(maxLength: 50, unicode: false),
                        Assignee = c.String(maxLength: 50, unicode: false),
                        DueDate = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Inspection",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InspectionForm = c.String(nullable: false, maxLength: 200, unicode: false),
                        SiteName = c.String(nullable: false, maxLength: 200, unicode: false),
                        SiteID = c.String(nullable: false, maxLength: 100, unicode: false),
                        AreaOfBusiness = c.String(nullable: false, maxLength: 200, unicode: false),
                        AreaInspected = c.String(nullable: false, maxLength: 200, unicode: false),
                        InspectedPerson = c.String(nullable: false, maxLength: 200, unicode: false),
                        ActivityName = c.String(nullable: false, maxLength: 300, unicode: false),
                        InspectionDate = c.DateTime(nullable: false, storeType: "date"),
                        CreatedBy = c.String(name: "Created By", nullable: false, maxLength: 100, unicode: false),
                        UserID = c.String(nullable: false, maxLength: 100, unicode: false),
                        ActivityQ = c.String(nullable: false, maxLength: 200, unicode: false),
                        SelectedForm = c.String(maxLength: 50, unicode: false),
                        Created = c.DateTime(nullable: false, storeType: "date"),
                        FurtherActionRequired = c.String(maxLength: 50, unicode: false),
                        Comments = c.String(maxLength: 50, unicode: false),
                        Assignee = c.String(maxLength: 50, unicode: false),
                        Priority = c.String(maxLength: 50, unicode: false),
                        Due = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Inspection");
            DropTable("dbo.InspectionExt");
            DropTable("dbo.CompliantQuestion");
        }
    }
}
