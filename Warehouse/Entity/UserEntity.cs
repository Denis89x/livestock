namespace Warehouse.Entity
{
    internal class UserEntity
    {
        public long userId { get; set; }

        public string username {  get; set; }

        public string password { get; set; }

        public string repeatedPassword {  get; set; }

        public string role { get; set; }

        public UserEntity(long userId, string username, string password, string repeatedPassword, string role)
        {
            this.userId = userId;
            this.username = username;
            this.password = password;
            this.repeatedPassword = repeatedPassword;
            this.role = role;
        }

        public UserEntity(string username, string password, string repeatedPassword, string role)
        {
            this.username = username;
            this.password = password;
            this.repeatedPassword = repeatedPassword;
            this.role = role;
        }
    }
}
