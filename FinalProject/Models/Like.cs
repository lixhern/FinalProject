using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Like
    {
        public int Id { get; set; }

        public int ItemId { get; set; }
        public string UserId { get; set; }
        public Item Item { get; set; }

    }
}
