using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace OrchidsWithLove_.Models
{
	public class AppDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
	{
		protected override void Seed(ApplicationDbContext context)
		{
			var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

			var adminRole = new IdentityRole { Name = "admin" };

			roleManager.Create(adminRole);

			var admin = new ApplicationUser { UserName = "Elena", Email = "postolskaya.elena@gmail.com" };
			string password = "admin_Elena1";
			var result = userManager.Create(admin, password);

			if (result.Succeeded)
			{
				userManager.AddToRole(admin.Id, adminRole.Name);
			}

			admin = new ApplicationUser { UserName = "Kristina", Email = "Kristinockakolkunova@gmail.com" };
			password = "admin_Kristina1";
			result = userManager.Create(admin, password);

			if (result.Succeeded)
			{
				userManager.AddToRole(admin.Id, adminRole.Name);
			}

			base.Seed(context);
		}
	}
}