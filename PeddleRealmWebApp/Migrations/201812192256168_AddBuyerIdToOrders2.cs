namespace PeddleRealmWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBuyerIdToOrders2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "Order_BuyerId", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "Order_BuyerId" });
            DropColumn("dbo.OrderDetails", "OrderId");
            RenameColumn(table: "dbo.OrderDetails", name: "Order_BuyerId", newName: "OrderId");
            DropPrimaryKey("dbo.Orders");
            AlterColumn("dbo.OrderDetails", "OrderId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "BuyerId", c => c.String());
            AlterColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Orders", "OrderId");
            CreateIndex("dbo.OrderDetails", "OrderId");
            AddForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders", "OrderId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropPrimaryKey("dbo.Orders");
            AlterColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "BuyerId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.OrderDetails", "OrderId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Orders", "BuyerId");
            RenameColumn(table: "dbo.OrderDetails", name: "OrderId", newName: "Order_BuyerId");
            AddColumn("dbo.OrderDetails", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderDetails", "Order_BuyerId");
            AddForeignKey("dbo.OrderDetails", "Order_BuyerId", "dbo.Orders", "BuyerId");
        }
    }
}
