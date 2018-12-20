namespace PeddleRealmWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBuyerIdToOrders1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", new[] { "Order_OrderId", "Order_BuyerId" }, "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "Order_OrderId", "Order_BuyerId" });
            DropColumn("dbo.OrderDetails", "Order_BuyerId");
            RenameColumn(table: "dbo.OrderDetails", name: "Order_OrderId", newName: "Order_BuyerId");
            DropPrimaryKey("dbo.Orders");
            AlterColumn("dbo.OrderDetails", "Order_BuyerId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Orders", "BuyerId");
            CreateIndex("dbo.OrderDetails", "Order_BuyerId");
            AddForeignKey("dbo.OrderDetails", "Order_BuyerId", "dbo.Orders", "BuyerId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "Order_BuyerId", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "Order_BuyerId" });
            DropPrimaryKey("dbo.Orders");
            AlterColumn("dbo.OrderDetails", "Order_BuyerId", c => c.Int());
            AddPrimaryKey("dbo.Orders", new[] { "OrderId", "BuyerId" });
            RenameColumn(table: "dbo.OrderDetails", name: "Order_BuyerId", newName: "Order_OrderId");
            AddColumn("dbo.OrderDetails", "Order_BuyerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.OrderDetails", new[] { "Order_OrderId", "Order_BuyerId" });
            AddForeignKey("dbo.OrderDetails", new[] { "Order_OrderId", "Order_BuyerId" }, "dbo.Orders", new[] { "OrderId", "BuyerId" });
        }
    }
}
