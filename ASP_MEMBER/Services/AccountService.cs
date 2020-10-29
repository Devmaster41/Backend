using ASP_MEMBER.Entitiy;
using ASP_MEMBER.Interface;
using ASP_MEMBER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_MEMBER.Services
{
    public class AccountService : IAccountService
    {
        private databaseEntities db = new databaseEntities();

        //เข้าสู่ระบบ
        public bool Login(LoginModel model)
        {
            try
            {
                var memberItem = this.db.Members.SingleOrDefault(m => m.email.Equals(model.email));
                if(memberItem != null)
                {
                    return PasswordHashModel.Verify(model.password, memberItem.password);
                }
                return false;
            }catch(Exception e)
            {
                throw e.GetErrorException();
            }
        }

        //ลงทะเบียน
        public void Register(RegisterModel model)
        {
            try
            {
                this.db.Members.Add(new Member 
                { 
                    firstname = model.firstname,
                    lastname = model.lastname,
                    email = model.email,
                    password = model.password,
                    role = RoleAccount.Member,
                    created = DateTime.Now,
                    updated = DateTime.Now
                });
                this.db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e.GetErrorException();
            }
        }
    }
}