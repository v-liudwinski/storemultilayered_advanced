using AccountLogic.AccountLogicInterface;
using BusinessLogic.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountLogic.RoleOperation
{
    public class UserController : IMenu, IPushCommand
    {
        private int _getNum;

        public void Menu()
        {
            Console.WriteLine($"----- User Profile -----");
            Console.WriteLine("1. View the all products;");
            Console.WriteLine("2. Find product by name;");
            Console.WriteLine("3. Create new order;");
            Console.WriteLine("4. View orders history;");
            Console.WriteLine("5. Change personal information;");
            Console.WriteLine("6. Log Out;");

            Console.Write("Put the number of operation: ");
            _getNum = int.Parse(Console.ReadLine());
            PushCommand();
        }

        public void PushCommand()
        {
            switch (_getNum)
            {
                case 1:
                    break;
                case 2:
                    FindProductByName find = new FindProductByName();
                    find.Execute();
                    Menu();
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    GuestController guestController = new GuestController();
                    guestController.Menu();
                    break;
                default:
                    Console.WriteLine("Incorrect number");
                    Menu();
                    break;
            }
        }
    }
}
