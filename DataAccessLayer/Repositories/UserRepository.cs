using DataAccessLayer.Models;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IRepository<User, string>
    {
        Dictionary<string, User> _users = new Dictionary<string, User>();

        public User Create(User user)
        {
            var id = Guid.NewGuid().ToString();
            user.UserId = id;
            _users[id] = user;
            return _users[id];
        }

        public List<User>? ReadAll()
        {
            if (_users.Count == 0)
                return null;

            List<User> users = _users.Values.ToList();
            users.Sort();
            return users;
        }

        public User? Read(string key)
        {
            if (_users.ContainsKey(key))
                return _users[key];
            return null;
        }

        public User? Update(User user, string key)
        {
            if (_users.ContainsKey(key))
            {
                _users[key] = user;
                return _users[key];
            }
            return null;
        }

        public User? Delete(string key)
        {
            if (_users.ContainsKey(key))
            {
                User item = _users[key];
                _users.Remove(key);
                return item;
            }
            return null;
        }

    }
}
