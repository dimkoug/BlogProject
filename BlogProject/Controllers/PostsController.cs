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
using System.Collections.ObjectModel;
using cloudscribe.Pagination.Models;

namespace BlogProject.Controllers
{
    public class PostsController : Controller
    {
        private readonly IBlogRepository _context;
        private readonly IMapper _mapper;

        public PostsController(IBlogRepository context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
        }

        // GET: Posts
        public async Task<IActionResult> Index(string title, int? CategoryId, string from, string to, int pageNumber=1, int PageSize=1)
        {
            
            int ExcludeRecords = (PageSize * pageNumber) - PageSize;
            var applicationDbContext = _context.GetPostList().Skip(ExcludeRecords).Take(PageSize);
            if (!String.IsNullOrEmpty(title))
            {
                applicationDbContext = applicationDbContext.Where(c => c.Title.Contains(title));
            }
            if (CategoryId > 0)
            {
                applicationDbContext = applicationDbContext.Where(c => c.CategoryId == CategoryId);
            }
            if (!String.IsNullOrEmpty(from))
            {
                DateTime from_date = DateTime.Parse(from);
                applicationDbContext = applicationDbContext.Where(c => c.DateCreated >= from_date);
            }
            if (!String.IsNullOrEmpty(to))
            {
                DateTime to_date = DateTime.Parse(to);
                applicationDbContext = applicationDbContext.Where(c => c.DateCreated <= to_date);
            }
            ViewBag.Categories = new SelectList(_context.GetCategoryList(), "CategoryId", "Title");
            var result = new PagedResult<Posts>
            {
                Data = applicationDbContext.ToList(),
                TotalItems = _context.GetPostList().Count(),
                PageNumber = pageNumber,
                PageSize = PageSize
            };
            return View(result);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts = _context.GetPost(id);
            if (posts == null)
            {
                return NotFound();
            }

            return View(posts);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.GetCategoryList(), "CategoryId", "Title");
            ViewData["Tags"] = new SelectList(_context.GetTagList(), "TagId", "Title");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostsVm posts, string[] TagPosts)
        {
            if (TagPosts != null)
            {
                posts.TagPosts = new List<TagPosts>();
                foreach (var tag in TagPosts)
                {
                    var tagToAdd = new TagPosts { TagId = Convert.ToInt32(tag), PostId = posts.PostId };
                    posts.TagPosts.Add(tagToAdd);
                }
            }

            if (ModelState.IsValid)
            {
                Posts model = _mapper.Map<Posts>(posts);
                DateTime date = DateTime.Now;
                model.DateCreated = date;
                _context.AddPost(model);
                _context.Commit();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.GetCategoryList(), "CategoryId", "Title", posts.CategoryId);
            ViewData["Tags"] = new MultiSelectList(_context.GetTagList().AsEnumerable(), "TagId", "Title", posts.TagPosts.Select(e=>e.TagId).AsEnumerable());
            return View(posts);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts = _context.GetPost(id);
            PostsVm vm = _mapper.Map<PostsVm>(posts);
            if (posts == null)
            {
                return NotFound();
            }
            vm.SelectedTags = posts.TagPosts.Select(e => e.TagId).AsEnumerable();
            ViewData["CategoryId"] = new SelectList(_context.GetCategoryList(), "CategoryId", "Title", posts.CategoryId);
            ViewData["Tags"] = new MultiSelectList(_context.GetTagList().AsEnumerable(), "TagId", "Title");

            return View(vm);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PostsVm posts, string[] SelectedTags)
        {
            if (id != posts.PostId)
            {
                return NotFound();
            }
            var model = _context.GetPost(id);

           
            if (SelectedTags != null)
            {
                var tags = model.TagPosts;
                var remove_items = _context.GetTagPostList().Where(e => e.PostId == model.PostId);
                foreach (var tag in remove_items)
                {
                    _context.DeleteTagPost(tag);


                }
               
                _context.Commit();
                foreach (var tag in SelectedTags)
                {
                    var tagToAdd = new TagPosts { TagId = Convert.ToInt32(tag), PostId = posts.PostId };
                    model.TagPosts.Add(tagToAdd);
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    DateTime date = DateTime.Now;
                    model.DateCreated = date;
                    _context.UpdatePost(model);
                    _context.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostsExists(posts.PostId))
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
            ViewData["CategoryId"] = new SelectList(_context.GetCategoryList(), "CategoryId", "Title", posts.CategoryId);
            ViewData["Tags"] = new SelectList(_context.GetTagList(), "TagId", "Title");
            return View(posts);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts = _context.GetPost(id);
            if (posts == null)
            {
                return NotFound();
            }

            return View(posts);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var posts = _context.GetPost(id);
            _context.DeletePost(posts);
            _context.Commit();
            return RedirectToAction(nameof(Index));
        }

        private bool PostsExists(int id)
        {
            return _context.PostExists(id);
        }
    }
}
