using Amazon.Data.Static;
using Amazon.Models;
using Microsoft.AspNetCore.Identity;
using static Amazon.Data.Enums.Enums;

namespace Amazon.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AmazonDbContext>();

                context.Database.EnsureCreated();

                if (!context.Vendor.Any())
                {
                    //Vendors 
                    context.Vendor.AddRange(new List<Vendor>()
                    {
                        new Vendor()
                        {
                            
                            Name = "Fruity Fruits",
                            Location = "Ismailia",
                            Phone = "0123456123",
                        },
                         new Vendor()
                        {
                             
                            Name = "Greeny Vegetables",
                            Location = "Minya",
                            Phone = "0123456789",
                        },
                          new Vendor()
                        {
                            
                            Name = "ITT Market",
                            Location = "Smart Village",
                            Phone = "01112233445",
                        }
                    });
                    context.SaveChanges();
                }
                //Categories
                if (!context.categories.Any())
                {
                    context.categories.AddRange(new List<Category>()
                    {
                        new Category()
                        {
                            
                            Name = "Fruits"
                        },
                        new Category()
                        {
                            
                            Name = "Vegetables"
                        },
                        new Category()
                        {
                            
                            Name = "Fast Food"
                        },
                    });
                context.SaveChanges();
                }

                //products
                if (!context.Products.Any())
                {
                    context.Products.AddRange(new List<Product>()
                    {
                        new Product()
                        {
                            Name = "Banana",
                            PricePerUnit = 12,
                            Description = "Fresh Banana",
                            ImageURL = "img/product/product-2.jpg",
                            CategoryId = 1,
                            VendorId = 1
                        },
                        new Product()
                        {
                            Name = "Guava",
                            PricePerUnit = 15,
                            Description = "Fresh Guava",
                            ImageURL = "img/product/product-3.jpg",
                            CategoryId = 1,
                            VendorId = 1
                        },
                        new Product()
                        {
                            Name = "Berry",
                            PricePerUnit = 25,
                            Description = "Fresh Berry",
                            ImageURL = "img/product/product-4.jpg",
                            CategoryId = 1,
                            VendorId = 1
                        },
                        new Product()
                        {
                            Name = "Mango",
                            PricePerUnit = 11,
                            Description = "Fresh Mango",
                            ImageURL = "img/product/product-6.jpg",
                            CategoryId = 1,
                            VendorId = 1
                        },
                        new Product()
                        {
                            Name = "Watermelon",
                            PricePerUnit = 20,
                            Description = "Fresh Watermelon",
                            ImageURL = "img/product/product-7.jpg",
                            CategoryId = 1,
                            VendorId = 1
                        },
                        new Product()
                        {
                            Name = "Apple",
                            PricePerUnit = 27,
                            Description = "Fresh Apple",
                            ImageURL = "img/product/product-8.jpg",
                            CategoryId = 1,
                            VendorId = 1
                        },
                        new Product()
                        {
                            Name = "Meat",
                            PricePerUnit = 150,
                            Description = "Fresh Meat",
                            ImageURL = "img/product/product-1.jpg",
                            CategoryId = 2,
                            VendorId = 2
                        },
                        new Product()
                        {
                            Name = "Fried Chicken",
                            PricePerUnit = 90,
                            Description = "Fresh Chicken",
                            ImageURL = "img/product/product-10.jpg",
                            CategoryId = 2,
                            VendorId = 2
                        },
                        new Product()
                        {
                            Name = "Juice",
                            PricePerUnit = 5,
                            Description = "Fresh Juice",
                            ImageURL = "img/product/product-11.jpg",
                            CategoryId = 3,
                            VendorId = 3
                        },
                        new Product()
                        {
                            Name = "Burger",
                            PricePerUnit = 15,
                            Description = "Yummy Burger",
                            ImageURL = "img/product/product-5.jpg",
                            CategoryId = 3,
                            VendorId = 3
                        },
                    });
                    context.SaveChanges();

                }





            }

        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.Client))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Client));
                if (!await roleManager.RoleExistsAsync(UserRoles.Vendor))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Vendor));

                //Users
                //------ Admin
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserEmail = "admin@Amazon.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new User()
                    {
                        Name = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        UserType = UserType.Admin

                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                //------ User
                string appUserEmail = "user@Amazon.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new User()
                    {
                        Name = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        UserType = UserType.Client

                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.Client);
                }

                //----- Vendor
                string appVendorEmail = "Vendor@Amazon.com";

                var appVendor = await userManager.FindByEmailAsync(appVendorEmail);
                if (appVendor == null)
                {
                    var newAppVendor = new User()
                    {
                        Name = "Application Vendor",
                        UserName = "app-Vendor",
                        Email = appVendorEmail,
                        EmailConfirmed = true,
                        UserType = UserType.Vendor
                    };
                    await userManager.CreateAsync(newAppVendor, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppVendor, UserRoles.Vendor);
                }

            }
        }
    }
}
