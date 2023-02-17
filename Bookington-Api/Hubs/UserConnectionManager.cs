namespace Bookington_Api.Hubs
{
    public class UserConnectionManager : IUserConnectionManager
    {

        private static Dictionary<Guid?, List<string>> userConnectionMap = new Dictionary<Guid?, List<string>>();

        private static string userConnectionMapLocker = string.Empty;

        public List<string> GetUserConnections(Guid userId)
        {
            var conn = new List<string>();
            lock (userConnectionMapLocker)
            {
                conn = userConnectionMap[userId];
            }
            return conn;
        }

        public void KeepUserConnection(Guid? userId, string connectionId)
        {
            lock (userConnectionMapLocker)
            {
                if (!userConnectionMap.ContainsKey(userId))
                {
                    userConnectionMap[userId] = new List<string>();
                }
                userConnectionMap[userId].Add(connectionId);
            }
        }

        public void RemoveUserConnection(string connectionId)
        {
            lock (userConnectionMapLocker)
            {
                foreach (var userId in userConnectionMap.Keys)
                {
                    if (userConnectionMap.ContainsKey(userId))
                    {
                        if (userConnectionMap[userId].Contains(connectionId))
                        {
                            userConnectionMap[userId].Remove(connectionId);
                            break;
                        }
                    }
                }
            }
        }
    }
}
