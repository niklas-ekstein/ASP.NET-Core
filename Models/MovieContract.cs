// Niklas Ekstein 910723-3133
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/// <summary>
/// Actors/Directors are assigned contracts in the movie controller to see what movies they work in.
/// </summary>
namespace MvcMovie.Models
{
    public class MovieContract
    {
        public int Id { get; set; }
        public string ContractTitle { get; set; }
        public int MovieId { get; set; }

    }
}
