using BlogProject.Data;
using BlogProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.services
{
    public class BlogRepository : IBlogRepository
    {

        private readonly ApplicationDbContext _context;

        public BlogRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddCategory(Categories category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            _context.Categories.Add(category);
        }

        public void AddPost(Posts post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }
            _context.Posts.Add(post);
        }

        public void AddTag(Tags tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }
            _context.Tags.Add(tag);
        }

        public bool CategoryExists(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            return _context.Categories.Any(a => a.CategoryId == Id);
        }

        public bool Commit()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void DeleteCategory(Categories category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _context.Categories.Remove(category);
        }

        public void DeletePost(Posts post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            _context.Posts.Remove(post);
        }

        public void DeleteTag(Tags tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            _context.Tags.Remove(tag);
        }

        public Categories GetCategory(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            return _context.Categories.Include(c => c.Parent).FirstOrDefault(a => a.CategoryId == Id);
        }

        public IEnumerable<Categories> GetCategoryList()
        {
            return _context.Categories.Include(c => c.Parent).ToList();
        }

        public Posts GetPost(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            return _context.Posts.FirstOrDefault(a => a.PostId == Id);
        }

        public IEnumerable<Posts> GetPostList()
        {
            return _context.Posts.ToList();
        }

        public Tags GetTag(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            return _context.Tags.FirstOrDefault(a => a.TagId == Id);
        }

        public IEnumerable<Tags> GetTagList()
        {
            return _context.Tags.ToList();
        }

        public bool PostExists(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            return _context.Posts.Any(a => a.PostId == Id);
        }

        public bool TagExists(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            return _context.Tags.Any(a => a.TagId == Id);
        }

        public void UpdateCategory(Categories category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            _context.Categories.Update(category);
        }

        public void UpdatePost(Posts post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }
            _context.Posts.Update(post);
        }

        public void UpdateTag(Tags tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }
            _context.Tags.Update(tag);
        }
    }
}
