namespace PeddleRealmWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyToItem : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Items", name: "ItemType_Id", newName: "ItemTypeId");
            RenameIndex(table: "dbo.Items", name: "IX_ItemType_Id", newName: "IX_ItemTypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Items", name: "IX_ItemTypeId", newName: "IX_ItemType_Id");
            RenameColumn(table: "dbo.Items", name: "ItemTypeId", newName: "ItemType_Id");
        }
    }
}
