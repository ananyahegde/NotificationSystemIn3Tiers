using DataAccessLayer.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Database;

namespace DataAccessLayer.Repositories
{

    public class UserRepository : IRepository<User, string>
    {
        Context context;

        public UserRepository()
        {
            context = new Context();
        }

        public User Create(User user)
        {
            var id = Guid.NewGuid().ToString();
            user.UserId = id;

            context.Add(user);
            context.SaveChanges();

            return user;
        }

        public List<User>? ReadAll()
        {
            return context.Set<User>().ToList();
        }

        public User? Read(string key)
        {
            User? user = context.users.Find(key);
            return user;
        }

        public User? Update(User user, string key)
        {
            var getUser = Read(key);
            if (getUser == null)
                throw new Exception("No users found.");

            getUser.Name = user.Name;
            getUser.Email = user.Email;
            getUser.Phone = user.Phone;

            context.SaveChanges();
            return user;
        }

        public User? Delete(string key)
        {
            var getUser = Read(key);

            if (getUser == null)
                throw new Exception("No users found.");
            context.Remove(getUser);

            context.SaveChanges();
            return getUser;
        }
    }
}
