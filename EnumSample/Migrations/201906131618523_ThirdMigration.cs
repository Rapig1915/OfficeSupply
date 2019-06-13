namespace OfficeSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryLogs", "InventoryId", c => c.Int(nullable: false));
            AddColumn("dbo.InventoryLogs", "ItemId", c => c.Int(nullable: false));
            AddColumn("dbo.InventoryStocks", "InventoryId", c => c.Int(nullable: false));
            AddColumn("dbo.InventoryStocks", "ItemId", c => c.Int(nullable: false));
            AddColumn("dbo.ItemImages", "ItemId", c => c.Int(nullable: false));
            AddColumn("dbo.VendorSupplyItems", "VendorId", c => c.Int(nullable: false));
            AddColumn("dbo.VendorSupplyItems", "ItemId", c => c.Int(nullable: false));
            CreateIndex("dbo.InventoryLogs", new[] { "InventoryId", "ItemId" }, unique: true, name: "Inventory_Item_Log_Unique");
            CreateIndex("dbo.InventoryStocks", new[] { "InventoryId", "ItemId" }, unique: true, name: "Inventory_Item_Stock_Unique");
            CreateIndex("dbo.ItemImages", "ItemId");
            CreateIndex("dbo.VendorSupplyItems", new[] { "VendorId", "ItemId" }, unique: true, name: "Vendor_Item_Unique");
            AddForeignKey("dbo.InventoryLogs", "InventoryId", "dbo.Inventories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.InventoryLogs", "ItemId", "dbo.Items", "Id", cascadeDelete: true);
            AddForeignKey("dbo.InventoryStocks", "InventoryId", "dbo.Inventories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.InventoryStocks", "ItemId", "dbo.Items", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ItemImages", "ItemId", "dbo.Items", "Id", cascadeDelete: true);
            AddForeignKey("dbo.VendorSupplyItems", "ItemId", "dbo.Items", "Id", cascadeDelete: true);
            AddForeignKey("dbo.VendorSupplyItems", "VendorId", "dbo.Vendors", "Id", cascadeDelete: true);
            DropColumn("dbo.InventoryLogs", "Inventory");
            DropColumn("dbo.InventoryLogs", "Item");
            DropColumn("dbo.InventoryStocks", "Inventory");
            DropColumn("dbo.InventoryStocks", "Item");
            DropColumn("dbo.ItemImages", "Item");
            DropColumn("dbo.VendorSupplyItems", "Vendor");
            DropColumn("dbo.VendorSupplyItems", "Item");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VendorSupplyItems", "Item", c => c.Int(nullable: false));
            AddColumn("dbo.VendorSupplyItems", "Vendor", c => c.Int(nullable: false));
            AddColumn("dbo.ItemImages", "Item", c => c.Int(nullable: false));
            AddColumn("dbo.InventoryStocks", "Item", c => c.Int(nullable: false));
            AddColumn("dbo.InventoryStocks", "Inventory", c => c.Int(nullable: false));
            AddColumn("dbo.InventoryLogs", "Item", c => c.Int(nullable: false));
            AddColumn("dbo.InventoryLogs", "Inventory", c => c.Int(nullable: false));
            DropForeignKey("dbo.VendorSupplyItems", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.VendorSupplyItems", "ItemId", "dbo.Items");
            DropForeignKey("dbo.ItemImages", "ItemId", "dbo.Items");
            DropForeignKey("dbo.InventoryStocks", "ItemId", "dbo.Items");
            DropForeignKey("dbo.InventoryStocks", "InventoryId", "dbo.Inventories");
            DropForeignKey("dbo.InventoryLogs", "ItemId", "dbo.Items");
            DropForeignKey("dbo.InventoryLogs", "InventoryId", "dbo.Inventories");
            DropIndex("dbo.VendorSupplyItems", "Vendor_Item_Unique");
            DropIndex("dbo.ItemImages", new[] { "ItemId" });
            DropIndex("dbo.InventoryStocks", "Inventory_Item_Stock_Unique");
            DropIndex("dbo.InventoryLogs", "Inventory_Item_Log_Unique");
            DropColumn("dbo.VendorSupplyItems", "ItemId");
            DropColumn("dbo.VendorSupplyItems", "VendorId");
            DropColumn("dbo.ItemImages", "ItemId");
            DropColumn("dbo.InventoryStocks", "ItemId");
            DropColumn("dbo.InventoryStocks", "InventoryId");
            DropColumn("dbo.InventoryLogs", "ItemId");
            DropColumn("dbo.InventoryLogs", "InventoryId");
        }
    }
}
