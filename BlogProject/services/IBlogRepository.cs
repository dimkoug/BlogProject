using BlogProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.services
{
    public interface IBlogRepository
    {
        IEnumerable<Posts> GetPostList();
        Posts GetPost(int? Id);

        void AddPost(Posts post);

        void UpdatePost(Posts post);
        void DeletePost(Posts post);

        bool PostExists(int? Id);


        IEnumerable<Categories> GetCategoryList();
        Categories GetCategory(int? Id);

        void AddCategory(Categories category);

        void UpdateCategory(Categories category);
        void DeleteCategory(Categories category);

        bool CategoryExists(int? Id);


        IEnumerable<Tags> GetTagList();
        Tags GetTag(int? Id);

        void AddTag(Tags tag);

        void UpdateTag(Tags tag);
        void DeleteTag(Tags tag);

        bool TagExists(int? Id);

        bool Commit();
    }
}
