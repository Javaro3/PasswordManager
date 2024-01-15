using Domains.Domains;
using Microsoft.EntityFrameworkCore;

namespace Repository {
    public class PasswordManagerContext : DbContext {
        public virtual DbSet<User> Users { get ; set; }
        public virtual DbSet<PasswordInfo> PasswordInfos { get; set; }

        public PasswordManagerContext() { }
        public PasswordManagerContext(DbContextOptions<PasswordManagerContext> options) : base(options) { }
    }
}
