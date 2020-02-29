using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogProject.Data;
using BlogProject.Models;
using BlogProject.Data.services;
using AutoMapper;
using BlogProject.ViewModels;

namespace BlogProject.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly IBlogRepository _context;
        private readonly IMapper _mapper;

        public CategoriesController(IBlogRepository context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GetCategoryList();
            return View(applicationDbContext);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = _context.GetCategory(id);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            ViewData["ParentId"] = new SelectList(_context.GetCategoryList(), "CategoryId", "Title");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriesVm categories)
        {
            if (ModelState.IsValid)
            {
                Categories model = _mapper.Map<Categories>(categories);
                _context.AddCategory(model);
                _context.Commit();
                AddMessage("success", "Added successfully");
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(_context.GetCategoryList(), "CategoryId", "Title", categories.ParentId);
            return View(categories);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = _context.GetCategory(id);
            if (categories == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = new SelectList(_context.GetCategoryList(), "CategoryId", "Title", categories.ParentId);
            return View(categories);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoriesVm categories)
        {
            if (id != categories.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Categories model = _mapper.Map<Categories>(categories);
                    _context.UpdateCategory(model);
                    _context.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriesExists(categories.CategoryId))
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
            ViewData["ParentId"] = new SelectList(_context.GetCategoryList(), "CategoryId", "Title", categories.ParentId);
            return View(categories);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = _context.GetCategory(id);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categories = _context.GetCategory(id);
            _context.DeleteCategory(categories);
            _context.Commit();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriesExists(int id)
        {
            return _context.CategoryExists(id);
        }
    }
}
