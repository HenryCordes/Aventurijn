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
    public class ParticipationController : Controller
    {
        private AvonturijnContext db = new AvonturijnContext();

        //
        // GET: /Participation/

        public ActionResult Index()
        {
            var participations = db.Participations.ToList();
            foreach (var participation in participations)
            {
                participation.Activity = db.Activities.Find(participation.ActivityId);
                participation.Activity.Subject = db.Subjects.Find(participation.Activity.SubjectId);
                participation.Student = db.Students.Find(participation.StudentId);
            }

            return View(participations);
        }

        //
        // GET: /Participation/Details/5

        public ActionResult Details(int id = 0)
        {
            Participation participation = db.Participations.Find(id);
            if (participation == null)
            {
                return HttpNotFound();
            }
            participation.Activity = db.Activities.Find(participation.ActivityId);
            participation.Activity.Subject = db.Subjects.Find(participation.Activity.SubjectId);
            participation.Student = db.Students.Find(participation.StudentId);
            return View(participation);
        }

        //
        // GET: /Participation/Create

        public ActionResult Create()
        {
            var viewModel = new ParticipationViewModel(db.Activities, db.Students);
            return View(viewModel);
        }

        //
        // POST: /Participation/Create

        [HttpPost]
        public ActionResult Create(Participation participation)
        {
            if (ModelState.IsValid)
            {
               // participation.Activity = db.Activities.Find(participation.ActivityId);
              //  participation.Student = db.Students.Find(participation.StudentId);
                db.Participations.Add(participation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var viewModel = new ParticipationViewModel(db.Activities, db.Students)
            {
                Participation = participation
            };
            return View(viewModel);
        }

        //
        // GET: /Participation/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Participation participation = db.Participations.Find(id);
            if (participation == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ParticipationViewModel(db.Activities, db.Students)
            {
                Participation = participation
            };
            return View(viewModel);
        }

        //
        // POST: /Participation/Edit/5

        [HttpPost]
        public ActionResult Edit(Participation participation)
        {
            if (ModelState.IsValid)
            {
                participation.Activity = db.Activities.Find(participation.ActivityId);
                participation.Activity.Subject = db.Subjects.Find(participation.Activity.SubjectId);
                participation.Student = db.Students.Find(participation.StudentId);
                db.Entry(participation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var viewModel = new ParticipationViewModel(db.Activities, db.Students)
            {
                Participation = participation
            };
            return View(viewModel);
        }

        //
        // GET: /Participation/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Participation participation = db.Participations.Find(id);
            if (participation == null)
            {
                return HttpNotFound();
            }
            return View(participation);
        }

        //
        // POST: /Participation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Participation participation = db.Participations.Find(id);
            db.Participations.Remove(participation);
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