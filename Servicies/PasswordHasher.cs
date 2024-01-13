using System.Security.Cryptography;
using System.Text;

namespace Servicies {
    public static class PasswordHasher {
        public static string HashPasswordSHA256(string password) {
            using (var sha256 = new SHA256Managed()) {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public static string EncryptRSA(string data, string confirmCode) {
            using (var rsa = new RSACryptoServiceProvider()) {
                rsa.FromXmlString(confirmCode);
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                byte[] encryptedData = rsa.Encrypt(dataBytes, true);
                return Convert.ToBase64String(encryptedData);
            }
        }

        public static string DecryptRSA(string encryptedData, string confirmCode) {
            using (var rsa = new RSACryptoServiceProvider()) {
                rsa.FromXmlString(confirmCode);
                byte[] encryptedBytes = Convert.FromBase64String(encryptedData);
                byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, true);
                return Encoding.UTF8.GetString(decryptedBytes);
            }

        }
    }
}
