using StoreRepository.Entity.AccountEntity;
using StoreRepository.Entity.AccountEntity.RoleEnum;
using StoreRepository.Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreRepository.Repository
{
    public class UserRepository : IRepository<User>
    {
        private List<User> users;

        public UserRepository()
        {
            users = new List<User>()
            {
                new User { UserRole = Role.User, Login = "arkadii", Password = "123" }
            };
        }

        public List<User> FindAll()
        {
            return users;
        }

        public void AddUser(User user)
        {
            users.Add(user);
        }
    }
}
