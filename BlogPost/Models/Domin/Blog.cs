using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogPost.Models.Domin
{
    public class Blog
    {
        public int Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool Published { get; set; }
        public string UserId { get; set; }
        public string MediaUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
  }
}






//var blog = new Blog();
//blog.Title = "iLOVE Animal so much";
//            blog.Body = "What is this thing.";
//            blog.Published = true;
//            blog.DateCreated = DateTime.Now;
//            blog.DateUpdated = DateTime.Now;
//            blog.MediaUrl = "~/Upload/2.png";
//            //blog.UserId = userModerator.Id;

//            context.Blogs.AddOrUpdate(p => p.Title, blog);
//            context.SaveChanges();