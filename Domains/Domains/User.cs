using System.ComponentModel.DataAnnotations.Schema;

namespace Domains.Domains {
    public class User {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        [NotMapped]
        public string Password { get; set; }
        public string? ConfirmCode { get; set; }
    }
}
