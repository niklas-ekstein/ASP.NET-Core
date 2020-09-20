// Niklas Ekstein 910723-3133
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
/// <summary>
/// Model for the index in movie controller.
/// </summary>
namespace MvcMovie.Models
{
    public class MovieGenreViewModel
    {
        public List<Movie> Movies { get; set; }
        public SelectList Genres { get; set; }
        public string MovieGenre { get; set; }
        public string SearchString { get; set; }
    }
}