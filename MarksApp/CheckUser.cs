using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarksApp
{
    public class CheckUser
    {
        public string login {  get; set; }

        public bool isTeach { get; }

        public string Status => isTeach ? "Teacher" : "User";

        public CheckUser(string login, bool isTeach) 
        {
            login = login.Trim();
            isTeach = isTeach;
        }

    }
}
