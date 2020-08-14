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
            var user = userManager.FindByEmail(User.Identity.Name);
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
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}