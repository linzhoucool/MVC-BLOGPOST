using BlogPost.Models.Domin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogPost.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public string Body { get; set; }
        public string UpdateReason { get; set;} 
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public virtual Blog Blog { get; set; }
        public int BlogId { get; set; }
    }
}