using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Windows.Forms.VisualStyles;
using FirstProject.Data;
using FirstProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FirstProject.Controllers
{
    public class BossViewModelsController : Controller
    {
        private BossContext db = new BossContext();

        // GET: BossViewModels
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
            return View(db.BossViewModels.ToList());
        }

        // GET: BossViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BossViewModel bossViewModel = db.BossViewModels.Find(id);
            if (bossViewModel == null)
            {
                return HttpNotFound();
            }
            return View(bossViewModel);
        }

        // GET: BossViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BossViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,KdCount,KdTime,Color")] BossViewModel bossViewModel)
        {
            if (ModelState.IsValid)
            {
                var dtnow_now = DateTime.Now.ToUniversalTime();
                TimeZoneInfo moscowTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
                DateTime dtnow = TimeZoneInfo.ConvertTimeFromUtc(dtnow_now, moscowTimeZone);
                var time_str = string.Format("{0}.{1}.{2}", dtnow.Hour < 10 ? "0" + dtnow.Hour.ToString() : dtnow.Hour.ToString()
                    , dtnow.Minute < 10 ? "0" + dtnow.Minute.ToString() : dtnow.Minute.ToString()
                    , dtnow.Second < 10 ? "0" + dtnow.Second.ToString() : dtnow.Second.ToString());
                var res = string.Format("{0}.{1}.{2} {3}:{4}:{5}", dtnow.Day, dtnow.Month, dtnow.Year, dtnow.Hour, dtnow.Minute, dtnow.Second);
                bossViewModel.LastTime = res;
                bossViewModel.KdTime = bossViewModel.KdTime.Replace(':', '.').Replace(',', '.').Replace(' ', '.');
                db.BossViewModels.Add(bossViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(bossViewModel);
        }

        // GET: BossViewModels/Edit/5
        public ActionResult Edit(int? id, bool fl = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BossViewModel bossViewModel = db.BossViewModels.Find(id);
            if (bossViewModel == null)
            {
                return HttpNotFound();
            }

            if (!fl)
            {
                return View(bossViewModel);
            }

            return Edit(bossViewModel,fl);
        }

        // POST: BossViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,KdCount,KdTime,LastTime,Color")] BossViewModel bossViewModel,bool fl = false)
        {
            if (ModelState.IsValid)
            {
                var dtnow_now = DateTime.Now.ToUniversalTime(); 
                TimeZoneInfo moscowTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
                DateTime dtnow = TimeZoneInfo.ConvertTimeFromUtc(dtnow_now, moscowTimeZone);
                HistoryModel hist = new HistoryModel();
                hist.Name = User.Identity.Name;
                
                
                if (fl)
                {
                    var res = string.Format("{0}.{1}.{2} {3}:{4}:{5}", dtnow.Day, dtnow.Month, dtnow.Year, dtnow.Hour, dtnow.Minute, dtnow.Second);
                    bossViewModel.LastTime = res;
                    hist.Type = "Ткнул упал сейчас";
                }
                else
                {
                    var mass = bossViewModel.LastTime.Split(' ');
                    var str = string.Empty;
                    for (int i = 1; i < mass.Length; i++)
                    {
                        str += mass[i] + ' ';
                    }

                    str = str.Remove(str.Length - 1);
                    bossViewModel.LastTime = mass[0] + ' ' + str.Replace('.', ':').Replace(',', ':').Replace('-', ':')
                        .Replace(' ', ':');
                    hist.Type = "Ткнул точное время";
                }

                hist.LastTime = bossViewModel.LastTime;
                hist.BossName = bossViewModel.Name;
                db.Entry(bossViewModel).State = EntityState.Modified;
                db.History.Add(hist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bossViewModel);
        }

        // GET: BossViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BossViewModel bossViewModel = db.BossViewModels.Find(id);
            if (bossViewModel == null)
            {
                return HttpNotFound();
            }
            return View(bossViewModel);
        }

        // POST: BossViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BossViewModel bossViewModel = db.BossViewModels.Find(id);
            db.BossViewModels.Remove(bossViewModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
