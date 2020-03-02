using BlogProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Data.services
{
    public interface IBlogRepository
    {
        List<Posts> GetPostList();
        Posts GetPost(int? Id);

        void AddPost(Posts post);

        void UpdatePost(Posts post);
        void DeletePost(Posts post);

        bool PostExists(int? Id);


        List<Categories> GetCategoryList();
        Categories GetCategory(int? Id);

        void AddCategory(Categories category);

        void UpdateCategory(Categories category);
        void DeleteCategory(Categories category);

        bool CategoryExists(int? Id);


        List<Tags> GetTagList();
        Tags GetTag(int? Id);

        void AddTag(Tags tag);

        void UpdateTag(Tags tag);
        void DeleteTag(Tags tag);

        bool TagExists(int? Id);

        bool Commit();

        List<TagPosts> GetTagPostList();
        void DeleteTagPost(TagPosts tag);
    }
}
