#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Entities;
using Business;
using Business.Models;
using Business.Services;
using Business.Results.Bases;
using Microsoft.AspNetCore.Authorization;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class MoviesController : Controller
    {
        // TODO: Add service injections here
        private readonly IMovieService _movieService;
        private readonly IDirectoryService _directoryService;


		public MoviesController(IMovieService movieService,IDirectoryService directoryService)
        {
            _movieService = movieService;
            _directoryService = directoryService;
        }

        // GET: Movies
        public IActionResult Index()
        {
            List<MovieModel> movieList = _movieService.Query().ToList(); ; // TODO: Add get list service logic here
            return View(movieList);
        }

        // GET: Movies/Details/5
        public IActionResult Details(int id)
        {
            MovieModel movie = _movieService.Query().SingleOrDefault(u => u.Id == id); // TODO: Add get item service logic here
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        // TODO: Add service injections here
        // TODO: Add service injections here
        [Authorize]
        public IActionResult Create()
        {
            if (!User.HasClaim("Director", "true"))
            {
                // If the user is not a director, redirect to the index page
                return RedirectToAction("Index", "Home"); // Change "Home" to the appropriate controller
            }

            // TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewBag.Directors = new SelectList(_directoryService.Query().ToList(), "Id", "UserName");
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MovieModel movie)
        {
            if (!User.HasClaim("Director", "true"))
            {
                // If the user is not a director, redirect to the index page
                return RedirectToAction("Index", "Home"); // Change "Home" to the appropriate controller
            }

            if (ModelState.IsValid)
            {
                // If model data is valid, insert service logic should be written here.
                Result result = _movieService.Add(movie);

                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", result.Message);
            }

            // TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewBag.Directors = new SelectList(_directoryService.Query().ToList(), "Id", "UserName");
            return View(movie);
        }
        // GET: Movies/Edit/5
        public IActionResult Edit(int id)
        {
            if (!User.HasClaim("Director", "true"))
            {
                // If the user is not a director, redirect to the index page
                return RedirectToAction("Login", "Directors"); // Change "Home" to the appropriate controller
            }

            MovieModel movie = _movieService.Query().SingleOrDefault(u => u.Id == id); ; // TODO: Add get item service logic here
            if (movie == null)
            {
                return NotFound();
            }
            // TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewBag.Directors = new SelectList(_directoryService.Query().ToList(), "Id", "UserName");
            return View(movie);
        }

        // POST: Movies/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MovieModel movie)
        {
            if (!User.HasClaim("Director", "true"))
            {
                // If the user is not a director, redirect to the index page
                return RedirectToAction("Login", "Directors"); // Change "Home" to the appropriate controller
            }

            if (ModelState.IsValid)
            {
                var result = _movieService.Update(movie); // update the user in the service
                if (result.IsSuccessful)
                {
                    // if update operation result is successful, carry successful result message to the List view through the GetList action
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewBag.Directors = new SelectList(_directoryService.Query().ToList(), "Id", "UserName");
            return View(movie);
        }

        // GET: Movies/Delete/5
        public IActionResult Delete(int id)
        {
            if (!User.HasClaim("Director", "true"))
            {
                // If the user is not a director, redirect to the index page
                return RedirectToAction("Login", "Directors"); // Change "Home" to the appropriate controller
            }

            MovieModel movie = _movieService.Query().SingleOrDefault(u => u.Id == id); // TODO: Add get item service logic here
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!User.HasClaim("Director", "true"))
            {
                // If the user is not a director, redirect to the index page
                return RedirectToAction("Login", "Directors"); // Change "Home" to the appropriate controller
            }

            var result = _movieService.DeleteUser(id);
            TempData["Message"] = result.Message;
            // TODO: Add delete service logic here
            return RedirectToAction(nameof(Index));
        }
	}
}
