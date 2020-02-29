using Microsoft.AspNetCore.Identity;

namespace BlogProject.Models
{
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {
        public virtual ApplicationUser User { get; set; }
    }
}