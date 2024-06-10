using Microsoft.AspNetCore.Identity;

namespace FinalProject.Models
{
    public class User : IdentityUser
    {
        public bool IsBlock { get; set; }

    }
}
