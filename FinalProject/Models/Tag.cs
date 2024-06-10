namespace FinalProject.Models
{
    public class Tag
    {
        public string Id { get; set; }
        public string Name { get; set; }
        
        public ICollection<Item> Items { get; set; }

        public Tag()
        {
            
            Id = Guid.NewGuid().ToString();
            Items = new List<Item>();
        }
    }
}
