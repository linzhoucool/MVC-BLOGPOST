using BlogPost.Models;
using BlogPost.Models.Domin;
using BlogPost.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogPost.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        private ApplicationDbContext DbContext;

        public BlogController()
            {
                DbContext = new ApplicationDbContext();
            }
            public ActionResult Index()
            {
                var userId = User.Identity.GetUserId();
                var model = DbContext.Blogs.Where(p => p.UserId == userId)
                .Select(p => new IndexBlogViewModel
                    {
                       Id = p.Id,
                       Title = p.Title,
                       Body= p.Body,
                       Published = p.Published,
                       UserId = p.UserId,
                       DateCreated= p.DateCreated,
                       DateUpdated= p.DateUpdated
                }).ToList();
              return View(model);
            }

        [HttpGet]
        public ActionResult PostBlog()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult PostBlog(PostBlogViewModel formData)
        {
            return SaveBlog(null, formData);
        }
        private ActionResult SaveBlog(int? id,PostBlogViewModel formData)

        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var userId = User.Identity.GetUserId();
            if (DbContext.Blogs.Any(p => p.UserId == userId &&
            p.Title == formData.Title &&
            (!id.HasValue || p.Id != id.Value)))
            {
                ModelState.AddModelError(nameof(PostBlogViewModel.Title),
                "Blog should be unique");
                return View();
            }

            string fileExtension;
            //Validating file upload
            if (formData.Media != null)

            {
                fileExtension = Path.GetExtension(formData.Media.FileName);
                if (!Constants.AllowedFileExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("", "File extension is not allowed.");
                    return View();
                }
            }
            Blog blog;

            if (!id.HasValue)
            {
                blog = new Blog();
                blog.UserId = userId;
                DbContext.Blogs.Add(blog);
            }
            else
            {
                blog = DbContext.Blogs.FirstOrDefault(p => p.Id == id && p.UserId == userId);
                if (blog == null)
                {
                    return RedirectToAction(nameof(BlogController.Index));
                }
            } 
            blog.Title = formData.Title;
            blog.Body = formData.Body;
            blog.Published = formData.Published;
            blog.DateCreated = DateTime.Now;
            blog.DateUpdated = DateTime.Now;
            //Handling file upload

            if (formData.Media != null)
            {
                if (!Directory.Exists(Constants.MappedUploadFolder))
                {

                    Directory.CreateDirectory(Constants.MappedUploadFolder);
                }
                var fileName = formData.Media.FileName;
                //var fullPathWithName = formData.Media.FileName;
                var fullPathWithName = Constants.MappedUploadFolder + fileName;
                formData.Media.SaveAs(fullPathWithName);
                blog.MediaUrl = Constants.UploadFolder + fileName;
            }
            DbContext.SaveChanges();
            return RedirectToAction(nameof(BlogController.Index));
        }




        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(BlogController.Index));
            }
            var userId = User.Identity.GetUserId();
            var blog = DbContext.Blogs.FirstOrDefault(
                p => p.Id == id/* && p.UserId == userId*/);

            if (blog == null)
            {
                return RedirectToAction(nameof(BlogController.Index));
            }
            var model = new PostBlogViewModel();
            model.Title = blog.Title;
            model.Body = blog.Body;
            model.Published = blog.Published;
            model.DateCreated = DateTime.Now;
            model.DateUpdated = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, PostBlogViewModel formData)
        {
           return SaveBlog(id, formData);
        }

        //GET THE DELETE
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(BlogController.Index));
            }

            var userId = User.Identity.GetUserId();

            var blogss = DbContext.Blogs.FirstOrDefault(p => p.Id == id && p.UserId == userId);

            if (blogss != null)
            {
                DbContext.Blogs.Remove(blogss);
                DbContext.SaveChanges();
            }

            return RedirectToAction(nameof(BlogController.Index));
        }


        //get the Details//
        [HttpGet]
        public ActionResult Detail(int? id)
        {
            if (!id.HasValue)
            return RedirectToAction(nameof(BlogController.Index));
            var userId = User.Identity.GetUserId();
            var blog = DbContext.Blogs.FirstOrDefault(p =>
            p.Id == id.Value);
            if (blog == null)
            return RedirectToAction(nameof(BlogController.Index));
            var model = new DetailBlogViewModel();
            model.Title = blog.Title;
            model.Published = blog.Published;
            model.Body = blog.Body;
            model.MediaUrl = blog.MediaUrl;
            model.DateCreated = blog.DateCreated;
            model.DateUpdated = blog.DateUpdated;
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateComment()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult CreateComment(DetailBlogViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var newComment = new Comment();
            if (newComment == null)
            {
                return RedirectToAction(nameof(BlogController.Detail));
            }
            newComment.UserId = User.Identity.GetUserId();
            newComment.DateCreated = DateTime.Now;
            newComment.DateUpdated = DateTime.Now;
            newComment.Body = model.Body;
            DbContext.Comments.Add(newComment);
            DbContext.SaveChanges();
            return RedirectToAction(nameof(BlogController.Detail));
        }  
    }
}


