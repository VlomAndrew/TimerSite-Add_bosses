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

namespace FirstProject.Controllers
{
    public class BossViewModelsController : Controller
    {
        private BossContext db = new BossContext();

        // GET: BossViewModels
        public ActionResult Index()
        {
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
                bossViewModel.LastTime = time_str;
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
        public ActionResult Edit([Bind(Include = "Id,Name,KdCount,KdTime,LastTime")] BossViewModel bossViewModel,bool fl = false)
        {
            if (ModelState.IsValid)
            {
                var dtnow_now = DateTime.Now.ToUniversalTime(); 
                TimeZoneInfo moscowTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
                DateTime dtnow = TimeZoneInfo.ConvertTimeFromUtc(dtnow_now, moscowTimeZone);
                var time_str = string.Format("{0}.{1}.{2}", dtnow.Hour < 10 ? "0" + dtnow.Hour.ToString() : dtnow.Hour.ToString()
                    , dtnow.Minute < 10 ? "0" + dtnow.Minute.ToString() : dtnow.Minute.ToString()
                    , dtnow.Second < 10 ? "0" + dtnow.Second.ToString() : dtnow.Second.ToString());
                if (fl) 
                    bossViewModel.LastTime = time_str;
                bossViewModel.LastTime = bossViewModel.LastTime.Replace(':', '.').Replace(',', '.').Replace('-', '.')
                    .Replace(' ', '.');
                db.Entry(bossViewModel).State = EntityState.Modified;
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
