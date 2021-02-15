using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FirstProject.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        [Authorize]
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

            if (user != null)
            {
                var role = userManager.GetRoles(user.Id);
                if (role.Contains(" Admin"))
                {
                    return View();
                }
            }
            return View();
        }

        [AllowAnonymous]
        [Authorize]
        public ActionResult About()
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
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        [Authorize]
        public ActionResult Contact()
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
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}