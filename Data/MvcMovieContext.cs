using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext(DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<AccountRatings> AccountRatings { get; set; }
        public DbSet<MovieContract> MovieContract { get; set; }
    }
}