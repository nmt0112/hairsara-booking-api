using System.ComponentModel.DataAnnotations;

namespace Storage.Models.Authentication.SignUp
{
    public class RegisterUser
    {
        [Required(ErrorMessage ="User Name is required")]
        public string? Username { get; set; }
        
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")] 
        public string? Email { get; set; }

        [Required(ErrorMessage = "PassWord is required")]
        public string? Password { get; set; }
        
    }
}
