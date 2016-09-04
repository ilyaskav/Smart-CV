namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingLinkfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResumeManagers", "Link", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ResumeManagers", "Link");
        }
    }
}
