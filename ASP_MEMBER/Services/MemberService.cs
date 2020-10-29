using ASP_MEMBER.Areas.HelpPage;
using ASP_MEMBER.Entitiy;
using ASP_MEMBER.Interface;
using ASP_MEMBER.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_MEMBER.Services
{
    public class MemberService : IMemberService 

    {
        private databaseEntities db = new databaseEntities();

        public IEnumerable<Entitiy.Member> MemberItem => this.db.Members.ToList();


        public void ChangePassword(string email, ChangPasswordModel model)
        {
            try
            {
                var memberItem = this.db.Members.SingleOrDefault(item => item.email.Equals(email));
                if (memberItem == null) throw new Exception("Not found member");
                this.db.Members.Attach(memberItem);
                memberItem.password = PasswordHashModel.Hash(model.new_pass);
                memberItem.updated = DateTime.Now;
                this.db.Entry(memberItem).State = System.Data.Entity.EntityState.Modified;
                this.db.SaveChanges();
            }catch(Exception e)
            {
                throw e.GetErrorException();
            }
        }
        public GetMemberModel GetMembers(MemberFilterOptions filters)
        {

            var items = this.MemberItem.Select(m => new GetMember
            {
                id = m.id,
                email = m.email,
                firstname = m.firstname,
                lastname = m.lastname,
                position = m.position,
                role = m.role,
                updated = m.updated

            }).OrderByDescending(m => m.updated);

            var memberItems = new GetMemberModel 
            { 
                items = items
                            .Skip((filters.startPage-1) * filters.limitPage)
                            .Take(filters.limitPage)
                            .ToArray(), 
                totalItems = items.Count() 
            };

            if (!string.IsNullOrEmpty(filters.searchType) && filters.searchType.Equals("updated"))
            {
                var paramItem = HttpContext.Current.Request.Params;
                var fromDate = paramItem.Get("searchText[from]").Replace(" GMT+0700 (Indochina Time)", "");
                var toDate = paramItem.Get("searchText[To]").Replace(" GMT+0700 (Indochina Time)", "");
                filters.searchText = $"{fromDate},{toDate}";
            }

            if (!string.IsNullOrEmpty(filters.searchType) && !string.IsNullOrEmpty(filters.searchText))
            {
                string searchText = filters.searchText;
                string searchType = filters.searchType;
                IEnumerable<GetMember> searchItem = new GetMember[] { };

               switch (searchType)
                {
                    case "updated":
                        var searchTexts = searchText.Split(',');
                        DateTime FromDate = DateTime.Parse(searchTexts[0]);
                        DateTime ToDate = DateTime.Parse(searchTexts[1]);
                        searchItem = from m in items
                                     where FromDate <= m.updated && ToDate >= m.updated
                                     select m;

                        break;

                    case "role":
                        searchItem = from m in items
                                     where Convert.ToInt16(m.GetType()
                                                .GetProperty(filters.searchType)
                                                .GetValue(m)) == Convert.ToInt16(searchText)
                                     select m;
                        break;


                    default:
                        searchItem = from m in items
                                     where m.GetType()
                                                .GetProperty(filters.searchType)
                                                .GetValue(m)
                                                .ToString()
                                                .ToLower()
                                                .Contains(searchText.ToLower())
                                     select m;
                        break;




                }
                memberItems.items = searchItem
                        .Skip((filters.startPage - 1) * filters.limitPage)
                        .Take(filters.limitPage)
                        .ToArray();

                memberItems.totalItems = searchItem.Count();
            }

            return memberItems;
        }

        public void UpdateProfile(string email, ProfileModel model)
        {
            try
            {
                var memberItem = this.db.Members.SingleOrDefault(item => item.email.Equals(email));
                if (memberItem == null) throw new Exception("Not found member");

                this.db.Members.Attach(memberItem);
                memberItem.firstname = model.firstname;
                memberItem.lastname = model.lastname;
                memberItem.position = model.position;
                memberItem.updated = DateTime.Now;

                this.onConverBase64ToImage(memberItem, model.image);
                this.db.Entry(memberItem).State = System.Data.Entity.EntityState.Modified;
                this.db.SaveChanges();
            }catch(Exception e)
            {
                throw e.GetErrorException();
            }
        }
        public void CreateMember(CreateMemberModel model)
        {
            try
            {
                Entitiy.Member memberCeate = new Entitiy.Member
                {
                    email = model.email,
                    password = PasswordHashModel.Hash(model.password),
                    firstname = model.firstname,
                    lastname = model.lastname,
                    position = model.position,
                    role = model.role,
                    created = DateTime.Now,
                    updated = DateTime.Now
                };
                this.onConverBase64ToImage(memberCeate, model.image);
                this.db.Members.Add(memberCeate);
                this.db.SaveChanges();
            }catch(Exception e)
            {
                throw e.GetErrorException();
            }
        }
        private void onConverBase64ToImage(Entitiy.Member memberItem, string image)
        {
     
            if (!string.IsNullOrEmpty(image))
            {
                string[] images = image.Split(',');
                if (images.Length == 2)
                {
                    if (images[0].IndexOf("image") >= 0)
                    {
                        memberItem.image_type = images[0];
                        memberItem.image = Convert.FromBase64String(images[1]);
                    }

                }
            }
            else if (image == null)
            {
                memberItem.image_type = null;
                memberItem.image = null;
            }
        }

        public void DeleteMember(int id)
        {
            try
            {
                var memberDelete = this.MemberItem.SingleOrDefault(m => m.id == id);
                if (memberDelete == null) throw new Exception("Note found member");
                this.db.Members.Remove(memberDelete);
                this.db.SaveChanges();
            }catch(Exception e)
            {
                throw e.GetErrorException();
            }
        }

        public void UpdateMember(int id, UpdateMamberModel model)
        {
            try
            {
                var memberUpdate = this.MemberItem.SingleOrDefault(m => m.id == id);
                if (memberUpdate == null) throw new Exception("Note found member");
                this.db.Members.Attach(memberUpdate);
                memberUpdate.email = model.email;
                memberUpdate.firstname = model.firstname;
                memberUpdate.lastname = model.lastname;
                memberUpdate.position = model.position;
                memberUpdate.updated = DateTime.Now;
                memberUpdate.role = model.role;
                if (!string.IsNullOrEmpty(model.password))
                {
                    memberUpdate.password = PasswordHashModel.Hash(model.password);
                }
                this.onConverBase64ToImage(memberUpdate, model.image);
                this.db.Entry(memberUpdate).State = System.Data.Entity.EntityState.Modified;
                this.db.SaveChanges();
            }catch(Exception e)
            {
                throw e.GetBaseException();
            }
        }
    }  
    
    
}