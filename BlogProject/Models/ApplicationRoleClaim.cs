using Microsoft.AspNetCore.Identity;

namespace BlogProject.Models
{
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public virtual ApplicationRole Role { get; set; }
    }
}