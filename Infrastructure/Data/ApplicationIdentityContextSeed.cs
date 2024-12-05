using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationIdentityContextSeed
    {
       
       
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                
               
                await SeedUserAsync(userManager);
            }

        }
        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager 
          )
        {
                if ( !userManager.Users.Any())
                {
                     var user =                 
                        new ApplicationUser
                        {

                            UserName = "admin@ecommerce2024.com",
                            Email = "admin@ecommerce2024.com",
                            DisplayName = "Admin El Admins",
                            Address = new Address
                            {
                                Id = Guid.NewGuid().ToString(),
                                FirstName = "Admin",
                                LastName = "El Admins",
                                Street = "10 The Street",
                                City = "New York",
                                State = "NY",
                                Zipcode = "90210"
                            }
                        }
                    ;
               
                var result = await userManager.CreateAsync(user, "Pa$$w0rd");

                if (result.Succeeded)
                {
                    // Optionally , you can add the user to a role
                }
                else
                {
                    throw new Exception("Failed to create the user");
                }

            }           
        }
    }
}
