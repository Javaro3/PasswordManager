using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Servicies.PassowrdHashers {
    public static class PasswordInfosHasher {
        public static (string PublicKey, string PrivateKey) GetKeys() {
            string publicKey, privateKey;
            using (var rsa = new RSACryptoServiceProvider()) {
                publicKey = rsa.ToXmlString(false);
                privateKey = rsa.ToXmlString(true);
            }
            return (publicKey, privateKey);
        }

        public static string Encrypt(string data, string publicKey) {
            using (var rsa = new RSACryptoServiceProvider()) {
                rsa.FromXmlString(publicKey);
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                byte[] encryptedData = rsa.Encrypt(dataBytes, false);
                return Convert.ToBase64String(encryptedData);
            }
        }

        public static string Decrypt(string encryptedData, string privateKey) {
            using (var rsa = new RSACryptoServiceProvider()) {
                rsa.FromXmlString(privateKey);
                byte[] encryptedBytes = Convert.FromBase64String(encryptedData);
                byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, false);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}
