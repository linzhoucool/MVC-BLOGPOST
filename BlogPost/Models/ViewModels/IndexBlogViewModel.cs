
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogPost.Models.ViewModels
{
    public class IndexBlogViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool Published { get; set; }
        public string Slug { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

    }
}