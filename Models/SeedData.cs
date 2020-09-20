// Niklas Ekstein 910723-3133
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using System;
using System.Linq;
using System.Collections.Generic;
/// <summary>
/// Seed data for the database. MovieContracts, Perons and Movies are seeded at the moment.
/// </summary>
namespace MvcMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcMovieContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcMovieContext>>()))
            {

                if (context.Movie.Any())
                {
                    return;
                }

                context.Movie.AddRange(
                    new Movie
                    {
                        Title = "Gladiator",
                        ReleaseDate = DateTime.Parse("1999-2-12"),
                        Genre = "Action",
                        Rating = "R",
                        MovieType = "Movie",
                        ProfilePicture = "GladiatorCover.jpg",
                        TrailerLink = "https://www.youtube.com/watch?v=vKQi3bBA1y8"
                    },

                    new Movie
                    {
                        Title = "Matrix",
                        ReleaseDate = DateTime.Parse("2000-3-13"),
                        Genre = "Action",
                        Rating = "R",
                        MovieType = "Movie",
                        ProfilePicture = "MatrixCover.jpg",
                        TrailerLink = "https://www.youtube.com/watch?v=vKQi3bBA1y8"
                    },

                    new Movie
                    {
                        Title = "Seinfeld",
                        ReleaseDate = DateTime.Parse("1989-7-5"),
                        Genre = "Comedy",
                        Rating = "R",
                        MovieType = "TV-Serie",
                        Discription = "TV series about some friends in NYC, was run 1989-1998 in 9 seasons.",
                        ProfilePicture = "SeinfeldCover.jpg",
                        TrailerLink = "https://www.youtube.com/watch?v=vKQi3bBA1y8"
                    }
                );


                if (context.Person.Any())
                {
                    return;
                }

                context.Person.AddRange(
                    new Person
                    {
                        FirstName = "Wayne",
                        LastName = "Rooney",
                        Role = "Actor",
                        ProfilePicture = "Rooney.jpg"
                    },

                    new Person
                    {
                        FirstName = "Cristiano",
                        LastName = "Ronaldo",
                        Role = "Actor",
                        ProfilePicture = "Ronaldo.jpg"
                    },

                    new Person
                    {
                        FirstName = "Steven",
                        LastName = "Gerrard",
                        Role = "Director",
                        ProfilePicture = "Gerrard.jpg"
                    }
                );

                if (context.MovieContract.Any())
                {
                    return;
                }

                context.MovieContract.AddRange(
                    new MovieContract
                    {
                        ContractTitle = "Gladiator",
                        MovieId = 1,
                    },


                    new MovieContract
                    {
                        ContractTitle = "Seinfeld",
                        MovieId = 2,
                    }
                );

                context.SaveChanges();
            }
        }
    }
}