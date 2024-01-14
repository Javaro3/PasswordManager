using System.Security.Cryptography;
using System.Text;

namespace Servicies.PassowrdHashers {
    public static class UserPasswordHasher {
        public static string HashPasswordSHA256(string password) {
            using (var sha256 = new SHA256Managed()) {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
