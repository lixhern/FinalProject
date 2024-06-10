namespace FinalProject.Models
{
    public class UserViewModel
    {
        public User user {  get; set; }
        public List<Collection> Collections { get; set; } = new List<Collection>();
        public List<Item> Items { get; set; } = new List<Item>();
    }
}
