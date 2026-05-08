using DataAccessLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IUserInteract
    {
        public void CreateUser();
        public List<User>? ReadAllUsers();
        public void ReadUser();
        public void UpdateUser();
        public void DeleteUser();
    }
}
