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
using Business.Models;
using Business.Services;
using System.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
//Generated from Custom Template.
namespace MVC.Controllers
{
    public class DirectorsController : Controller
    {
        // TODO: Add service injections here
        private readonly IDirectoryService _directorService;

        public DirectorsController(IDirectoryService directorService)
        {
            _directorService = directorService;
        }

        // GET: Directors
        public IActionResult Index()
        {

            if (!User.HasClaim("Director", "true"))
            {
                // If the user is not a director, redirect to the index page
                return RedirectToAction("Login", "Directors"); // Change "Home" to the appropriate controller
            }

            List<DirectoryModel> directorList = _directorService.Query().ToList(); ; // TODO: Add get list service logic here
            return View(directorList);
        }

        // GET: Directors/Details/5
        public IActionResult Details(int id)
        {
            DirectoryModel director = _directorService.Query().SingleOrDefault(r => r.Id == id); ; // TODO: Add get item service logic here
            if (director == null)
            {
                return NotFound();
            }
            return View(director);
        }

        // GET: Directors/Create
        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View();
        }

        // POST: Directors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DirectoryModel director)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                var result = _directorService.Add(director);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message; // we must put TempData["Message"] in the Index view
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(director);
        }

        // GET: Directors/Edit/5
        public IActionResult Edit(int id)
        {
            DirectoryModel director = _directorService.Query().SingleOrDefault(r => r.Id == id); ; // TODO: Add get item service logic here
            if (director == null)
            {
                return NotFound();
            }
            // TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(director);
        }

        // POST: Directors/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DirectoryModel director)
        {
            if (ModelState.IsValid)
            {
                var result = _directorService.Update(director);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message; // we must put TempData["Message"] in the Index view
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(director);
        }

        // GET: Directors/Delete/5
        public IActionResult Delete(int id)
        {
            var result = _directorService.Delete(id);
            TempData["Message"] = result.Message; // we must put TempData["Message"] in the Index view
            return RedirectToAction(nameof(Index));
        }

        // POST: Directors/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            return RedirectToAction(nameof(Index));
        }
        #region User Authentication
        public IActionResult Login()
        {
            return View(); // returning the Login view to the user for entering the user name and password
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Login(DirectoryModel director)
        {
            // checking the active user from the database table by the user name and password
            var existingUser = _directorService.Query().SingleOrDefault(u => u.UserName == director.UserName && u.Surname == director.Surname);
            if (existingUser is null) // if an active user with the entered user name and password can't be found in the database table
            {
                ModelState.AddModelError("", "Invalid user name and surname!"); // send the invalid message to the view's validation summary 
                return View(); // returning the Login view
            }

            // Creating the claim list that will be hashed in the authentication cookie which will be sent with each request to the web application.
            // Only non-critical user data, which will be generally used in the web application such as user name to show in the views or user role
            // to check if the user is authorized to perform specific actions, should be put in the claim list.
            // Critical data such as password must never be put in the claim list!
            List<Claim> userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, existingUser.UserName),
                new Claim(ClaimTypes.Surname, existingUser.Surname),
                new Claim("Director", "true")
            };

            // creating an identity by the claim list and default cookie authentication
            var userIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

            // creating a principal by the identity
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            // signing the user in to the MVC web application and returning the hashed authentication cookie to the client
            HttpContext.SignInAsync(userPrincipal);

            // redirecting user to the home page
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }



}

