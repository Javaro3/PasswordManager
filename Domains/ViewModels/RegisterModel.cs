using System.ComponentModel.DataAnnotations;

namespace Domains.ViewModels {
    public class RegisterModel {
        [Required(ErrorMessage = "Login is required")]
        [Display(Name = "Login")]
        [MaxLength(32, ErrorMessage = "Max Length is 32")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MaxLength(16, ErrorMessage = "Max Length is 16")]
        [MinLength(8, ErrorMessage = "Max Length is 8")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Repeat Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Repeat Password")]
        [MaxLength(16, ErrorMessage = "Max Length is 16")]
        [MinLength(8, ErrorMessage = "Max Length is 8")]
        public string RepeatPassword { get; set; }

        [Required(ErrorMessage = "Confirm Code is required")]
        [Display(Name = "Confirm Code")]
        [MaxLength(16, ErrorMessage = "Max Length is 16")]
        [MinLength(4, ErrorMessage = "Max Length is 4")]
        public string ConfirmCode { get; set; }
    }
}
