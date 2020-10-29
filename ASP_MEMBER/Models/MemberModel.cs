﻿using ASP_MEMBER.Entitiy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_MEMBER.Models
{
    public class MemberModel
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string position { get; set; }
        [JsonIgnore]
        public string image_type { get; set; }
        [JsonIgnore]
        public byte[] image_bype { get; set; }
        public string image 
        { 
            get
            {
                if(image_bype !=null && !string.IsNullOrEmpty(image_type))
                {
                    return $"{image_type},{Convert.ToBase64String(image_bype)}";
                }
                return null;
            } 
        }
        public RoleAccount role { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime updated { get; set; }
    }
}