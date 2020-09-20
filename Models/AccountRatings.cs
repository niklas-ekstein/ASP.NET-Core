// Niklas Ekstein 910723-3133
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/// <summary>
/// Account Ratings, makes it possible to put diffrent ratings with diffrent accounts on the same movie.
/// </summary>
namespace MvcMovie.Models
{
    public class AccountRatings
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int MovieId { get; set; }
        public string rating { get; set; }
    }
}
