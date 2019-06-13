namespace OfficeSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeventhMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ItemImages", "ItemId", "dbo.Items");
            DropIndex("dbo.ItemImages", new[] { "ItemId" });
            DropTable("dbo.ItemImages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ItemImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        ImageURL = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ItemImages", "ItemId");
            AddForeignKey("dbo.ItemImages", "ItemId", "dbo.Items", "Id", cascadeDelete: true);
        }
    }
}
