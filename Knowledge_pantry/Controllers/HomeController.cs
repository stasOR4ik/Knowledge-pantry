using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Knowledge_pantry.Models;
using Knowledge_pantry.Data;
using Microsoft.EntityFrameworkCore;

namespace Knowledge_pantry.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;
        public HomeController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index(SortState sortOrder = SortState.LastUpdateTimeDescendingly)
        {
            IQueryable<Summary> summaries = db.Summaries;

            ViewData["CaptionSort"] = sortOrder == SortState.CaptionAscending ? 
                SortState.CaptionDescendingly : SortState.CaptionAscending;
            ViewData["LastUpdateTimeSort"] = sortOrder == SortState.LastUpdateTimeAscending ?
                SortState.LastUpdateTimeDescendingly : SortState.LastUpdateTimeAscending;
            ViewData["LikeSort"] = sortOrder == SortState.LikeAscending ? SortState.LikeDescendingly : SortState.LikeAscending;
            ViewData["NumberOfSpecialtySort"] = sortOrder == SortState.NumberOfSpecialtyAscending ?
                SortState.NumberOfSpecialtyDescendingly : SortState.NumberOfSpecialtyAscending;

            switch (sortOrder)
            {
                case SortState.CaptionAscending:
                    summaries = summaries.OrderBy(s => s.Caption);
                    break;
                case SortState.CaptionDescendingly:
                    summaries = summaries.OrderByDescending(s => s.Caption);
                    break;
                case SortState.LastUpdateTimeDescendingly:
                    summaries = summaries.OrderByDescending(s => s.LastUpdateTime);
                    break;
                case SortState.LikeAscending:
                    summaries = summaries.OrderBy(s => s.Like);
                    break;
                case SortState.LikeDescendingly:
                    summaries = summaries.OrderByDescending(s => s.Like);
                    break;
                case SortState.NumberOfSpecialtyAscending:
                    summaries = summaries.OrderBy(s => s.NumberOfSpecialty);
                    break;
                case SortState.NumberOfSpecialtyDescendingly:
                    summaries = summaries.OrderByDescending(s => s.NumberOfSpecialty);
                    break;
                default:
                    summaries = summaries.OrderBy(s => s.LastUpdateTime);
                    break;
            }
            return View(summaries.AsNoTracking().ToList());
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
