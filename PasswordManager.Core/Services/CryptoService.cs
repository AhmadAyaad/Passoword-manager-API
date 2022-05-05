using Microsoft.Extensions.Configuration;
using PasswordManager.Core.IServices;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManager.Core.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly IConfiguration configuration;
        private readonly string _key;
        private byte[] initializationVector = new byte[16];
        private byte[] buffer;
        private string _encryptedPassword;
        public CryptoService(IConfiguration configuration)
        {
            this.configuration = configuration;
            _key = this.configuration.GetSection("key").Value;
        }
        public string EncryptedPassword => _encryptedPassword;

        public void EncryptPassword(string password)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_key);
                aes.IV = initializationVector;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(password);
                        }
                        buffer = memoryStream.ToArray();
                    }
                }
            }
            _encryptedPassword = Convert.ToBase64String(buffer);
        }
        public string DecryptPassword(string encryptedPassword)
        {
            byte[] buffer = Convert.FromBase64String(encryptedPassword);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_key);
                aes.IV = initializationVector;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
