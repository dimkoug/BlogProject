using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.Models
{
    public partial class Tags
    {
        public Tags()
        {
            TagPosts = new HashSet<TagPosts>();
        }

        [Key]
        public int TagId { get; set; }
        public string Title { get; set; }

        public virtual ICollection<TagPosts> TagPosts { get; set; }
    }
}
