using ASP_MEMBER.Entitiy;
using ASP_MEMBER.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP_MEMBER.Models;
using System.Data;

namespace ASP_MEMBER.Services
{
    public class DBAccessTokenService : IAccessTokenService
    {

        private databaseEntities db = new databaseEntities();

        public string GenerateAccessToken(string email, int minute = 60)
        {
            try
            {
                var memberItem = this.db.Members.SingleOrDefault(m => m.email.Equals(email));
                if (memberItem == null) throw new Exception("Not found member");
                var accessTokenCreate = new AccessToken
                {
                    token = Guid.NewGuid().ToString(),
                    exprise = DateTime.Now.AddMinutes(minute),
                    memberID = memberItem.id

                };
                this.db.AccessTokens.Add(accessTokenCreate);
                this.db.SaveChanges();
                return accessTokenCreate.token;

            }catch(Exception e)
            {
                throw e.GetErrorException();
            }
        }

        public Member VerifyAccessToken(string accessToken)
        {
            try
            {
                var accessTokenItem = this.db.AccessTokens.SingleOrDefault(item => item.token.Equals(accessToken));
                if (accessTokenItem == null) return null;
                if (accessTokenItem.exprise < DateTime.Now) return null;
                return accessTokenItem.Member;
            }
            catch
            {
                return null;
            }
        }
    }
}