using StoreRepository.Entity.AccountEntity.RoleEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreRepository.Entity.AccountEntity
{
    public class Administrator : IAdministrator
    {
        public Role AdminRole { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
