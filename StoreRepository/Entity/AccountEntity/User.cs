using StoreRepository.Entity.AccountEntity.RoleEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreRepository.Entity.AccountEntity
{
    public class User : IUser
    {
        public Role UserRole { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
