using StoreRepository.Entity.CommercialEntity;
using StoreRepository.Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreRepository.Repository
{
    public class OrderRepository : IRepository<Order>
    {
        private List<Order> orders;

        public List<Order> FindAll()
        {
            return orders;
        }
    }
}
