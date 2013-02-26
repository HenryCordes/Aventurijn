using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using Aventurijn.Activities.Web.Models.Domain;

namespace Aventurijn.Activities.Web.Models.ViewModel
{
    public class ParticipationsViewModel
    {
        public ParticipationsViewModel(IEnumerable<Activity> activities,
                                       IEnumerable<Student> students,
                                       IEnumerable<Subject> subjects)
        {
            Activities = activities.ToList();
            Students = students.ToList();
            Subjects = subjects.ToList();
        }

        public IEnumerable<Participation> Participations { get; set; }


        [Display(Name = "Activiteiten")]
        public List<Activity> Activities { get; private set; }

        [Display(Name = "Leerlingen")]
        public List<Student> Students { get; private set; }

        [Display(Name = "Onderwerpen")]
        public List<Subject> Subjects { get; private set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public string FromDateAsString {
            get
            {
                return FromDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            set
            {
                var date = new DateTime();
                if (DateTime.TryParse(value, out date))
                {
                    FromDate = date;
                }

            }
        }

        public string ToDateAsString {
            get
            {
                return ToDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            set
            {
                var date = new DateTime();
                if (DateTime.TryParse(value, out date))
                {
                    ToDate = date;
                }

            }
        }
    }
}