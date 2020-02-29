using BlogProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.ViewModels
{
    public class CategoriesVm
    {

        public int CategoryId { get; set; }
        public int? ParentId { get; set; }
        [Required]
        public string Title { get; set; }


    }
}
