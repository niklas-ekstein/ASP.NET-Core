// Niklas Ekstein 910723-3133
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/// <summary>
/// Actor/Director data is saved in the database, has a MovieContract.
/// </summary>
namespace MvcMovie.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public List<MovieContract> PersonContracts;
        public string ProfilePicture { get; set; }

        public Person()
        {
            PersonContracts = new List<MovieContract>();
        }
    }
}
