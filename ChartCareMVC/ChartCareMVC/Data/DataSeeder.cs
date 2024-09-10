using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ChartCareMVC.Data
{
    public class DataSeeder
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            Console.WriteLine("\n\n\nSEEDROLES WAS CALLED\n\n\n");
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "ADMIN", "IT", "Manager", "User" };

            foreach (var roleName in roleNames)
            {
                Console.WriteLine("\n\n\nATTEMPTING TO ADD ROLE\n\n\n");
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

    }
}
