using BlogProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.ViewModels
{
    public class PostsVm
    {
        public int PostId { get; set; }
        public int CategoryId { get; set; }
        public bool Published { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual Categories Category { get; set; }
        public virtual List<TagPosts> TagPosts { get; set; }

        public IEnumerable<int> SelectedTags { get; set; }
    }
}
