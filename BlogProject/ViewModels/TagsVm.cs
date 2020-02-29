using BlogProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.ViewModels
{
    public class TagsVm
    {
        public int TagId { get; set; }
        [Required]
        public string Title { get; set; }

        public virtual ICollection<TagPosts> TagPosts { get; set; }
    }
}
