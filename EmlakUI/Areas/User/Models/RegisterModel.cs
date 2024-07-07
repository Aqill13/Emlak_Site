using System.ComponentModel.DataAnnotations;

namespace EmlakUI.Areas.User.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please enter your Firstname")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Please enter your Lastname")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Please enter your Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
