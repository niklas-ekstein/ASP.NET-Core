// Niklas Ekstein 910723-3133
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;
using MvcMovie.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
/// <summary>
/// Central class that handles the movies along with other data connecting to the movies.
/// Adding, editing, deleting and setting up movies in a well organized index list.
/// In the list conditions based on who is logged in and what rating they assigned movies are shown.
/// </summary>
namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public Movie movieCreate;

        public MoviesController(MvcMovieContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.Movie
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync()
            };

            //Loops AccontRatings and Movies in the database to get the ratings for each account.
            foreach (Movie m in movieGenreVM.Movies)
            {
                Account AccLogged = GetLoggedInAcc();
                var testAR = _context.AccountRatings;

                if (AccLogged != null)
                {
                    foreach (AccountRatings AR in testAR)
                    {
                        if (AR.MovieId == m.Id)
                        {
                            if (AR.AccountId == AccLogged.Id)
                            {
                                //If there is not rating on the account, the last rating from antoher account will show.
                                m.Rating = AR.rating + " rating by you " + AccLogged.Username;
                            }
                        }
                    }
                }
                //Not logged in users gets empty string rating.
                else
                {
                    m.Rating = "";
                }

            }

            return View(movieGenreVM);
        }

        public Account GetLoggedInAcc()
        {
            return Session.usernameSession;
        }

        public async Task<IActionResult> MovieMakers(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            //AssignContractAMovie and AssignContractToActor can be done in another way, somewhere else.
            AssignContractAMovie();
            AssignContractToActor();

            return View(person);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            AssignContractAMovie();
            AssignContractToActor();

            movie = findCrew(movie);


            return View(movie);
        }

        public void AssignContractAMovie()
        {
            foreach (var con in _context.MovieContract)
            {
                foreach (var mv in _context.Movie)
                {
                    if (mv.Title == con.ContractTitle)
                    {
                        con.ContractTitle = mv.Title;
                    }

                }
            }
        }


        //Here, all the contracts are added to all the actors/directors.
        //Right now it assigns all the contracts to all the actors.
        //The contracts and actors are created in the SeedData class.
        public void AssignContractToActor()
        {

            foreach (var per in _context.Person)
            {
                //movie.Title = "test22";
                foreach (var mc in _context.MovieContract)
                {
                    per.PersonContracts.Add(mc);
                }
            }
        }


        //Finds the crew to the crew list.
        public Movie findCrew(Movie movie)
        {
            var testMC2 = _context.MovieContract;

            foreach (Person per in _context.Person)
            {
                foreach (MovieContract mc in testMC2)
                {
                    if (movie.Id == mc.MovieId)
                    {
                        movie.CrewList.Add(per);
                    }
                }
            }
            return movie;
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            Account AccLogged = GetLoggedInAcc();
            
            if (AccLogged != null)
            {
                return View();
            }
            else
            {
                return new EmptyResult();
            }
        }



        /// <summary>
        /// Creates a movie based on the model that is passed in.
        /// Saves the movie and the rating in the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        public async Task<IActionResult> Create(MovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                movieCreate = new Movie
                {
                    Title = model.Title,
                    ReleaseDate = model.ReleaseDate,
                    Genre = model.Genre,
                    MovieType = model.MovieType,
                    Rating = model.Rating,
                    TrailerLink = model.TrailerLink,
                    Discription = model.Discription,
                    ProfilePicture = uniqueFileName,
                };


                Account LoggedAcc = GetLoggedInAcc();
                AccountRatings ARvalues = new AccountRatings();

                ARvalues.AccountId = LoggedAcc.Id;
                ARvalues.MovieId = movieCreate.Id;
                ARvalues.rating = movieCreate.Rating;

                movieCreate.ARList.Add(ARvalues);
                _context.Update(ARvalues);

                _context.Add(movieCreate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(movieCreate);
        }

        private string UploadedFile(MovieViewModel model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            else
            {
                uniqueFileName = "Null";
            }

            return uniqueFileName;
        }

        public async Task<IActionResult> Edit(int? id)
        {
            Account AccLogged = GetLoggedInAcc();

            if (AccLogged != null)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var movie = await _context.Movie.FindAsync(id);
                if (movie == null)
                {
                    return NotFound();
                }
                return View(movie);
            }
            else
            {
                return new EmptyResult();
            }

        }



        //Edits the movie data and updates the database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Rating,MovieType,TrailerLink,Discription,ProfilePicture")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Account LoggedAcc = GetLoggedInAcc();
                    if (LoggedAcc != null)
                    {
                        AccountRatings ARvalues = new AccountRatings();

                        ARvalues.AccountId = LoggedAcc.Id;
                        ARvalues.MovieId = movie.Id;
                        ARvalues.rating = movie.Rating;

                        movie.ARList.Add(ARvalues);
                        _context.Update(ARvalues);
                        _context.Update(movie);

                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movieCreate);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            Account AccLogged = GetLoggedInAcc();

            if (AccLogged != null)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var movie = await _context.Movie
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (movie == null)
                {
                    return NotFound();
                }

                return View(movie);
            }
            else
            {
                return new EmptyResult();
            }

        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}