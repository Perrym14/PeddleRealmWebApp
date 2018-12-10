using NUnit.Framework;
using System.Data.Entity.Migrations;

namespace PeddleRealm.IntegrationTests
{
    [SetUpFixture]
    public class GlobalSetUp
    {
        [OneTimeSetUp]
        public void SetUp()
        //If database doesn't exist, it will be created
        //and then will be migrated to the latest version.
        {
            var configuration = new PeddleRealmWebApp.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }
    }
}
