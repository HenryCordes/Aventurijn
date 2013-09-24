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
using Aventurijn.Activities.Web.Models.Extensions;
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
           
            var viewModel = new ParticipationsViewModel(GetActivityList(),
                                                        GetStudentsList(),
                                                        GetSubjectsList());

            viewModel.FromDate = DateTime.UtcNow.Date.AddDays(-7);
            viewModel.ToDate = DateTime.UtcNow.Date;
            
            viewModel.Participations = GetParticipations(viewModel.FromDate, viewModel.ToDate, 0);

            return View(viewModel);
        }

         [HttpPost]
        public ActionResult Index(FormCollection form)
         {

             var collection = Request.Form;
             DateTime from = DateTime.MinValue;
             DateTime to = DateTime.MinValue;
             int studentId = 0;


             if (collection["from"] != null)
             {
                 DateTime.TryParse(collection["from"], out from);
             }

             if (collection["to"] != null)
             {
                 DateTime.TryParse(collection["to"], out to);
             }

             if (collection["studentId"] != null)
             {
                 int.TryParse(collection["studentId"], out studentId);

             }
     
             var viewModel = new ParticipationsViewModel(GetActivityList(),
                                                        GetStudentsList(),
                                                        GetSubjectsList());
             if (from > DateTime.MinValue && to > DateTime.MinValue)
             {
                 viewModel.FromDate = from;
                 viewModel.ToDate = to;
             }
             else
             {
                 viewModel.FromDate = DateTime.UtcNow.Date.AddDays(-7);
                 viewModel.ToDate = DateTime.UtcNow.Date;
             }


             viewModel.Participations = GetParticipations(viewModel.FromDate, viewModel.ToDate, studentId);

             return View(viewModel);

         }

         [HttpPost]
        public ActionResult Indexorg([FromJson] IEnumerable<Participation> participations)
        {
            if (ModelState.IsValid)
            {
                SaveParticipations(participations);
            }
            var viewModel = new ParticipationsViewModel(GetActivityList(),
                                                        GetStudentsList(),
                                                        GetSubjectsList());
            viewModel.FromDate = DateTime.UtcNow.Date.AddDays(-7);
            viewModel.ToDate = DateTime.UtcNow.Date;
            viewModel.Participations = participations;

            return View(viewModel);
        }

         public ActionResult New()
         {
             var viewModel = new NewParticipationsViewModel(GetSubjectsList(),
                                                            GetActivityList(),
                                                            GetStudentsList(),
                                                            GetLevelsList());
             viewModel.Participations = null;
             viewModel.ParticipationDate = DateTime.UtcNow.Date;

             return View(viewModel);
         }

         [HttpPost]
         [HandleError]
         public JsonResult New(NewParticipationsRequest request)
         {
            // var activity = db.Activities.Find(request.ActivityId);
            var participations =  new List<Participation>();
             foreach (var studentId in request.StudentIds)
             {
                // var student = db.Students.Find(studentId);

                 var participation = new Participation()
                     {
                         ActivityId = request.ActivityId,
                         Activity = db.Activities.Find(request.ActivityId),
                         SubjectId = request.SubjectId,
                         Subject = db.Subjects.Find(request.SubjectId),
                         StudentId = studentId,
                         Student = db.Students.Find(studentId),
                         ParticipationDateTime = request.ParticipationDate,
                         ExtraInfo = request.ExtraInfo
                     };
                 db.Participations.Add(participation);
                 participations.Add(participation);
             }
             db.SaveChanges();

             return Json(participations);
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
            participation.Subject = db.Subjects.Find(participation.SubjectId);
            participation.Student = db.Students.Find(participation.StudentId);
            return View(participation);
        }

        //
        // GET: /Participation/Create

        public ActionResult Create()
        {
            var viewModel = new ParticipationViewModel(GetActivityList(), GetStudentsList());
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
            var viewModel = new ParticipationViewModel(GetActivityList(), GetStudentsList())
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
            var viewModel = new ParticipationViewModel(GetActivityList(), GetStudentsList())
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
                participation.Subject = db.Subjects.Find(participation.Subject.SubjectId);
                participation.Student = db.Students.Find(participation.StudentId);
                db.Entry(participation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var viewModel = new ParticipationViewModel(GetActivityList(), GetStudentsList())
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
            participation.Activity = db.Activities.Find(participation.ActivityId);
            participation.Subject = db.Subjects.Find(participation.SubjectId);
            participation.Student = db.Students.Find(participation.StudentId);
            return View(participation);
        }

        //
        // POST: /Participation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DeleteParticipation(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Remove(int id)
        {
            DeleteParticipation(id);
            return Json(id);
        }


        [HttpPost]
        [HandleError]
        public JsonResult Save(IEnumerable<Participation> participations)
        {
            try
            {
                participations = SaveParticipations(participations);
                return Json(participations);
            }
            catch
            {
                return Json(false);
            }

        }

        [HttpPost]
        [HandleError]
        public JsonResult WithinDateRange(DateTime from, DateTime to, int studentId = 0)
        {

            var participations = GetParticipations(from, to, studentId);

            return Json(participations);
        }


        [HandleError]
        public ActionResult PerSubject(int? id)
        {
            var from = DateTime.UtcNow.Date.AddMonths(-1);
            var to = DateTime.UtcNow.Date;
            const int subjectId = 0;

            var viewModel = ParticipationsPerSubjectViewModel(id, from, to);

            return View("PerSubject", viewModel);
        }


        [HandleError]
        public JsonResult SubjectOrdered(DateTime from, DateTime to, int? id)
        {
            var viewModel = ParticipationsPerSubjectViewModel(id, from, to);

            return Json(viewModel);
        }

     
        public ActionResult ReadOnly()
        {
            DateTime from = DateTime.Parse(Request.QueryString["from"]);
            DateTime to = DateTime.Parse(Request.QueryString["to"]);
            int studentId = int.Parse(Request.QueryString["studentId"]);
 
            var participations = GetParticipations(from, to, studentId);
            return View("readonly", participations);
        }

        private void DeleteParticipation(int id)
        {
            Participation participation = db.Participations.Find(id);
            db.Participations.Remove(participation);
            db.SaveChanges();
        }


        private ParticipationsPerSubjectViewModel ParticipationsPerSubjectViewModel(int? id, DateTime from, DateTime to)
        {
            var subjectId = 0;
            if (id.HasValue)
            {
                subjectId = (int)id;
            }

            var viewModel = new ParticipationsPerSubjectViewModel(GetSubjectsList());
            viewModel.FromDate = from;
            viewModel.ToDate = to;
            viewModel.ParticipationsPerSubject = GetParticipationsPerSubject(from, to, subjectId);
            return viewModel;
        }


        private IEnumerable<Participation> SaveParticipations(IEnumerable<Participation> participations)
        {
            foreach (var participation in participations)
            {
                participation.Activity = db.Activities.Find(participation.ActivityId);
                participation.Subject = db.Subjects.Find(participation.SubjectId);
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

            return participations;
        }

        private List<Participation> GetParticipations(DateTime from, DateTime to, int studentId)
        {
             var queryableParticipations = db.Participations.Where(p => p.ParticipationDateTime > from &&
                                                                        p.ParticipationDateTime <= to);
             if (studentId > 0)
             {
                 queryableParticipations = queryableParticipations.Where(p => p.StudentId == studentId);
             }
             var participations = queryableParticipations.OrderBy(p => p.ParticipationDateTime).ToList();

             foreach (var participation in participations)
             {
                 participation.Activity = db.Activities.Find(participation.ActivityId);
                 participation.Subject = db.Subjects.Find(participation.SubjectId == 0 ? participation.Activity.SubjectId : participation.SubjectId);

                 participation.Student = db.Students.Find(participation.StudentId);
             }
             return participations;
        }

        private IEnumerable<ParticipationsPerSubject> GetParticipationsPerSubject(DateTime from, DateTime to, int subjectId = 0)
        {
            var result = new List<ParticipationsPerSubject>();
            var queryableParticipations = db.Participations.Where(p => p.ParticipationDateTime > from &&
                                                                       p.ParticipationDateTime <= to &&
                                                                       p.Participating == true);
            if (subjectId > 0)
            {
                queryableParticipations = queryableParticipations.Where(p => p.SubjectId == subjectId);
            }
            var participations = queryableParticipations.OrderBy(p => p.ParticipationDateTime).ToList();

            foreach (var participation in participations)
            {
                var activity = db.Activities.Find(participation.ActivityId);
                var subject = db.Subjects.Find(activity.SubjectId);
           
                var participationPerSubject = new ParticipationsPerSubject()
                    {
                        SubjectName = subject.Name,
                        SubjectId = subject.SubjectId,
                        ActivityName = activity.Name,
                        ActivityId = participation.ActivityId,
                        NumberOfParticipations = participations.Count(p => p.ActivityId == participation.Activity.ActivityId)
                    };
                result.Add(participationPerSubject);
            }
            return result.DistinctBy(p => p.ActivityId).OrderBy(p => p.SubjectName).ThenBy(p => p.ActivityName);
        }

        private IEnumerable<Activity> GetActivityList()
        {
            return db.Activities.Where(a => a.Active == true).OrderBy(a => a.Name);
        }

        private IEnumerable<Student> GetStudentsList()
        {
            return db.Students.OrderBy(s => s.Name);
        }


        private IEnumerable<Subject> GetSubjectsList()
        {
            return db.Subjects.OrderBy(su => su.Name);
        }

        private IEnumerable<Level> GetLevelsList()
        {
            return db.Levels.OrderBy(su => su.LevelId);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}