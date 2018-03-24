using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Knowledge_pantry.Models;
using Knowledge_pantry.Data;

namespace Knowledge_pantry.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;
        public HomeController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Summaries.ToList());
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpPost]
        public IActionResult About(Summary summary)
        {
            db.Summaries.Add(summary);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
