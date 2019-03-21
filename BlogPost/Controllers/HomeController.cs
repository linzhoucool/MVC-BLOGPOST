using BlogPost.Models;
using System;
using BlogPost.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogPost.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext DbContext;
        public HomeController()
        {
            DbContext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var model = DbContext.Blogs.Where(p => p.Published == true)
            .Select(p => new IndexHomeViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Body = p.Body,
                MediaUrl= p.MediaUrl,
                DateCreated = p.DateCreated,
                DateUpdated = p.DateUpdated
            }).ToList();          
            return View(model);
        }

        public ActionResult Check(string input)
        {
            var model = DbContext.Blogs.Where(p => p.Published == true && p.Title.Contains(input)|| p.Body.Contains(input))
          .Select(p => new IndexHomeViewModel
          {
              Id = p.Id,
              Title = p.Title,
              Body = p.Body,
              MediaUrl = p.MediaUrl,
              DateCreated = p.DateCreated,
              DateUpdated = p.DateUpdated
          }).ToList();
           return View("Index",model);
        }

    public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}