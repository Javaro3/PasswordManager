using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domains.ViewModels {
    public class PasswordGeneratorModel {
        [Display(Name = "Password")]
        [JsonIgnore]
        public string Password { get; set; }
        
        [Display(Name = "Password Length")]
        [Range(minimum:1, maximum:64)]
        public int Length { get; set; }
        
        [Display(Name = "Use digits (123)")]
        public bool UseDigits { get; set; }
        
        [Display(Name = "Use lowercase (abc)")]
        public bool UseLowercase { get; set; }
        
        [Display(Name = "Use uppercase (ABC)")]
        public bool UseUppercase { get; set; }
        
        [Display(Name = "Use symbols (!@#)")]
        public bool UseSymbols { set; get; }
        
        [Display(Name = "Use unique")]
        public bool UseUnique { get; set; }
        
        [Display(Name = "Don't use exclude similar (iI1loO0)")]
        public bool DontUseExcludeSimilar { get; set; }
    }
}