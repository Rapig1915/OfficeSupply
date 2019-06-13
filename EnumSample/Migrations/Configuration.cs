namespace OfficeSupply.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<OfficeSupply.Models.MyDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OfficeSupply.Models.MyDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Categories.AddOrUpdate(
                new Category { Id = 1, Name = "Paper" },
                new Category { Id = 2, Name = "Pen" }
                );

            context.Items.AddOrUpdate(
                new Item { Id = 1, CategoryId = 1, Name = "A4", Variety = "#4169E1" },
                new Item { Id = 2, CategoryId = 1, Name = "A5", Variety = "#8B4513" },
                new Item { Id = 3, CategoryId = 2, Name = "Majik", Variety = "#F4A460" },
                new Item { Id = 4, CategoryId = 2, Name = "GelPen", Variety = "#D8BFD8" }
                );

            context.Vendors.AddOrUpdate(
                new Vendor { Id = 1, Name = "Mike" },
                new Vendor { Id = 2, Name = "Ben" }
                );

            context.VendorSupplyItems.AddOrUpdate(
                new VendorSupplyItem { Id = 1, ItemId = 1, VendorId = 1 },
                new VendorSupplyItem { Id = 2, ItemId = 2, VendorId = 1 },
                new VendorSupplyItem { Id = 3, ItemId = 3, VendorId = 1 },
                new VendorSupplyItem { Id = 4, ItemId = 3, VendorId = 2 },
                new VendorSupplyItem { Id = 5, ItemId = 4, VendorId = 2 }
                );

            context.Inventories.AddOrUpdate(
                new Inventory { Id = 1, Name = "InventoryA", Description = "First Inventory" },
                new Inventory { Id = 2, Name = "InventoryB", Description = "Second Inventory" }
                );
        }
    }
}
