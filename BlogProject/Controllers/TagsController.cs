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
    public class TagsController : Controller
    {
        private readonly IBlogRepository _context;
        private readonly IMapper _mapper;

        public TagsController(IBlogRepository context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
        }

        // GET: Tags
        public async Task<IActionResult> Index()
        {
            return View(_context.GetTagList());
        }

        // GET: Tags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tags = _context.GetTag(id);
            if (tags == null)
            {
                return NotFound();
            }

            return View(tags);
        }

        // GET: Tags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TagsVm tags)
        {
            if (ModelState.IsValid)
            {
                Tags model = _mapper.Map<Tags>(tags);
                _context.AddTag(model);
                _context.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(tags);
        }

        // GET: Tags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tags = _context.GetTag(id);
            if (tags == null)
            {
                return NotFound();
            }
            return View(tags);
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TagsVm tags)
        {
            if (id != tags.TagId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Tags model = _mapper.Map<Tags>(tags);
                    _context.UpdateTag(model);
                    _context.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagsExists(tags.TagId))
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
            return View(tags);
        }

        // GET: Tags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tags = _context.GetTag(id);
            if (tags == null)
            {
                return NotFound();
            }

            return View(tags);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tags = _context.GetTag(id);
            _context.DeleteTag(tags);
            _context.Commit();
            return RedirectToAction(nameof(Index));
        }

        private bool TagsExists(int id)
        {
            return _context.TagExists(id);
        }
    }
}
