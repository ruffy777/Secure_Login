using System.Security.Cryptography;
using System.Text;

namespace seclogin.Helpers
{

    public class HashHelper
    {
        public string PasswordHash(string password, string salt)
        {
            const int keySize = 64;
            const int iterations = 350000;

            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            var saltbytes = Encoding.UTF8.GetBytes(salt);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                saltbytes,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);

        }
    }
}
