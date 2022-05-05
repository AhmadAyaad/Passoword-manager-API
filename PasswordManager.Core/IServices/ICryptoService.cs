namespace PasswordManager.Core.IServices
{
    public interface ICryptoService
    {
        string EncryptedPassword { get; }
        void EncryptPassword(string password);
        string DecryptPassword(string encryptedPassword);
    }
}
