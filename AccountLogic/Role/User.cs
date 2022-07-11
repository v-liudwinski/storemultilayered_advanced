using System;
using System.Collections.Generic;
using System.Text;

namespace AccountLogic.Role
{
    public class User
    {
        public Role role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
