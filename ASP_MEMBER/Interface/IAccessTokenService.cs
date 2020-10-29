using ASP_MEMBER.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_MEMBER.Interface
{
    interface IAccessTokenService
    {

        string GenerateAccessToken(string email, int minute = 50);
        Member VerifyAccessToken(string accessToken);
    }
}
