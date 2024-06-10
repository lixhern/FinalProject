namespace FinalProject.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int CollectionId { get; set; }
        public string UserId { get; set; }
        public Collection Collection { get; set; }
        
        public User User { get; set; }

        public ICollection<Tag> Tags { get; set; } 
        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public string? CustomString1 { get; set; }
        public string? CustomString2 { get; set; }
        public string? CustomString3 { get; set; }

        public Item()
        {
            Tags = new List<Tag>();
            Likes = new List<Like>();
            Comments = new List<Comment>();
        }
    }

    
}
