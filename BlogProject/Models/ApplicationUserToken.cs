﻿using Microsoft.AspNetCore.Identity;

namespace BlogProject.Models
{
    public class ApplicationUserToken : IdentityUserToken<string>
    {
        public virtual ApplicationUser User { get; set; }
    }
}