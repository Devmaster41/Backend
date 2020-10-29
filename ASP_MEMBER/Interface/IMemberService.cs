
using ASP_MEMBER.Entitiy;
using ASP_MEMBER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_MEMBER.Interface
{
    interface IMemberService
    {
        IEnumerable<Member> MemberItem { get; }
        GetMemberModel GetMembers(MemberFilterOptions filters);
        void UpdateProfile(string email, ProfileModel model);
        void ChangePassword(string email, ChangPasswordModel model);
        void CreateMember(CreateMemberModel model);
        void DeleteMember(int id);
        void UpdateMember(int id, UpdateMamberModel model);
    }
}
