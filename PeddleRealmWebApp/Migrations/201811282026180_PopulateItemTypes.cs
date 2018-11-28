namespace PeddleRealmWebApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateItemTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO ItemTypes (Id, Name) VALUES (1, 'Food')");
            Sql("INSERT INTO ItemTypes (Id, Name) VALUES (2, 'Clothes')");
            Sql("INSERT INTO ItemTypes (Id, Name) VALUES (3, 'Tools')");
        }

        public override void Down()
        {
            Sql("DELETE FROM ItemTypes WHERE Id IN (1,2,3)");
        }
    }
}
