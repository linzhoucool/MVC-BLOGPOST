using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogPost.Models.ViewModels
{
    public class CreateViewModel
    {
        [Required][AllowHtml]
        public string Body { get; set; }
        
        public string UpdateReason { get; set; }
        public int BlogId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}