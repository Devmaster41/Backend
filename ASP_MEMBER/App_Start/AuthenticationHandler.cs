using ASP_MEMBER.Interface;
using ASP_MEMBER.Models;
using ASP_MEMBER.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASP_MEMBER
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private IAccessTokenService accessTokenService;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var Authorization = request.Headers.Authorization;
            if(Authorization != null)
            {
                string AccessToken = Authorization.Parameter;
                string AccessTokenType = Authorization.Scheme;
                if (AccessTokenType.Equals("Bearer"))
                {
                    this.accessTokenService = new DBAccessTokenService();
                    var memberItem = this.accessTokenService.VerifyAccessToken(AccessToken);
                    if(memberItem != null)
                    {
                        var userLogin = new UserLogin(new GenericIdentity(memberItem.email), memberItem.role);
                        userLogin.Member = memberItem;
                        Thread.CurrentPrincipal = userLogin;
                        HttpContext.Current.User = userLogin;
                    }

                }
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}