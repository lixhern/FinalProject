namespace FinalProject.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int ItemId {  get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public Item Item { get; set; }
    }
}
