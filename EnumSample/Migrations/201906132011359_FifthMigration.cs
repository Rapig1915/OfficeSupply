namespace OfficeSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FifthMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "ItemImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "ItemImage");
        }
    }
}
