using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstProject.Controllers
{
    public class TimerController : Controller
    {
        // GET: Timer
        [Authorize(Roles = "Admin,Officer")]
        public ActionResult Index()
        {
            
            return View();
        }
    }
}