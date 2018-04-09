using Knowledge_pantry.Data;
using Knowledge_pantry.Models;
using Knowledge_pantry.Models.SummaryOptionsViewModels;
using Knowledge_pantry.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Knowledge_pantry.Controllers
{

    public class SummaryOptionsController : ManageController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly UrlEncoder _urlEncoder;

        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
        private const string RecoveryCodesKey = nameof(RecoveryCodesKey);

        ApplicationDbContext db;

        public SummaryOptionsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, ILogger<ManageController> logger, UrlEncoder urlEncoder, ApplicationDbContext context) : base(userManager, signInManager, emailSender, logger, urlEncoder, context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _urlEncoder = urlEncoder;
            db = context;
        }

        [HttpGet]
        public IActionResult CreateSummary()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateSummary(string caption, int numberOfSpecialty, string annotation, string text)
        {
            Summary summary = new Summary(caption, annotation, numberOfSpecialty, text, _userManager.GetUserId(User));
            db.Summaries.Add(summary);
            //ApplicationUser user = db.Users.FirstOrDefault(p => p.Id == _userManager.GetUserId(User));
            //user.Summaries.Add(summary);
            //db.Users.Update(user);
            db.SaveChanges();
            return RedirectToAction("ViewAndEditSummary", new { id = summary.Id });
        }

        [HttpGet]
        public IActionResult ViewAndEditSummary(int id)
        {
            Summary summary = db.Summaries.Include(p => p.Comments).FirstOrDefault(p => p.Id == id);
            if (_userManager.GetUserId(User) != summary.LinkToCreator)
            {
                return RedirectToAction("ViewSummary", new { id = id });
            }
            else
            {
                List<Comment> temporarySummaryComments = new List<Comment>();
                if (summary.Comments.Count != 0)
                {
                    foreach (var comment in summary.Comments)
                    {
                        if (comment.ParentCommentId != 0)
                        {
                            comment.Text = db.Comments.FirstOrDefault(p => p.Id == comment.ParentCommentId).CreatorName + ", " + comment.Text;
                        }
                        temporarySummaryComments.Add(comment);
                    }
                    return View(new ViewAndEditSummaryViewModel(summary, temporarySummaryComments.OrderByDescending(s => s.CreationTime).ToList()));
                }
                else
                {
                    return View(new ViewAndEditSummaryViewModel(summary, null));
                }
            }
        }

        [HttpPost]
        public IActionResult ViewAndEditSummary(bool delete, string caption, int numberOfSpecialty, string annotation, string text,
            int id, int like, bool messageExist, int parentId, string messageText, bool likeExist, int commentId, bool view)
        {
            Summary summary = db.Summaries.Include(x => x.Comments).FirstOrDefault(p => p.Id == id);
            if (!delete)
            {
                if (!messageExist)
                {
                    if (view)
                    {
                        return RedirectToAction("ViewSummary", new { id = id });
                    }
                    else
                    {
                        if (likeExist)
                        {
                            Comment comment = db.Summaries.FirstOrDefault(p => p.Id == id).Comments.FirstOrDefault(p => p.Id == commentId);
                            comment.Like++;
                            db.Comments.Update(comment);
                            db.Summaries.Update(summary);
                            db.SaveChanges();
                            return RedirectToAction("ViewAndEditSummary", new { id = id });
                        }
                        else
                        {
                            summary.Caption = caption;
                            summary.Annotation = annotation;
                            summary.Like = like;
                            summary.NumberOfSpecialty = numberOfSpecialty;
                            summary.Text = text;
                            summary.LastUpdateTime = DateTime.Now;
                            db.Update(summary);
                        }
                    }
                }
                else
                {
                    Comment comment = new Comment(_userManager.GetUserName(User), parentId, db.Summaries.FirstOrDefault(p => p.Id == id),
                        messageText);
                    db.Comments.Add(comment);
                    db.Summaries.Update(summary);
                    db.SaveChanges();
                    return RedirectToAction("ViewAndEditSummary", new { id = id });
                }
            }
           else
            {
                db.Remove(summary);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ViewSummary(int id)
        {
            Summary summary = db.Summaries.Include(x => x.Comments).FirstOrDefault(p => p.Id == id);
            List<Comment> temporarySummaryComments = new List<Comment>();
            if (summary.Comments != null)
            {
                foreach (var comment in summary.Comments)
                {
                    if (comment.ParentCommentId != 0)
                    {
                        comment.Text = db.Comments.FirstOrDefault(p => p.Id == comment.ParentCommentId).CreatorName + ", " + comment.Text;
                    }
                    temporarySummaryComments.Add(comment);
                }
                return View(new ViewAndEditSummaryViewModel(summary, temporarySummaryComments.OrderByDescending(s => s.CreationTime).ToList()));
            }
            else
            {
                return View(new ViewAndEditSummaryViewModel(summary, null));
            }
        }

        [HttpPost]
        public IActionResult ViewSummary(int id, int like, bool messageExist, int parentId, string messageText, bool likeExist, int commentId)
        {
            Summary summary = db.Summaries.Include(x => x.Comments).FirstOrDefault(p => p.Id == id);
            if (messageExist)
            {
                Comment comment = new Comment(_userManager.GetUserName(User), parentId, db.Summaries.FirstOrDefault(p => p.Id == id),
                    messageText);
                db.Comments.Add(comment);
                db.Summaries.Update(summary);
                db.SaveChanges();
                return RedirectToAction("ViewSummary", new { id = id });
            }
            else if (likeExist)
            {
                Comment comment = db.Summaries.FirstOrDefault(p => p.Id == id).Comments.FirstOrDefault(p => p.Id == commentId);
                comment.Like++;
                db.Comments.Update(comment);
                db.Summaries.Update(summary);
                db.SaveChanges();
                return RedirectToAction("ViewSummary", new { id = id });
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
