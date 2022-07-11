using StoreRepository.Entity.AccountEntity.RoleEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreRepository.Entity.AccountEntity
{
    public class Guest : IGuest
    {
        public Role GuestRole { get; set; }
    }
}
