using Microsoft.AspNetCore.Identity;

namespace BlogProject.Models
{
    public class ApplicationUserLogin : IdentityUserLogin<string>
    {
        public virtual ApplicationUser User { get; set; }
    }
}