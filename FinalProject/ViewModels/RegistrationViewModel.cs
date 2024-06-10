using System.ComponentModel.DataAnnotations;


namespace FinalProject.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        //[Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        //[Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        //[Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
