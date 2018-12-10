using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NUnit.Framework;
using PeddleRealmWebApp.Models;
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
            MigrateDbToLatestVersion();
            CreateRolesAndUsers();
        }

        private static void MigrateDbToLatestVersion()
        {
            var configuration = new PeddleRealmWebApp.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }


        private void CreateRolesAndUsers()
        {
            var context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole { Name = "Admin" };
                roleManager.Create(role);

                //Create default Admin user

                var user = new ApplicationUser { UserName = "MarkT", Email = "MarkT@domain.com" };
                string userPWD = "Tang@2014";

                var createUser = userManager.Create(user, userPWD);

                if (createUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "Admin");
                }
            }

            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole { Name = "User" };
                roleManager.Create(role);

                //Create default User
                var user = new ApplicationUser { UserName = "JohnJ", Email = "JohnJ@domain.com" };
                string userPWD = "Jacobson@2014";

                var createUser = userManager.Create(user, userPWD);

                if (createUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "User");
                }
            }

            context.SaveChanges();
        }
    }
}
