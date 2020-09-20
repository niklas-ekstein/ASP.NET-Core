// Niklas Ekstein 910723-3133
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/// <summary>
/// Helps the movie controller to see if there is an account logged in.
/// </summary>
namespace MvcMovie.Models
{
    public static class Session
    {
        public static Account usernameSession;
    }
}