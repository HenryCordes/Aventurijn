using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aventurijn.Activities.Web.Attributes;
using Aventurijn.Activities.Web.Models.Domain;
using Aventurijn.Activities.Web.Models.Context;
using Aventurijn.Activities.Web.Models.ViewModel;

namespace Aventurijn.Activities.Web.Controllers
{
     [Authorize]
    public class ParticipationController : Controller
    {
        private AvonturijnContext db = new AvonturijnContext();

        //
        // GET: /Participation/

        public ActionResult Index()
        {
            var viewModel = new ParticipationsViewModel(db.Activities.OrderBy(a => a.Name),
                                                        db.Students.OrderBy(s => s.Name),
                                                        db.Subjects.OrderBy(su => su.Name));
            viewModel.FromDate = DateTime.UtcNow.Date.AddMonths(-1);
            viewModel.ToDate = DateTime.UtcNow.Date;

            var participations = db.Participations.Where(p => p.ParticipationDateTime > viewModel.FromDate &&
                                                              p.ParticipationDateTime < viewModel.ToDate)
                                                  .OrderBy(p => p.ParticipationDateTime).ToList();

            foreach (var participation in participations)
            {
                participation.Activity = db.Activities.Find(participation.ActivityId);
                participation.Activity.Subject = db.Subjects.Find(participation.Activity.SubjectId);
                participation.Student = db.Students.Find(participation.StudentId);
            }
            
            viewModel.Participations = participations.OrderBy(p => p.ParticipationDateTime);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index([FromJson] IEnumerable<Participation> participations)
        {
            if (ModelState.IsValid)
            {
                SaveParticipations(participations);
            }
            var viewModel = new ParticipationsViewModel(db.Activities.OrderBy(a => a.Name),
                                                        db.Students.OrderBy(s => s.Name),
                                                        db.Subjects.OrderBy(su => su.Name));
            viewModel.FromDate = DateTime.UtcNow.Date.AddMonths(-1);
            viewModel.ToDate = DateTime.UtcNow.Date;
            viewModel.Participations = participations;

            return View(viewModel);
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
            var viewModel = new ParticipationViewModel(db.Activities.OrderBy(a => a.Name), db.Students.OrderBy(s => s.Name));
            return View(viewModel);
        }

        //
        // POST: /Participation/Create

        [HttpPost]
        public ActionResult Create(Participation participation)
        {
            if (ModelState.IsValid)
            {
                db.Participations.Add(participation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var viewModel = new ParticipationViewModel(db.Activities.OrderBy(a => a.Name), db.Students.OrderBy(s => s.Name))
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
            var viewModel = new ParticipationViewModel(db.Activities.OrderBy(a => a.Name), db.Students.OrderBy(s => s.Name))
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
            var viewModel = new ParticipationViewModel(db.Activities.OrderBy(a => a.Name), db.Students.OrderBy(s => s.Name))
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

        [HttpPost]
        [HandleError]
        public JsonResult Save(IEnumerable<Participation> participations)
        {
      //    if (ModelState.IsValid)
     //       {
                SaveParticipations(participations);
                return Json(true);
      //      }
     //       else
     //       {
        //        return Json(false);
      //      }
        }

        [HttpPost]
        public JsonResult WithinDateRange(DateTime from, DateTime to, int studentId = 0)
        {
            var participations = GetParticipations(from, to, studentId);

            return Json(participations);
        }

        public ActionResult ReadOnly()
        {
            DateTime from = DateTime.Parse(Request.QueryString["from"]);
            DateTime to = DateTime.Parse(Request.QueryString["to"]);
            int studentId = int.Parse(Request.QueryString["studentId"]);
 
            var participations = GetParticipations(from, to, studentId);
            return View("readonly", participations);
        }

        private void SaveParticipations(IEnumerable<Participation> participations)
        {
            foreach (var participation in participations)
            {
                participation.Activity = db.Activities.Find(participation.ActivityId);
                participation.Activity.Subject = db.Subjects.Find(participation.Activity.SubjectId);
                participation.Student = db.Students.Find(participation.StudentId);
                if (participation.ParticipationId == 0)
                {
                    db.Entry(participation).State = EntityState.Added;
                }
                else
                {
                    db.Entry(participation).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
        }

        private List<Participation> GetParticipations(DateTime from, DateTime to, int studentId)
        {
             var queryableParticipations = db.Participations.Where(p => p.ParticipationDateTime > from &&
                                                                        p.ParticipationDateTime < to);
             if (studentId > 0)
             {
                 queryableParticipations = queryableParticipations.Where(p => p.StudentId == studentId);
             }
             var participations = queryableParticipations.OrderBy(p => p.ParticipationDateTime).ToList();

             foreach (var participation in participations)
             {
                 participation.Activity = db.Activities.Find(participation.ActivityId);
                 participation.Activity.Subject = db.Subjects.Find(participation.Activity.SubjectId);
                 participation.Student = db.Students.Find(participation.StudentId);
             }
             return participations;
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}