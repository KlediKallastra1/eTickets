using eTickets.Data.Static;
using eTickets.Entities;
using Microsoft.AspNetCore.Identity;

namespace eTickets.Data
{
    public class ApplicationDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                //Cinema
                if (!context.Cinema.Any())
                {
                    context.Cinema.AddRange(new List<Cinema>()
                    {
                        new Cinema(){
                            CinemaName = "Cinema 1",
                            LogoPath = "NoPath",
                            Description = "Demo DEscription"
                        },
                        new Cinema(){
                            CinemaName = "Cinema 2",
                            LogoPath = "NoPath",
                            Description = "Demo DEscription"
                        }
                    });
                    context.SaveChanges();
                }

                //Actors
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor(){
                            ActorName = "Actor 1",
                            ProfilePicturePath = "NoPath",
                            Biography = "Demo Biography"
                        },
                        new Actor(){
                            ActorName = "Actor 2",
                            ProfilePicturePath = "NoPath",
                            Biography = "Demo Biography"
                        }
                    });
                    context.SaveChanges();
                }

                //Producers
                if (!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                    {
                        new Producer(){
                            ProducerName = "Producer 1",
                            ProfilePicturePath = "NoPath",
                            Biography = "Demo Biography"
                        },
                        new Producer(){
                            ProducerName = "Producer 2",
                            ProfilePicturePath = "NoPath",
                            Biography = "Demo Biography"
                        }
                    });
                    context.SaveChanges();
                }

                //Movies
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie(){
                            Name = "Movie 1",
                            Description = "Demo Description",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now,
                            ImagePath = "No Path",
                            Price = 40.76,
                            MovieCategory = MovieCategory.Comedy,
                            CinemaId = 1,
                            ProducerId = 4
                        },
                        new Movie(){
                            Name = "Movie 2",
                            Description = "Demo Description",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now,
                            ImagePath = "No Path",
                            Price =  91.25,
                            MovieCategory = MovieCategory.Action,
                            CinemaId = 2,
                            ProducerId = 3
                        }
                    });
                    context.SaveChanges();
                }

                //Actors_Movies
                if (!context.Actors_Movies.Any())
                {
                    context.Actors_Movies.AddRange(new List<Actor_Movie>()
                    {
                        new Actor_Movie(){
                            ActorId = 2,
                            MovieId = 10
                        },
                        new Actor_Movie(){
                            ActorId = 3,
                            MovieId = 11
                        },
                        new Actor_Movie(){
                            ActorId = 4,
                            MovieId = 12
                        },
                        new Actor_Movie(){
                            ActorId = 3,
                            MovieId = 13
                        },
                        new Actor_Movie(){
                            ActorId = 5,
                            MovieId = 14
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Role's Sector
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin)) await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User)) await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //User's Sector
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var adminUser = await userManager.FindByEmailAsync("admin@eTickets.al");
                if(adminUser == null)
                {
                    var newAdminUser = new ApplicationUser
                    {
                        FirstName = "Admin",
                        LastName = "Kledi",
                        UserName = "admin",
                        Email = "admin@eTickets.al",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Prill2022!");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                var appUser = await userManager.FindByEmailAsync("user@eTickets.al");
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser
                    {
                        FirstName = "Normal User",
                        LastName = "Kledi",
                        UserName = "user",
                        DOB = new DateTime(1999,05,13),
                        Email = "user@eTickets.al",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Prill2022!");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
