using Warehouse.Entity;

namespace Warehouse.Storage
{
    internal interface AuthRepo
    {
        bool isUserExistsByUsername(string username);

        bool isUserCredentialsValid(string username, string password);

        string fetchRoleByUsername(string username);

        void createUser(UserEntity userEntity);
    }
}