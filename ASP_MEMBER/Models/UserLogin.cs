using ASP_MEMBER.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ASP_MEMBER.Models
{
    public class UserLogin : GenericPrincipal
    {
        public Member Member { get; set; }

        public UserLogin(IIdentity identity, RoleAccount roles) 
            : base(identity, new string[] { roles.ToString() })
        {
        }
    }
}