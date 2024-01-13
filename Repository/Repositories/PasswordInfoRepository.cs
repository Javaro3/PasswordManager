using Domains.Domains;
using Microsoft.EntityFrameworkCore;
using Servicies;

namespace Repository.Repositories {
    public class PasswordInfoRepository : IRepository<PasswordInfo> {
        private readonly PasswordManagerContext _context;

        public PasswordInfoRepository(PasswordManagerContext context) {
            _context = context;
        }

        public void Add(PasswordInfo entity) {
            entity.PasswordHash = PasswordHasher.EncryptRSA(entity.Password, entity.User.ConfirmCode);
            _context.Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<PasswordInfo> GetAll() {
            var passwordInfos = _context.PasswordInfos
                .Include(e => e.User)
                .ToList();
            foreach (var passwordInfo in passwordInfos){
                passwordInfo.Password = PasswordHasher.DecryptRSA(passwordInfo.PasswordHash, passwordInfo.User.ConfirmCode);
            }
            return passwordInfos;
        }

        public PasswordInfo GetById(int id) {
            var result = _context.PasswordInfos.FirstOrDefault(e => e.Id == id);
            result.Password = PasswordHasher.DecryptRSA(result.PasswordHash, result.User.ConfirmCode);
            return result;
        }

        public void Remove(PasswordInfo entity) {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(PasswordInfo entity) {
            entity.PasswordHash = PasswordHasher.EncryptRSA(entity.Password, entity.User.ConfirmCode);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
