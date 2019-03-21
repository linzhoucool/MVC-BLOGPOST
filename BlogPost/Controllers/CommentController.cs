using BlogPost.Models;
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
    public class CommentController : Controller
    {
        private ApplicationDbContext DbContext;
        public CommentController()
        {
            DbContext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var comment = DbContext.Comments
                .Where(p => p.UserId == userId)
                .Select(p => new IndexCommentViewModel
                {
                    Id = p.Id,
                    Body = p.Body,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    UpdateReason = p.UpdateReason
                }).ToList();
            return View(comment);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var userId = User.Identity.GetUserId();
            var comments = new Comment();
            comments.UserId = userId;
            comments.Body = model.Body;
            comments.DateCreated =DateTime.Now;
            comments.DateUpdated = DateTime.Now;
            comments.BlogId = model.BlogId;
            DbContext.Comments.Add(comments);
            DbContext.SaveChanges();
            return RedirectToAction(nameof(CommentController.Index));
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(CommentController.Index));
            }
            var comments = DbContext.Comments.FirstOrDefault(p => p.Id == id.Value);
            if (comments == null)
            {
                return RedirectToAction(nameof(CommentController.Index));
            }
            var newComment = new CreateViewModel();
            newComment.Body = comments.Body;
            newComment.UpdateReason = comments.UpdateReason;
            return View(newComment);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Edit(int? id, CreateViewModel model)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(CommentController.Index));
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            var comment = DbContext.Comments.FirstOrDefault(p => p.Id == id.Value);
            comment.Body = model.Body;
            comment.UpdateReason = model.UpdateReason;
            comment.DateCreated = DateTime.Now;
            comment.DateUpdated = DateTime.Now;
            DbContext.SaveChanges();
            return RedirectToAction(nameof(CommentController.Index));
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(CommentController.Index));
            }
            var comment = DbContext.Comments.FirstOrDefault(p => p.Id == id.Value);
            if (comment == null)
            {
                return RedirectToAction(nameof(CommentController.Index));
            }
            DbContext.Comments.Remove(comment);
            DbContext.SaveChanges();
            return RedirectToAction(nameof(CommentController.Index));
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {

            if (!id.HasValue)
            {
                return RedirectToAction(nameof(CommentController.Index));
            }
            var comment = DbContext.Comments.FirstOrDefault(p => p.Id == id.Value);
            if (comment == null)
            {
                return RedirectToAction(nameof(CommentController.Index));
            }
            var newComment = new DetailCommentViewModel();
            newComment.Body = comment.Body;
            newComment.DateCreated = comment.DateCreated;
            newComment.DateUpdated = comment.DateUpdated;
            newComment.UpdateReason = comment.UpdateReason;
            return View(newComment);
        }
    }
}