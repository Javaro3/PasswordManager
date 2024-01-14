using Domains.Domains;
using Microsoft.EntityFrameworkCore;
using Servicies.PassowrdHashers;

namespace Repository.Repositories {
    public class PasswordInfoRepository : IRepository<PasswordInfo> {
        private readonly PasswordManagerContext _context;

        public PasswordInfoRepository(PasswordManagerContext context) {
            _context = context;
        }

        public void Add(PasswordInfo entity) {
            entity.PasswordHash = PasswordInfosHasher.Encrypt(entity.Password, entity.User.PublicKey);
            _context.Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<PasswordInfo> GetAll() {
            var passwordInfos = _context.PasswordInfos
                .Include(e => e.User)
                .ToList();
            foreach (var passwordInfo in passwordInfos){
                passwordInfo.Password = PasswordInfosHasher.Decrypt(passwordInfo.PasswordHash, passwordInfo.User.PrivateKey);
            }
            return passwordInfos;
        }

        public PasswordInfo GetById(int id) {
            return GetAll().FirstOrDefault(e => e.Id == id);
        }

        public void Remove(PasswordInfo entity) {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(PasswordInfo entity) {
            entity.PasswordHash = PasswordInfosHasher.Encrypt(entity.Password, entity.User.PublicKey);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<PasswordInfo> GetPasswordInfosByUser(User user) {
            return GetAll().Where(e => e.UserId == user.Id);
        }
    }
}