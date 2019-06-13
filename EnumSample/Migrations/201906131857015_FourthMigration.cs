namespace OfficeSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourthMigration : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.InventoryLogs", "Inventory_Item_Log_Unique");
            CreateIndex("dbo.InventoryLogs", "InventoryId");
            CreateIndex("dbo.InventoryLogs", "ItemId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.InventoryLogs", new[] { "ItemId" });
            DropIndex("dbo.InventoryLogs", new[] { "InventoryId" });
            CreateIndex("dbo.InventoryLogs", new[] { "InventoryId", "ItemId" }, unique: true, name: "Inventory_Item_Log_Unique");
        }
    }
}
