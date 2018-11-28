using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using PeddleRealmWebApp.Models;

[assembly: OwinStartupAttribute(typeof(PeddleRealmWebApp.Startup))]
namespace PeddleRealmWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesAndUsers();
        }

        //This  method creates default User roles and Admin user for login if not already existing.
        private void createRolesAndUsers()
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
            }
        }
    }
}
