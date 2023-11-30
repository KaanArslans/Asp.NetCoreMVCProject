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
	}
}
