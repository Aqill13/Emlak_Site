using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace EmlakUI.Areas.Admin.Identity
{
    public static class SeedIdentity
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedService = app.ApplicationServices.CreateScope();

            var serciceProvider = scopedService.ServiceProvider;

            await Seed(serciceProvider);

            return app;
        }
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<UserAdmin>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var admin = new UserAdmin()
            {
                FullName = "Aqil Kalbiyev",
                UserName = "admin",
                Email = "admin@gmail.com"
            };

            if (await userManager.FindByNameAsync(admin.FullName) == null)
            {
                var createAdmin = await userManager.CreateAsync(admin,"admin01");
            }

            if (await roleManager.FindByNameAsync(admin.UserName) == null)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = admin.UserName
                };
                var createRole = await roleManager.CreateAsync(role);
                var result = await userManager.AddToRoleAsync(admin, role.Name);
            }
        }
    }
}
