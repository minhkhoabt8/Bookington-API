namespace Bookington_Api.Hubs
{
    public interface IUserConnectionManager
    {
        void KeepUserConnection(Guid? userId, string connectionId);
        void RemoveUserConnection(string connectionId);
        List<string> GetUserConnections(Guid userId);
    }
}
