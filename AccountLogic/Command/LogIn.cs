using AccountLogic.AccountRepository;
using AccountLogic.Command.CommandInterface;
using AccountLogic.RoleOperation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountLogic.Command
{
    public class LogIn : ICommand
    {
        private UserRepository userRepository = new UserRepository();
        private string _login;
        private string _password;
        private UserController userController = new UserController();
        private GuestController guestController = new GuestController();

        public void Execute()
        {
            Console.WriteLine("----- Log In -----");
            Console.Write("Enter the login: ");
            _login = Console.ReadLine();

            Console.Write("Enter the password: ");
            _password = Console.ReadLine();

            foreach (var user in userRepository.FindAll())
            {
                if (_login == user.Login && _password == user.Password)
                {
                    userController.Menu();
                }
                else
                {
                    Console.WriteLine("\nUncorrect login or password.\n");
                    guestController.Menu();
                }
            }
        }
    }
}
