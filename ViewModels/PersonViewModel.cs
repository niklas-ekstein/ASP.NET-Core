// Niklas Ekstein 910723-3133
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
/// <summary>
/// View model for when an actor/director is clicked, takes in a picture.
/// </summary>
namespace MvcMovie.ViewModels
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public IFormFile ProfileImage { get; set; }
    }
}
