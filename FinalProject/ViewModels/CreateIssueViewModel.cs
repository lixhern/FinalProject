using System.ComponentModel.DataAnnotations;

namespace FinalProject.ViewModels
{
    public class CreateIssueViewModel
    {
        [Required]
        public string UserName {  get; set; }
        [Required]
        public string Collection {  get; set; }
        [Required]
        public string PageUrl { get; set; }
        [Required]
        public string Priority { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
