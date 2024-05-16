using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal interface AuthRepo
    {
        bool isUserExistsByUsername(string username);

        bool isUserCredentialsValid(string username, string password);

        string fetchRoleByUsername(string username);

        void createUser(UserEntity userEntity);
    }
}