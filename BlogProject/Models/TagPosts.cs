using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.Models
{
    public partial class TagPosts
    {
        [Key]
        public int TagPostId { get; set; }
        public int PostId { get; set; }
        public int TagId { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual Posts Post { get; set; }
        public virtual Tags Tag { get; set; }
    }
}
