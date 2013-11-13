namespace EFDemos.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReportedAt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Positions", "ReportedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Positions", "ReportedAt");
        }
    }
}
