// Niklas Ekstein 910723-3133
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
/// <summary>
/// The view model that is passed to the view when creating a movie, it takes in a picture that i uploaded.
/// (Problem with picture in Internet explorer, works fine in Google Chrome.)
/// </summary>
namespace MvcMovie.ViewModels
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        
        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [Required]
        [StringLength(30)]
        public string Genre { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [Required]
        [StringLength(30)]
        [Display(Name = "Type")]
        public string MovieType { get; set; }
        
        public string Rating { get; set; }

        [Display(Name = "Trailer link, start with http")]
        public string TrailerLink { get; set; }

        public string Discription { get; set; }

        [Required(ErrorMessage = "Please choose profile image")]
        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }
    }
}
