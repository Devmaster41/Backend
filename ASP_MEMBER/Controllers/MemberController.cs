using ASP_MEMBER.Entitiy;
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
    [Authorize]
    public class MemberController : ApiController
    {
        private IMemberService memberService;
        public MemberController()
        {
            this.memberService = new MemberService();
        }

        [Route("api/member/data")]
        public MemberModel GetMemberLogin()
        {
            var userLogin = this.memberService
                .MemberItem
                .SingleOrDefault(item => item.email.Equals(User.Identity.Name));
            if (userLogin == null) return null;
            return new MemberModel
            {
                id = userLogin.id,
                created = userLogin.created,
                email = userLogin.email,
                firstname = userLogin.firstname,
                image_type = userLogin.image_type,
                image_bype = userLogin.image,
                lastname = userLogin.lastname,
                position = userLogin.position,
                role = userLogin.role,
                updated = userLogin.updated
            };
        }

        [Route("api/member/profile")]
        public IHttpActionResult PostUpdateProfile([FromBody] ProfileModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this.memberService.UpdateProfile(User.Identity.Name, model);
                    return Ok(this.GetMemberLogin());
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Exception", e.Message);
                }
            }
            return BadRequest(ModelState.GetErrorModelState());
        }

        [Route("api/member/change-password")]
        public IHttpActionResult PostChangePassword([FromBody] ChangPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this.memberService.ChangePassword(User.Identity.Name, model);
                    return Ok(new { Message = "Password has chaged" });
                }catch(Exception e)
                {
                    ModelState.AddModelError("Exception", e.Message);
                }
            }
            return BadRequest(ModelState.GetErrorModelState());
        }

        [Authorize(Roles ="Admin")]
        public MemberModel GetMember(int id)
        {
            return this.memberService.MemberItem
                .Select(m => new MemberModel
                {
                    id = m.id,
                    created = m.created,
                    email = m.email,
                    firstname = m.firstname,
                    image_type = m.image_type,
                    image_bype = m.image,
                    lastname = m.lastname,
                    position = m.position,
                    role = m.role,
                    updated = m.updated
                })
                .SingleOrDefault(m => m.id == id);
        }

        [Authorize(Roles ="Employee,Admin")]
        public GetMemberModel GetMembers([FromUri] MemberFilterOptions filters)
        {
            
            if (ModelState.IsValid)
            {
                return this.memberService.GetMembers(filters);
            }

            throw new HttpResponseException(Request.CreateResponse(
                HttpStatusCode.BadRequest,
                new {Message = ModelState.GetErrorModelState()}
             ));
            
        }

        [Authorize(Roles ="Admin")]
        public IHttpActionResult PostCreateMember([FromBody] CreateMemberModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.memberService.CreateMember(model);
                    return Ok("Create successful");
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError("Exception", e.Message);
            }

            return BadRequest(ModelState.GetErrorModelState());
        }

        [Authorize(Roles ="Employee,Admin")]
        public IHttpActionResult DeleteMember(int id)
        {
            try
            {
                this.memberService.DeleteMember(id);
                return Ok("Delete successful");
            }catch(Exception e)
            {
                ModelState.AddModelError("Exception", e.Message);
            }
            return BadRequest(ModelState.GetErrorModelState());
        }

        [Authorize(Roles ="Admin")]
        public IHttpActionResult PutUpdateMember(int id, [FromBody] UpdateMamberModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this.memberService.UpdateMember(id, model);
                    return Ok("Update successful");
                }catch(Exception e)
                {
                    ModelState.AddModelError("Exception", e.Message);
                }
            }
            return BadRequest(ModelState.GetErrorModelState());
        }
    }
}
