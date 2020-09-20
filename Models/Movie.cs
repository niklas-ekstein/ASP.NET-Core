// Niklas Ekstein 910723-3133
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
/// <summary>
/// Model for the movies that are saved in the database.
/// </summary>

namespace MvcMovie.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        public string Genre { get; set; }

        [Display(Name = "Type")]
        public string MovieType { get; set; }

        public string Rating { get; set; }

        public List<Person> CrewList;

        public List<AccountRatings> ARList;

        [Display(Name = "Trailer")]
        public string TrailerLink { get; set; }

        public string Discription { get; set; }

        [Display(Name = "Profile Picture")]
        public string ProfilePicture { get; set; }

        public Movie()
        {
            CrewList = new List<Person>();
            ARList = new List<AccountRatings>();
        }
    }
}