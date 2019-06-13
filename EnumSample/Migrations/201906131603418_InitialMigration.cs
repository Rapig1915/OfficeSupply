namespace OfficeSupply.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Inventories", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Inventories", "Description", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Items", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Items", "Variety", c => c.String(nullable: false));
            DropTable("dbo.MineDatas");
            DropTable("dbo.MyDatas");
        }
        
        public override void Down()
        {
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
                "dbo.MineDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Enum1 = c.Byte(nullable: false),
                        Enum2 = c.Byte(),
                        FlagsEnum = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Items", "Variety", c => c.String());
            AlterColumn("dbo.Items", "Name", c => c.String());
            AlterColumn("dbo.Inventories", "Description", c => c.String());
            AlterColumn("dbo.Inventories", "Name", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String());
        }
    }
}
