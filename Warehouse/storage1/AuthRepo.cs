namespace Warehouse.storage1
{
    internal interface AuthRepo
    {
        bool checkExistsUser(string username, string password);
    }
}
