namespace PasswordManager.Common.DTOS
{
    public class UserLoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class UserLoginDTOForUpdate : UserLoginDTO
    {
        public string OldUsername { get; set; }
    }
}
