namespace FinalProject.Models
{
    public class ItemModel
    {
        public Item Item {  get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public int LikesCount { get; set; }

    }
}
