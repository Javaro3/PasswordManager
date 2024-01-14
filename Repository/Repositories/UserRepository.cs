using Domains.Domains;
using Microsoft.EntityFrameworkCore;
using Servicies.PassowrdHashers;

namespace Repository.Repositories
{
    public class UserRepository : IRepository<User> {
        private readonly PasswordManagerContext _context;

        public UserRepository(PasswordManagerContext context) {
            _context = context;
        }

        public void Add(User entity) {
            entity.PasswordHash = UserPasswordHasher.HashPasswordSHA256(entity.Password);
            var keys = PasswordInfosHasher.GetKeys();
            entity.PublicKey = keys.PublicKey;
            entity.PrivateKey = keys.PrivateKey;
            _context.Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll() {
            return _context.Users.ToList();
        }

        public bool CheckPassword(User user, string password) {
            return user.PasswordHash == UserPasswordHasher.HashPasswordSHA256(password);
        }

        public User GetById(int id) {
            return _context.Users.FirstOrDefault(e => e.Id == id);
        }

        public User GetByLogin(string login) {
            return _context.Users.FirstOrDefault(e => e.Login == login);
        }

        public bool IsLoginExist(string login) {
            return GetAll().Select(e => e.Login).Contains(login);
        }

        public void Remove(User entity) {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(User entity) {
            entity.PasswordHash = UserPasswordHasher.HashPasswordSHA256(entity.Password);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
