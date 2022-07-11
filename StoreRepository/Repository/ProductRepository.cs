using StoreRepository.Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreRepository.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private List<Product> products;

        public ProductRepository()
        {
            products = new List<Product>()
            {
                new Product { Name = "Coke", Category = "Beverage", Cost = 5.99m, Description = "Sweet drink" },
                new Product { Name = "Pork", Category = "Meat", Cost = 15.50m, Description = "BBQ meat" },
                new Product { Name = "White Bread", Category = "Bakery", Cost = 3.40m, Description = "Sliced loaf of white bread" }
            };
        }

        public List<Product> FindAll()
        {
            return products;
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }
    }
}
