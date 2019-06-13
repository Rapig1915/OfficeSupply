namespace OfficeSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAnnotations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InventoryLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Inventory = c.Int(nullable: false),
                        Item = c.Int(nullable: false),
                        InOrOut = c.Boolean(nullable: false),
                        Count = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InventoryStocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Inventory = c.Int(nullable: false),
                        Item = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Item = c.Int(nullable: false),
                        ImageURL = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(),
                        Variety = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.MineDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Enum1 = c.Byte(nullable: false),
                        Enum2 = c.Byte(),
                        FlagsEnum = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MyDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Enum1 = c.Byte(nullable: false),
                        Enum2 = c.Byte(),
                        FlagsEnum = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VendorSupplyItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Vendor = c.Int(nullable: false),
                        Item = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Items", new[] { "CategoryId" });
            DropTable("dbo.VendorSupplyItems");
            DropTable("dbo.Vendors");
            DropTable("dbo.MyDatas");
            DropTable("dbo.MineDatas");
            DropTable("dbo.Items");
            DropTable("dbo.ItemImages");
            DropTable("dbo.InventoryStocks");
            DropTable("dbo.InventoryLogs");
            DropTable("dbo.Inventories");
            DropTable("dbo.Categories");
        }
    }
}
