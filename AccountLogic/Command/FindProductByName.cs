using AccountLogic.Command.CommandInterface;
using BusinessLogic.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Command
{
    public class FindProductByName : ICommand
    {
        private ProductService productService = new ProductService();
        private string _name;

        public void Execute()
        {
            Console.WriteLine("----- Find Product By Name -----");
            Console.Write("Enter the name of the product: ");
            _name = Console.ReadLine();

            productService.ShowProductByTheName(_name);
        }
    }
}
