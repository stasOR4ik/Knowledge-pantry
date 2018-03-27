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
    public class CreateNewSummaryController : ManageController
    {
        public CreateNewSummaryController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, ILogger<ManageController> logger, UrlEncoder urlEncoder, ApplicationDbContext context) : base(userManager, signInManager, emailSender, logger, urlEncoder, context)
        {
        }

        [HttpGet]
        public IActionResult CreateNewSummary()
        {
            return View();
        }

    }
}
