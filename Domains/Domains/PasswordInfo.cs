using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domains.Domains {
    public class PasswordInfo {
        [JsonIgnore]
        public int Id { get; set; }
        [NotMapped]
        public virtual string Password { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public string Login { get; set; }
        public string? Description { get; set; }
        public string ServiceName { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; } 
    }
}
