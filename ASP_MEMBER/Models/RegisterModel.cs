using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace ASP_MEMBER.Models
{
    public class RegisterModel
    {
        [Required]
        public string firstname { get; set; }

        [Required]
        public string lastname { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        [Compare("password",ErrorMessage ="รหัสผ่านไม่ตรงกัน")]
        public string cpassword { get; set; }

       
    }
}