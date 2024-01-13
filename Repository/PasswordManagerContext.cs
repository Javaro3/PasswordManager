using Domains.Domains;
using Microsoft.EntityFrameworkCore;

namespace Repository {
    public class PasswordManagerContext : DbContext {
        public virtual DbSet<User> Users { get ; set; }
        public virtual DbSet<PasswordInfo> PasswordInfos { get; set; }

        public PasswordManagerContext() { }
        public PasswordManagerContext(DbContextOptions<PasswordManagerContext> options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseNpgsql("Host=ec2-34-242-199-141.eu-west-1.compute.amazonaws.com;Port=5432;Database=d6bbhffsd1gljo;Username=ikjujfniipccgq;Password=c317b6a7c2640bd4c7a548a05dbdc2b475370d0022ae7ad84d3d0f3b7db6d00f;");

    }
}
