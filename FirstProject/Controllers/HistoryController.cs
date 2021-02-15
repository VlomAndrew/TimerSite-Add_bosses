using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstProject.Data;
using FirstProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FirstProject.Controllers
{
    public class HistoryController : Controller
    {
        // GET: History
        BossContext db = new BossContext();
        public ActionResult Index()
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = null;
            user = userManager.FindByName(User.Identity.Name);
            if (User.Identity.IsAuthenticated)
            {
                if (user == null)
                {
                    return RedirectToAction("LogOffProg", "Account");
                }
            }
            var histList = db.History.Take(50).ToList();
            ViewBag.Hist = histList;
            return View(ViewBag);
        }
    }
}