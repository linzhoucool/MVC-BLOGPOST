using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogPost.Models.ViewModels
{
    public class DetailCommentViewModel
    {
   
        public string Body { get; set; }
        public string UpdateReason { get; set; }
        public string MediaUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}