// Niklas Ekstein 910723-3133
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{

    /// <summary>
    /// Account, there are no requirments to creating a account. The password is not hidden either.
    /// </summary>
    public class Account
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
        public List<AccountRatings> ARList;
        public Account()
        {
            ARList = new List<AccountRatings>();
        }
    }
}
