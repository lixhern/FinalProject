namespace FinalProject.Models
{
    public class HomePanelModel
    {
        public List<TagModel> Tags { get; set; } = new List<TagModel>();
        public List<Collection> Collections { get; set; } = new List<Collection>();
        public List<Item> Items { get; set; } = new List<Item>();
    }
    
}
