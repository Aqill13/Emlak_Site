using System.ComponentModel.DataAnnotations;

namespace EmlakUI.Areas.User.Models
{
    public class ConfirmMail
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter Code")]
        public int Code { get; set; }
    }
}
