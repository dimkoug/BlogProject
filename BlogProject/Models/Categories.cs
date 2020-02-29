using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.Models
{
    public partial class Categories
    {
        public Categories()
        {
            InverseParent = new HashSet<Categories>();
            Posts = new HashSet<Posts>();
        }

        [Key]
        public int CategoryId { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; }

        public virtual Categories Parent { get; set; }
        public virtual ICollection<Categories> InverseParent { get; set; }
        public virtual ICollection<Posts> Posts { get; set; }
    }
}
