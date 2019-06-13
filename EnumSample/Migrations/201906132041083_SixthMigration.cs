namespace OfficeSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SixthMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "PrevItemImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "PrevItemImage");
        }
    }
}
