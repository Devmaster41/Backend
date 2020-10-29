using ASP_MEMBER.Interface;
using ASP_MEMBER.Models;
using ASP_MEMBER.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ASP_MEMBER.Controllers
{
    public class AccountController : ApiController
    {
        private AccountService account;
        private IAccessTokenService AccessToken;

        protected AccountController()
        {
            this.account = new AccountService();
            this.AccessToken = new DBAccessTokenService();
        }

        //การลงทะเบียน
        [Route("api/account/register")]
        public IHttpActionResult PostRegister([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.password = PasswordHashModel.Hash(model.password);
                    this.account.Register(model);
                    return Ok("Successful");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Exception", e.Message);
                }
            }

            return BadRequest(ModelState.GetErrorModelState());
        }

        //เข้าสู่ระบบ
        [Route("api/account/login")]
        public AccessTokenModel PostLogin([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (this.account.Login(model))
                    {
                        return new AccessTokenModel
                        {
                            accessToken = this.AccessToken.GenerateAccessToken(model.email)
                        };
                    }
                    throw new Exception("Usename or Password is Invalid");

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Exception", e.Message);
                }
            }
            throw new HttpResponseException(Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    new { Message = ModelState.GetErrorModelState() }
                ));

        }
    }
}
