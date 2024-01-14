using System.ComponentModel.DataAnnotations;

namespace Domains.ViewModels {
    public class PasswordInfoModel {
        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Login is required")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Service Name is required")]
        [Display(Name = "Service Name")]
        public string ServiceName { get; set; }
    }
}
