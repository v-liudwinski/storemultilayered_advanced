using System;
using System.Collections.Generic;
using System.Text;

namespace AccountLogic.Role
{
    public class Guest
    {
        public Role role { get; set; }

        public Guest()
        {
            role = Role.Guest;
        }
    }
}
