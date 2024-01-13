using Domains.Domains;
using Microsoft.EntityFrameworkCore;
using Servicies;
using System.Linq;

namespace Repository.Repositories {
    public class UserRepository : IRepository<User> {
        private readonly PasswordManagerContext _context;
        private readonly PasswordInfoRepository _passwordInfoRepository;

        public UserRepository(PasswordManagerContext context, PasswordInfoRepository passwordInfoRepository = null) {
            _context = context;
            _passwordInfoRepository = passwordInfoRepository;
        }

        public void Add(User entity) {
            entity.PasswordHash = PasswordHasher.HashPasswordSHA256(entity.Password);
            _context.Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll() {
            return _context.Users.ToList();
        }

        public bool CheckPassword(User user, string password) {
            return user.PasswordHash == PasswordHasher.HashPasswordSHA256(password);
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
            entity.PasswordHash = PasswordHasher.HashPasswordSHA256(entity.Password);
            var passwordInfos = GetAllPasswordInfos(entity);
            foreach (var passwordInfo in passwordInfos){
                _passwordInfoRepository.Update(passwordInfo);
            }
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<PasswordInfo> GetAllPasswordInfos(User user) {
            return _context.PasswordInfos
                .Include(e => e.User)
                .Where(e => e.UserId == user.Id)
                .ToList();
        }
    }
}
