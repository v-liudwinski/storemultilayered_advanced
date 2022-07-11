using AccountLogic.AccountLogicInterface;
using AccountLogic.AccountRepository;
using AccountLogic.Command;
using BusinessLogic.Command;
using BusinessLogic.Repository;
using BusinessLogic.Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountLogic.RoleOperation
{
    public class GuestController : IMenu, IPushCommand, IGetRepository
    {
        private int _getNum;
        public ProductRepository productRepository { get; set; }
        public UserRepository userRepository { get; set; }

        public void GetRepository(Dictionary<string, IRepositoryEntity> repositories)
        {
            productRepository = (ProductRepository)repositories["productRepository"];
            userRepository = (UserRepository)repositories["userRepository"];
        }

        public void Menu()
        {
            Console.WriteLine("----- Guest Menu -----");
            Console.WriteLine("1. Find product by name;");
            Console.WriteLine("2. Sign Up;");
            Console.WriteLine("3. Log In;");

            Console.Write("Put the number of operation: ");
            _getNum = int.Parse(Console.ReadLine());
            PushCommand();
        }

        public void PushCommand()
        {
            switch (_getNum)
            {
                case 1:
                    FindProductByName find = new FindProductByName();
                    find.Execute();
                    Menu();
                    break;
                case 2:
                    RegisterUser registerUser = new RegisterUser();
                    registerUser.Execute();
                    break;
                case 3:
                    LogIn logIn = new LogIn();
                    logIn.Execute();
                    break;
                default:
                    Console.WriteLine("Incorrect number");
                    Menu();
                    break;
            }
        }

        

    }
}
