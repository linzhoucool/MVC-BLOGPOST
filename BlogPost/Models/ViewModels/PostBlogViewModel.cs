using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogPost.Models.ViewModels
{
    public class PostBlogViewModel
    {
      

        [AllowHtml]
        public string Body { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public bool Published { get; set; }
  
        public HttpPostedFileBase Media { get; set; }
        

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

    }
}