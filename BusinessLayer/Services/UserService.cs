using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using BusinessLayer.Interfaces;

namespace BusinessLayer.Services
{
    public class UserService : IUserInteract
    {
        private UserRepository _repo = new UserRepository();

        public void UserMenu()
        {
            while (true)
            {
                Console.WriteLine("\nPlease enter what you wish to do.");
                Console.WriteLine("1. Add User");
                Console.WriteLine("2. Get User");
                Console.WriteLine("3. Get All Users");
                Console.WriteLine("4. Update User");
                Console.WriteLine("5. Delete User");
                Console.WriteLine("6. Go Back");
                int.TryParse(Console.ReadLine(), out int choice);

                switch (choice)
                {
                    case 1: CreateUser(); break;
                    case 2: ReadUser(); break;
                    case 3: ReadAllUsers(); break;
                    case 4: UpdateUser(); break;
                    case 5: DeleteUser(); break;
                    case 6: return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        public void CreateUser()
        {
            User user = new User();

            Console.WriteLine();
            Console.Write("Please enter your name: ");
            user.Name = Console.ReadLine() ?? "";

            Console.Write("\nPlease enter your email: ");
            user.Email = Console.ReadLine() ?? "";

            Console.Write("\nPlease enter your phone number: ");
            user.Phone = Console.ReadLine() ?? "";

            User createdUser = _repo.Create(user);
            Console.WriteLine($"\nUser Created.\nUserId: {createdUser.UserId}\nName: {createdUser.Name}\nEmail: {createdUser.Email}\nPhone: {createdUser.Phone}\n");
        }

        public List<User>? ReadAllUsers()
        {
            List<User>? users = _repo.ReadAll();
            if (users == null)
            {
                Console.WriteLine("No users found.");
                return null;
            }
            foreach (var u in users)
                Console.WriteLine($"\nUserId: {u.UserId}\nName: {u.Name}\nEmail: {u.Email}\nPhone: {u.Phone}\n");
            return users;
        }

        public void ReadUser()
        {
            Console.Write("\nEnter the UserId: ");
            string userId = Console.ReadLine();
            User? user = _repo.Read(userId);

            if (user == null)
            {
                Console.WriteLine("User Not Found");
            }
            else
            {
                Console.WriteLine($"\nUserId: {user.UserId}\nName: {user.Name}\nEmail: {user.Email}\nPhone: {user.Phone}");
            }
        }

        public void UpdateUser()
        {
            User user = new User();

            Console.Write("\nPlease enter the Id of the user you want to modify.");
            user.UserId = Console.ReadLine() ?? "";

            Console.Write("\nPlease enter updated name.");
            user.Name = Console.ReadLine() ?? "";

            Console.Write("\nPlease enter updated email.");
            user.Email = Console.ReadLine() ?? "";

            Console.Write("\nPlease enter updated phone number.");
            user.Phone = Console.ReadLine() ?? "";

            User? updatedUser = _repo.Update(user, user.UserId);
            Console.WriteLine($"\nUser Updated.\nUserId: {updatedUser.UserId}\nName: {updatedUser.Name}\nEmail: {updatedUser.Email}\nPhone: {updatedUser.Phone}\n");
        }

        public void DeleteUser()
        {
            Console.Write("\nPlease Enter the UserId for the user you want to delete.");
            string userId = Console.ReadLine();
            User? deletedUser = _repo.Delete(userId);
            Console.WriteLine($"\nUser Deleted.\nUserId: {deletedUser.UserId}\nName: {deletedUser.Name}\nEmail: {deletedUser.Email}\nPhone: {deletedUser.Phone}\n");
        }
    }
}
