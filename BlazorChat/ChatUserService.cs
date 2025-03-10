using System.Collections.Concurrent;

public class ChatUserService
{
    private readonly ConcurrentDictionary<string, string> _users = new();

    public void Add(string connectionId, string username)
    {
        _users[username] = connectionId;
    }

    public void RemoveByName(string username)
    {
        _users.TryRemove(username, out _);
    }

    public string GetConnectionIdByName(string username)
    {
        return _users.TryGetValue(username, out var connectionId) ? connectionId : null;
    }

    public IEnumerable<(string ConnectionId, string Username)> GetAll()
    {
        return _users.Select(u => (u.Value, u.Key));
    }
}