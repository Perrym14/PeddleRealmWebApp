namespace PeddleRealmWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBuyerIdToOrders : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropPrimaryKey("dbo.Orders");
            AddColumn("dbo.OrderDetails", "Order_OrderId", c => c.Int());
            AddColumn("dbo.OrderDetails", "Order_BuyerId", c => c.String(maxLength: 128));
            AddColumn("dbo.Orders", "BuyerId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Orders", new[] { "OrderId", "BuyerId" });
            CreateIndex("dbo.OrderDetails", new[] { "Order_OrderId", "Order_BuyerId" });
            AddForeignKey("dbo.OrderDetails", new[] { "Order_OrderId", "Order_BuyerId" }, "dbo.Orders", new[] { "OrderId", "BuyerId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", new[] { "Order_OrderId", "Order_BuyerId" }, "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "Order_OrderId", "Order_BuyerId" });
            DropPrimaryKey("dbo.Orders");
            AlterColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Orders", "BuyerId");
            DropColumn("dbo.OrderDetails", "Order_BuyerId");
            DropColumn("dbo.OrderDetails", "Order_OrderId");
            AddPrimaryKey("dbo.Orders", "OrderId");
            CreateIndex("dbo.OrderDetails", "OrderId");
            AddForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders", "OrderId", cascadeDelete: true);
        }
    }
}
