using ASP_MEMBER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_MEMBER.Interface
{
    interface IAccountService
    {

        void Register(RegisterModel model);

        bool Login(LoginModel model);
    }
}
