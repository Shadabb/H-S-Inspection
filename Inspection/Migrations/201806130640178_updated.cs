namespace Inspection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Inspection", name: "Created_By", newName: "Created_By");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Inspection", name: "Created_By", newName: "Created_By");
        }
    }
}
