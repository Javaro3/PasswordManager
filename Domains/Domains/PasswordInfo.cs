using System.ComponentModel.DataAnnotations.Schema;

namespace Domains.Domains {
    public class PasswordInfo {
        public int Id { get; set; }
        [NotMapped]
        public virtual string Password { get; set; }
        public string PasswordHash { get; set; }
        public string Login { get; set; }
        public string? Description { get; set; }
        public string ServiceName { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; } 
    }
}
