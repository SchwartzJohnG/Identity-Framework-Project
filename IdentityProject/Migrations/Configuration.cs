namespace IdentityProject.Migrations
{
    using Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<IdentityProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "IdentityProject.Models.ApplicationDbContext";
        }

        protected override void Seed(IdentityProject.Models.ApplicationDbContext context)
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

            var userStore = new UserStore<ApplicationUser>(context);
            var roleStore = new RoleStore<IdentityRole>(context);

            var userManager = new ApplicationUserManager(userStore);
            var roleManager = new ApplicationRoleManager(roleStore);

            if (!roleManager.RoleExists(ApplicationRoleManager.ROLE_ADMIN)) {
                roleManager.Create(new IdentityRole(ApplicationRoleManager.ROLE_ADMIN));
            }

            var user = userManager.FinuudByName("eric@gmail.com");
            if (user == null) {
                user = new ApplicationUser {
                    UserName = "eric@gmail.com",
                    Email = "eric@gmail.com"
                };
                userManager.Create(user, "Password!1");
            }

            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(ApplicationRoleManager.ROLE_ADMIN)) {
                userManager.AddToRole(user.Id, ApplicationRoleManager.ROLE_ADMIN);
            }
        }
    }
}
