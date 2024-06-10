using System.ComponentModel.DataAnnotations;
using FinalProject.Enum;

namespace FinalProject.ViewModels
{
    public class CreateCollectionViewModel
    {
        [Required]
        public string Name { get; set; }

/*        [Required]
        public string ImageUrl { get; set; }*/

        [Required]
        public string Description { get; set; }

        [Required]
        public Category Category { get; set; }

      

        public bool CustomString1State { get; set; }
        public string? CustomString1Name { get; set; }
        public bool CustomString2State { get; set; }
        public string? CustomString2Name { get; set; }
        public bool CustomString3State { get; set; }
        public string? CustomString3Name { get; set; }
    }
    
}
