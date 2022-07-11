using AccountLogic.Role;
using AccountLogic.Command.CommandInterface;
using System;
using System.Collections.Generic;
using System.Text;
using AccountLogic.AccountRepository;
using AccountLogic.RoleOperation;

namespace BusinessLogic.Command
{
    public class RegisterUser : ICommand
    {
        private string _login;
        private string _password;
        private string _tryPassword;
        private UserRepository userRepository = new UserRepository();
        private UserController userController = new UserController();
        private GuestController guestController = new GuestController();

        public void Execute()
        {
            Console.WriteLine("----- Registration -----");
            Console.Write("Enter the login: ");
            _login = Console.ReadLine();

            Console.Write("Enter the password: ");
            _password = Console.ReadLine();

            Console.Write("Repeat the password: ");
            _tryPassword = Console.ReadLine();

            if (_password == _tryPassword)
            {
                User user = new User();
                user.role = Role.User;
                user.Login = _login;
                user.Password = _password;
                userRepository.AddUser(user);
                userController.Menu();
            }
            else
            {
                Console.WriteLine("Incorrect login or password.");
                guestController.Menu();
            }
        }
    }
}
