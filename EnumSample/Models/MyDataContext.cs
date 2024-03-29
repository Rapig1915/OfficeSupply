﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OfficeSupply.Models
{
    public class MyDataContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MyDataContext() : base("name=MyDataContext")
        {
        }

        /// <summary>
        /// Migration for tables
        /// </summary>
        public System.Data.Entity.DbSet<OfficeSupply.Models.Category> Categories { get; set; }
        public System.Data.Entity.DbSet<OfficeSupply.Models.Item> Items { get; set; }
        public System.Data.Entity.DbSet<OfficeSupply.Models.Vendor> Vendors { get; set; }
        public System.Data.Entity.DbSet<OfficeSupply.Models.VendorSupplyItem> VendorSupplyItems { get; set; }
        public System.Data.Entity.DbSet<OfficeSupply.Models.Inventory> Inventories { get; set; }
        public System.Data.Entity.DbSet<OfficeSupply.Models.InventoryLog> InventoryLogs { get; set; }
        public System.Data.Entity.DbSet<OfficeSupply.Models.InventoryStock> InventoryStocks { get; set; }

    }
}
