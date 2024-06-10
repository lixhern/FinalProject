using Microsoft.AspNetCore.Identity;

namespace FinalProject.Models
{
    public class AdminPanelModel
    {
        public List<User> Users { get; set; }
        public UserManager<User> UserManager { get; set; }
    }
}
