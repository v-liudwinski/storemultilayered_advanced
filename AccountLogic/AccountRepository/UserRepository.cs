using AccountLogic.Role;
using BusinessLogic.Repository;
using BusinessLogic.Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountLogic.AccountRepository
{
    public class UserRepository : IRepository<User>, IRepositoryEntity
    {
        private List<User> users;

        public UserRepository()
        {
            users = new List<User>()
            {
                new User { Login = "lomtik", Password = "123" }
            };
        }

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public List<User> FindAll()
        {
            return users;
        }
    }
}
