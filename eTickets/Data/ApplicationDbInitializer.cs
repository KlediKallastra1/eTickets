using eTickets.Entities;

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
                            ProducerId = 2
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
                            ProducerId = 1
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
                            MovieId = 3
                        },
                        new Actor_Movie(){
                            ActorId = 1,
                            MovieId = 4
                        },
                        new Actor_Movie(){
                            ActorId = 1,
                            MovieId = 4
                        },
                        new Actor_Movie(){
                            ActorId = 2,
                            MovieId = 3
                        }
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
