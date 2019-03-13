﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogPost.Models.ViewModels
{
    public class DetailBlogViewModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public bool Published { get; set; }
        public string MediaUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

    }
}