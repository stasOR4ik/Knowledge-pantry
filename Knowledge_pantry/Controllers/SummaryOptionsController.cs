using Knowledge_pantry.Data;
using Knowledge_pantry.Models;
using Knowledge_pantry.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            Summary summary = new Summary
            {
                Caption = caption,
                Annotation = annotation,
                Like = 0,
                NumberOfSpecialty = numberOfSpecialty,
                Text = text,
                LinkToCreator = _userManager.GetUserId(User),
                LastUpdateTime = DateTime.Now
            };
            db.Add(summary);
            db.SaveChanges();
            return RedirectToAction("CreateSummary");
        }

        [HttpGet]
        public IActionResult ViewAndEditSummary(int id)
        {
            return View(db.Summaries.FirstOrDefault(p => p.Id == id));
        }

        [HttpPost]
        public IActionResult ViewAndEditSummary(bool delete, string caption, int numberOfSpecialty, string annotation, string text, int id, int like)
        {
            Summary summary = db.Summaries.FirstOrDefault(p => p.Id == id);
            if (delete != true)
            {
                summary.Caption = caption;
                summary.Annotation = annotation;
                summary.Like = like;
                summary.NumberOfSpecialty = numberOfSpecialty;
                summary.Text = text;
                summary.LastUpdateTime = DateTime.Now;
                db.Update(summary);
                db.SaveChanges();
                
            }
           else
            {
                db.Remove(summary);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
