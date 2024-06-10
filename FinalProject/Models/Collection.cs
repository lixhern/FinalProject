using FinalProject.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class Collection
    {
        
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }

        //public string ImageUrl { get; set; }
        public string Description { get; set; }

        public Category Category { get; set; }

        public User User { get; set; }

        public ICollection<Item> Items { get; set; }
        public bool CustomString1State { get; set; }
        public string? CustomString1Name { get; set; }
        public bool CustomString2State { get; set; }
        public string? CustomString2Name { get; set; }
        public bool CustomString3State { get; set; }
        public string? CustomString3Name { get; set; }
    }
}
