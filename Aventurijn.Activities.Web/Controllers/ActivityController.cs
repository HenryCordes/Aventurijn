using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aventurijn.Activities.Web.Models.Domain;
using Aventurijn.Activities.Web.Models.Context;
using Aventurijn.Activities.Web.Models.ViewModel;

namespace Aventurijn.Activities.Web.Controllers
{
    [Authorize]
    public class ActivityController : Controller
    {
        private AvonturijnContext db = new AvonturijnContext();

        //
        // GET: /Activity/

        public ActionResult Index()
        {
            var activities = db.Activities.ToList();
            foreach (var activity in activities)
            {
                activity.Subject = db.Subjects.Find(activity.SubjectId);
            }
            return View(activities);
        }

        //
        // GET: /Activity/Details/5

        public ActionResult Details(long id = 0)
        {
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            activity.Subject = db.Subjects.Find(activity.SubjectId);
            return View(activity);
        }

        //
        // GET: /Activity/Create

        public ActionResult Create()
        {
            var viewModel = new ActivityViewModel(db.Subjects.OrderBy(s => s.Name));
            return View(viewModel);
        }

        //
        // POST: /Activity/Create

        [HttpPost]
        public ActionResult Create(Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Activities.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var viewModel = new ActivityViewModel(db.Subjects.OrderBy(s => s.Name))
                {
                    Activity = activity
                };
            return View(viewModel);
        }

        //
        // GET: /Activity/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ActivityViewModel(db.Subjects.OrderBy(s => s.Name))
            {
                Activity = activity
            };
            return View(viewModel);
          
        }

        //
        // POST: /Activity/Edit/5

        [HttpPost]
        public ActionResult Edit(Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var viewModel = new ActivityViewModel(db.Subjects.OrderBy(s => s.Name))
            {
                Activity = activity
            };
            return View(viewModel);
        }

        //
        // GET: /Activity/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        //
        // POST: /Activity/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Activity activity = db.Activities.Find(id);
            db.Activities.Remove(activity);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}