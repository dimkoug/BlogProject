using AutoMapper;
using BlogProject.Models;
using BlogProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Profiles
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            CreateMap<CategoriesVm, Categories>();
            CreateMap<PostsVm, Posts>();
            CreateMap<TagsVm, Tags>();
            CreateMap<TagPostsVm, TagPosts>();
        }
    }
}
