using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.Models
{
    public partial class Posts
    {
        public Posts()
        {
            TagPosts = new HashSet<TagPosts>();
        }

        [Key]
        public int PostId { get; set; }
        public int CategoryId { get; set; }
        public bool Published { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Categories Category { get; set; }
        public virtual ICollection<TagPosts> TagPosts { get; set; }
    }
}
